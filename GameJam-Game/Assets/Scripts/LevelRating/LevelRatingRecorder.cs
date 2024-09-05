using System;
using Nidavellir.EventArgs;
using Nidavellir.Order;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir.LevelRating
{
    public class LevelRatingRecorder : MonoBehaviour
    {
        [SerializeField] private LevelData m_currentLevel;
        [SerializeField] private OrderManager m_orderManager;
        [SerializeField] private Timer m_timer;
        [SerializeField] private GameWinner m_gameWinner;
        
        private LevelRatingMetrics m_levelRatingMetrics = new();
        
        public LevelRatingMetrics LevelRatingMetrics => this.m_levelRatingMetrics;

        private void Awake()
        {
            if (this.m_orderManager is null)
            {
                this.m_orderManager = FindObjectOfType<OrderManager>();
            }
            
            if (this.m_timer is null)
            {
                this.m_timer = FindObjectOfType<Timer>();
            }
            
            if (this.m_gameWinner is null)
            {
                this.m_gameWinner = FindObjectOfType<GameWinner>();
            }
            
            this.m_orderManager.OrderExpired += this.OnOrderExpired;
            this.m_orderManager.UnneededOrderDelivered += this.OnUnneededOrderDelivered;
            this.m_orderManager.OrderDelivered += this.OnOrderDelivered;
            this.m_gameWinner.GameWon += this.OnGameWon;
        }

        private void OnGameWon(object sender, System.EventArgs e)
        {
            this.m_levelRatingMetrics.LeftTimeFrames = this.m_timer.RemainingFrameTime;
        }

        private void OnOrderDelivered(object sender, PackageOrderChangeEventArgs e)
        {
            if (e.IsFirstOrder)
            {
                this.m_levelRatingMetrics.InOrderOrders++;
            }
        }

        private void OnUnneededOrderDelivered(object sender, PackageOrderChangeEventArgs e)
        {
            this.m_levelRatingMetrics.UnneededOrders++;
        }

        private void OnOrderExpired(object sender, PackageOrderChangeEventArgs e)
        {
            this.m_levelRatingMetrics.FailedOrders++;
        }
    }
}