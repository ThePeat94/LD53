using System;
using DefaultNamespace.Order;
using EventArgs;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameWinner : MonoBehaviour
    {
        [SerializeField] private int m_amountOfOrdersToWin;

        private EventHandler m_gameWon;
        
        private int m_currentDeliveredOrders;

        private OrderManager m_orderManager;

        public event EventHandler GameWon
        {
            add => this.m_gameWon += value;
            remove => this.m_gameWon -= value;
        }
        
        private void Awake()
        {
            this.m_orderManager = FindObjectOfType<OrderManager>();
            this.m_orderManager.OrderDelivered += this.OnOrderSuccess;
        }

        private void OnOrderSuccess(object sender, PackageOrderChangeEventArgs e)
        {
            this.m_currentDeliveredOrders++;
            if (this.m_currentDeliveredOrders >= this.m_amountOfOrdersToWin)
            {
                this.m_gameWon?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}