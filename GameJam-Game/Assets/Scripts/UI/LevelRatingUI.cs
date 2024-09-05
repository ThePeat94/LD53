using Nidavellir.LevelRating;
using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class LevelRatingUI : MonoBehaviour
    {
        [SerializeField] private GameObject m_failedRatingPanel;
        [SerializeField] private GameObject m_inOrderRatingPanel;
        [SerializeField] private GameObject m_timeLeftRatingPanel;
        [SerializeField] private GameObject m_extraOrdersRatingPanel;
        [SerializeField] private TextMeshProUGUI m_totalSumText;
        [SerializeField] private TextMeshProUGUI m_starAmountText;
        

        public void ShowLevelRatings(LevelRatingMetrics metrics, LevelRatingCalculationResult result)
        {
            this.ShowRatingInPanel(this.m_failedRatingPanel, metrics.FailedOrders, LevelRatingCalculator.PENALTY_PER_FAILED_ORDER, result.FailedOrdersSum);
            this.ShowRatingInPanel(this.m_inOrderRatingPanel, metrics.InOrderOrders, LevelRatingCalculator.POINT_PER_IN_ORDER_ORDER, result.InOrderOrdersSum);
            this.ShowRatingInPanel(this.m_timeLeftRatingPanel, metrics.LeftTimeFrames, LevelRatingCalculator.POINT_PER_LEFT_FRAME, result.LeftTimeFramesSum);
            this.ShowRatingInPanel(this.m_extraOrdersRatingPanel, metrics.UnneededOrders, LevelRatingCalculator.PENALTY_PER_UNNEEDED_ORDER, result.UnneededOrdersSum);
            this.m_totalSumText.text = $"{result.TotalSum:F2}";
            this.m_starAmountText.text = $"{result.StarRating}/3";
        }

        private void ShowRatingInPanel(GameObject targetPanel, int amount, float weight, float sum)
        {
            var texts = targetPanel.GetComponentsInChildren<TextMeshProUGUI>();
            var amountText = texts[1];
            var sumText = texts[2];

            amountText.text = $"{amount} x {weight}";
            sumText.text = $"= {sum:F2}";
        }
    }
}