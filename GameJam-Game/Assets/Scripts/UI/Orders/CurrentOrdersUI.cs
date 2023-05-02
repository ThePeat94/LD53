using System.Collections.Generic;
using Nidavellir.EventArgs;
using Nidavellir.Order;
using UnityEngine;

namespace Nidavellir.UI.Orders
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
            this.m_orderManager.OrdersSpawned += this.OnOrdersSpawned;
            this.m_orderManager.OrderDelivered += this.OnOrderDelivered;
            this.m_orderManager.OrderExpired += this.OnOrderExpired;
        }

        private void FixedUpdate()
        {
            this.UpdateOrderTime();
        }

        private void UpdateOrderTime()
        {
            foreach (var (packageOrder, uiOrder) in this.m_orderUis)
            {
                uiOrder.UpdateTime(packageOrder);
            }
        }

        private void OnOrderDelivered(object sender, PackageOrderChangeEventArgs e)
        {
            this.RemoveOrderFromUI(e.PackageOrder);
        }

        private void OnOrderExpired(object sender, PackageOrderChangeEventArgs e)
        {
            this.RemoveOrderFromUI(e.PackageOrder);
        }

        private void RemoveOrderFromUI(PackageOrder order)
        {
            if (!this.m_orderUis.ContainsKey(order))
                return;

            var orderUi = this.m_orderUis[order];
            this.m_orderUis.Remove(order);
            Destroy(orderUi.gameObject);
        }

        private void OnOrdersSpawned(object sender, PackageOrderChangeEventArgs e)
        {
            if(this.m_orderUis.ContainsKey(e.PackageOrder)) 
                return;
            
            var instantiatedOrderUi = Instantiate(this.m_orderUiPrefab, this.m_orderUiParent);
            instantiatedOrderUi.Init(e.PackageOrder);
            this.m_orderUis.Add(e.PackageOrder, instantiatedOrderUi);
        }
    }
}