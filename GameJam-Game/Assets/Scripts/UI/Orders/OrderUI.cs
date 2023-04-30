using DefaultNamespace.Order;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OrderUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_orderName;
        [SerializeField] private Image m_image;
        [SerializeField] private Transform m_componentGridParent;
        [SerializeField] private MiniComponentUI m_miniComponentPrefab;

        public void Init(PackageOrder packageOrder)
        {
            this.m_orderName.text = packageOrder.OrderData.Name;
            this.m_image.sprite = packageOrder.OrderData.Icon;
            foreach (var neededComponent in packageOrder.OrderData.NeededComponents)
            {
                var instantiatedMiniComponent = Instantiate(this.m_miniComponentPrefab, this.m_componentGridParent);
                instantiatedMiniComponent.Init(neededComponent);
            }
        }
    }
}