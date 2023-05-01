using System;
using System.Collections.Generic;
using System.Linq;
using EventArgs;
using Interactable;
using Scriptables;
using Unity.XR.OpenVR;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Order
{
    public class OrderManager : MonoBehaviour
    {
        private EventHandler<PackageOrderChangeEventArgs> m_orderSpawned;
        // TODO: Subscribe in time manager to this event and cut of time
        private EventHandler<PackageOrderChangeEventArgs> m_orderExpired; 
        // TODO: Subscribe in time manager to this event and add time
        private EventHandler<PackageOrderChangeEventArgs> m_orderDelivered;

        [SerializeField] private LevelData m_levelData;
        [SerializeField] private GameStateManager m_gameStateManager;
        
        private Queue<PackageOrder> m_currentOrders = new();

        private List<OrderData> m_availableOrders;
        private int m_currentOrderSpawnFrameCountdown;
        private int m_maxOrders = 5;
        
        private ComponentEndPoint m_endPoint;

        public event EventHandler<PackageOrderChangeEventArgs> OrdersSpawned
        {
            add => this.m_orderSpawned += value;
            remove => this.m_orderSpawned -= value;
        }
        
        public event EventHandler<PackageOrderChangeEventArgs> OrderExpired
        {
            add => this.m_orderExpired += value;
            remove => this.m_orderExpired -= value;
        }
        
        public event EventHandler<PackageOrderChangeEventArgs> OrderDelivered
        {
            add => this.m_orderDelivered += value;
            remove => this.m_orderDelivered -= value;
        }
        
        public IReadOnlyCollection<PackageOrder> CurrentOrders => this.m_currentOrders;

        private void Awake()
        {
            this.m_availableOrders = this.m_levelData.AvailableOrders.ToList();
            this.m_currentOrderSpawnFrameCountdown = Random.Range(this.m_levelData.MinFramesForOrderSpawn, this.m_levelData.MaxFramesForOrderSpawn + 1);
                
            if (this.m_endPoint is null)
            {
                this.m_endPoint = FindObjectOfType<ComponentEndPoint>();
            }
            this.m_endPoint.PackageDelivered += this.OnPackageDelivered;

            if (this.m_gameStateManager == null)
            {
                this.m_gameStateManager = FindObjectOfType<GameStateManager>();
            }
            
        }


        private void FixedUpdate()
        {
            if (this.m_gameStateManager.CurrentState != GameStateManager.State.Playing)
                return;
            
            this.ExpireOrders();
            this.SpawnNewOrders();
        }

        private void SpawnNewOrders()
        {
            if (this.m_currentOrders.Count >= this.m_maxOrders) return;

            this.m_currentOrderSpawnFrameCountdown--;
            if (this.m_currentOrderSpawnFrameCountdown <= 0)
            {
                Debug.Log("Creating new Order");
                this.m_currentOrderSpawnFrameCountdown = Random.Range(this.m_levelData.MinFramesForOrderSpawn, this.m_levelData.MaxFramesForOrderSpawn + 1);
                var rndOrderData = this.m_availableOrders[UnityEngine.Random.Range(0, this.m_availableOrders.Count)];
                var newPackageOrder = new PackageOrder(rndOrderData);
                this.m_currentOrders.Enqueue(newPackageOrder);
                this.m_orderSpawned?.Invoke(this, new PackageOrderChangeEventArgs(newPackageOrder));
            }
        }

        private void OnPackageDelivered(object sender, PackageDeliveryEventArgs args)
        {
            this.PackageWasCompleted(args.EventPackage);
        }

        private void PackageWasCompleted(ComponentPackage deliveredPackage)
        {
            var copiedOrderQueue = new Queue<PackageOrder>(this.m_currentOrders);
            var index = -1;
            PackageOrder orderWithAll = null; 
            while (copiedOrderQueue.Count > 0)
            {
                index++;
                var order = copiedOrderQueue.Dequeue();
                var foundOrderContainingEachComponent = false;
                if (order.OrderData.NeededComponents.Count != deliveredPackage.ContainedComponents.Count)
                {
                    continue;
                }
                foreach (var contained in deliveredPackage.ContainedComponents)
                {
                    if (!order.OrderData.NeededComponents.Contains(contained))
                    {
                        break;
                    }

                    foundOrderContainingEachComponent = true;
                }

                if (foundOrderContainingEachComponent)
                {
                    orderWithAll = order;
                    break;
                }
            }

            if (orderWithAll is null)
            {
                Debug.Log("Order not fullfilled");
                return;
            }

            var listedOrders = this.m_currentOrders.ToList();
            listedOrders.RemoveAt(index);
            this.m_currentOrders = new Queue<PackageOrder>(listedOrders);
            this.m_orderDelivered?.Invoke(this, new PackageOrderChangeEventArgs(orderWithAll));
        }

        public void ExpireOrders()
        {

            var copiedOrders = this.m_currentOrders.ToList();
            var expiredOrders = new List<PackageOrder>();
            var index = 0;
            foreach (var order in this.m_currentOrders)
            {
                order.CurrentFrameCountdown--;
                if (order.CurrentFrameCountdown <= 0)
                {
                    copiedOrders.RemoveAt(index);
                    expiredOrders.Add(order);
                }

                index++;
            }

            if (copiedOrders.Count != this.m_currentOrders.Count)
            {
                this.m_currentOrders = new Queue<PackageOrder>(copiedOrders);
                foreach (var expiredOrder in expiredOrders)
                {
                    this.m_orderExpired?.Invoke(this, new PackageOrderChangeEventArgs(expiredOrder));
                }
            }
        }
    }
}