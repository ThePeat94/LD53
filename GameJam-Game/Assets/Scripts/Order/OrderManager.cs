using System;
using System.Collections.Generic;
using System.Linq;
using EventArgs;
using Interactable;
using Scriptables;
using UnityEngine;

namespace DefaultNamespace.Order
{
    public class OrderManager : MonoBehaviour
    {
        private EventHandler m_ordersChanged;

        [SerializeField] private List<OrderData> m_availableOrders;

        private Queue<PackageOrder> m_currentOrders = new();

        private int m_currentOrderSpawnFrameCountdown;
        private int m_maxOrders = 5;
        private readonly int m_orderSpawnFrameTime = 30;

        private ComponentEndPoint m_endPoint;

        public event EventHandler OrdersChanged
        {
            add => this.m_ordersChanged += value;
            remove => this.m_ordersChanged -= value;
        }
        
        public IReadOnlyCollection<PackageOrder> CurrentOrders => this.m_currentOrders;

        private void Awake()
        {
            this.m_currentOrderSpawnFrameCountdown = this.m_orderSpawnFrameTime;
                
            if (this.m_endPoint is null)
            {
                this.m_endPoint = FindObjectOfType<ComponentEndPoint>();
            }
            this.m_endPoint.PackageDelivered += this.OnPackageDelivered;
        }

        private void FixedUpdate()
        {
            if(this.m_currentOrders.Count >= this.m_maxOrders) return;
            
            this.m_currentOrderSpawnFrameCountdown--;
            if (this.m_currentOrderSpawnFrameCountdown <= 0)
            {
                Debug.Log("Creating new Order");
                this.m_currentOrderSpawnFrameCountdown = this.m_orderSpawnFrameTime;
                var rndOrderData = this.m_availableOrders[UnityEngine.Random.Range(0, this.m_availableOrders.Count)];
                var newPackageOrder = new PackageOrder(rndOrderData);
                this.m_currentOrders.Enqueue(newPackageOrder);
                this.m_ordersChanged?.Invoke(this, System.EventArgs.Empty);
            }
            
            
            
        }

        private void OnPackageDelivered(object sender, System.EventArgs args)
        {
            PackageDeliveryEventArgs eventArgs = (PackageDeliveryEventArgs) args;
            this.PackageWasCompleted(eventArgs.EventPackage);
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
            this.m_ordersChanged?.Invoke(this, System.EventArgs.Empty);
        }

        public void ExpireOrders()
        {

            var copiedOrders = m_currentOrders.ToList();
            var index = 0;
            foreach (var order in this.m_currentOrders)
            {
                order.CurrentFrameCountdown--;
                if (order.CurrentFrameCountdown <= 0)
                {
                    copiedOrders.RemoveAt(index);
                }

                index++;
            }

            if (copiedOrders.Count != m_currentOrders.Count)
            {
                this.m_currentOrders = new Queue<PackageOrder>(copiedOrders);
                this.m_ordersChanged?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}