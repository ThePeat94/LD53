using System;
using System.Globalization;
using DefaultNamespace.Order;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OrderUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_orderName;
        [SerializeField] private TextMeshProUGUI m_restTime;
        [SerializeField] private Image m_image;
        [SerializeField] private Transform m_componentGridParent;
        [SerializeField] private MiniComponentUI m_miniComponentPrefab;

        private Sequence m_sequence;

        public void Init(PackageOrder packageOrder)
        {
            this.m_orderName.text = packageOrder.OrderData.Name;
            this.m_image.sprite = packageOrder.OrderData.Icon;
            foreach (var neededComponent in packageOrder.OrderData.NeededComponents)
            {
                var instantiatedMiniComponent = Instantiate(this.m_miniComponentPrefab, this.m_componentGridParent);
                instantiatedMiniComponent.Init(neededComponent);
            }
            
            this.transform.DOPunchScale(new Vector3(1, 1, 0) * 0.2f, 0.5f, 8, 0.8f);
        }

        public void UpdateTime(PackageOrder order)
        {
            this.m_restTime.text = Convert.ToString((int)(order.CurrentFrameCountdown * Time.fixedDeltaTime), CultureInfo.InvariantCulture);

            if (this.m_sequence == null && order.CurrentFrameCountdown <= order.OrderData.FrameTime * 0.2f)
            {
                this.m_sequence = DOTween.Sequence();
                this.m_sequence.Append(this.transform.DOPunchScale(new Vector3(1, 1, 0) * 0.2f, 0.5f, 8, 0.8f));
                this.m_sequence.SetLoops(-1);
                this.m_sequence.SetEase(Ease.InOutSine);
                this.m_sequence.Play();   
            }
        }
    }
}