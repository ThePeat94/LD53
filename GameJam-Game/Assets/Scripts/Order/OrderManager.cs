using System;
using System.Collections.Generic;
using System.Linq;
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

        public event EventHandler OrdersChanged
        {
            add => this.m_ordersChanged += value;
            remove => this.m_ordersChanged -= value;
        }
        
        public IReadOnlyCollection<PackageOrder> CurrentOrders => this.m_currentOrders;

        private void Awake()
        {
            this.m_currentOrderSpawnFrameCountdown = this.m_orderSpawnFrameTime;
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
                return;
            
            var listedOrders = this.m_currentOrders.ToList();
            listedOrders.RemoveAt(index);
            this.m_currentOrders = new Queue<PackageOrder>(listedOrders);
            this.m_ordersChanged?.Invoke(this, System.EventArgs.Empty);

        }
    }
}