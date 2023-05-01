using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WinConditionUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_ordersNeededText;
        
        private GameWinner m_gameWinner;

        private void Awake()
        {
            this.m_gameWinner = FindObjectOfType<GameWinner>();
            this.m_gameWinner.SucceededOrder += this.OnSuccessfulOrder;
        }

        private void Start()
        {
            this.m_ordersNeededText.text = $"{this.m_gameWinner.CurrentDeliveredOrders}/{this.m_gameWinner.AmountOfOrdersToWin} Orders";
        }

        private void OnSuccessfulOrder(object sender, System.EventArgs e)
        {
            this.m_ordersNeededText.text = $"{this.m_gameWinner.CurrentDeliveredOrders}/{this.m_gameWinner.AmountOfOrdersToWin} Orders";
        }
    }
}