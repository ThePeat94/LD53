using System;
using System.Collections.Generic;
using DefaultNamespace.Order;
using UnityEngine;

namespace UI
{
    public class CurrentOrdersUI : MonoBehaviour
    {
        [SerializeField] private OrderManager m_orderManager;
        [SerializeField] private OrderUI m_orderUiPrefab;
        [SerializeField] private Transform m_orderUiParent;


        private Dictionary<PackageOrder, OrderUI> m_orderUis = new();

        private void Awake()
        {
            if (this.m_orderManager is null)
            {
                this.m_orderManager = FindObjectOfType<OrderManager>();
            }
            this.m_orderManager.OrdersChanged += this.OnOrdersChanged;
        }

        private void OnOrdersChanged(object sender, System.EventArgs e)
        {
            foreach (var order in this.m_orderManager.CurrentOrders)
            {
                if(this.m_orderUis.ContainsKey(order))
                    continue;
                
                var instantiatedOrderUi = Instantiate(this.m_orderUiPrefab, this.m_orderUiParent);
                instantiatedOrderUi.Init(order);
                this.m_orderUis.Add(order, instantiatedOrderUi);
            }
        }
    }
}