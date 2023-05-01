﻿using System;
using DefaultNamespace.Order;
using EventArgs;
using Scriptables;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameWinner : MonoBehaviour
    {
        [SerializeField] private LevelData m_levelData;

        private int m_amountOfOrdersToWin;
        private EventHandler m_gameWon;
        private EventHandler m_succeededOrder;
        
        private int m_currentDeliveredOrders;
        private OrderManager m_orderManager;

        public int AmountOfOrdersToWin => this.m_amountOfOrdersToWin;
        public int CurrentDeliveredOrders => this.m_currentDeliveredOrders;
        
        public event EventHandler GameWon
        {
            add => this.m_gameWon += value;
            remove => this.m_gameWon -= value;
        }
        
        public event EventHandler SucceededOrder
        {
            add => this.m_succeededOrder += value;
            remove => this.m_succeededOrder -= value;
        }

        private void Awake()
        {
            this.m_amountOfOrdersToWin = this.m_levelData.NeededOrdersToFulfill;
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
            else
            {
                this.m_succeededOrder?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}