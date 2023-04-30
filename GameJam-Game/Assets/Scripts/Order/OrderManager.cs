using System;
using System.Collections.Generic;
using Scriptables;
using UnityEngine;

namespace DefaultNamespace.Order
{
    public class OrderManager : MonoBehaviour
    {
        private EventHandler m_ordersChanged;

        [SerializeField] private List<OrderData> m_availableOrders;

        private Queue<OrderData> m_currentOrders = new();

        private int m_currentOrderSpawnFrameCountdown;
        private readonly int m_orderSpawnFrameTime = 300;

        public event EventHandler OrdersChanged
        {
            add => this.m_ordersChanged += value;
            remove => this.m_ordersChanged -= value;
        }
        
        public List<OrderData> AvailableOrders => this.m_availableOrders;

        private void Awake()
        {
            this.m_currentOrderSpawnFrameCountdown = this.m_orderSpawnFrameTime;
        }

        private void FixedUpdate()
        {
            this.m_currentOrderSpawnFrameCountdown--;
            if (this.m_currentOrderSpawnFrameCountdown <= 0)
            {
                this.m_currentOrderSpawnFrameCountdown = this.m_orderSpawnFrameTime;
                this.m_currentOrders.Enqueue(this.m_availableOrders[UnityEngine.Random.Range(0, this.m_availableOrders.Count)]);
                this.m_ordersChanged?.Invoke(this, System.EventArgs.Empty);
            }
        }

        private void OrderWasCompleted(OrderData completedOrderData)
        {
            this.DequeueFirstFoundOrder(completedOrderData);
            this.m_ordersChanged?.Invoke(this, System.EventArgs.Empty);
        }

        private void DequeueFirstFoundOrder(OrderData completedOrderData)
        {
            var tmpQueue = new Queue<OrderData>();
            var foundFirstOrder = false;
            while (this.m_currentOrders.Count > 0)
            {
                var order = this.m_currentOrders.Dequeue();
                if (order != completedOrderData || foundFirstOrder)
                {
                    tmpQueue.Enqueue(order);
                }
                else
                {
                    foundFirstOrder = true;
                }
            }

            this.m_currentOrders = tmpQueue;
        }
    }
}