using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir.LevelRating
{
    public class LevelRatingCalculator
    {
        public const float POINT_PER_LEFT_FRAME = 0.5f;
        public const int POINT_PER_IN_ORDER_ORDER = 75;
        public const int PENALTY_PER_FAILED_ORDER = -75;
        public const int PENALTY_PER_UNNEEDED_ORDER = -100;
        
        public static LevelRatingCalculationResult CalculateLevelRating(LevelRatingMetrics levelRatingMetrics, LevelRatingThresholds levelRatingThresholds)
        {
            var result = new LevelRatingCalculationResult();
            result.LeftTimeFramesSum = levelRatingMetrics.LeftTimeFrames * POINT_PER_LEFT_FRAME;
            result.FailedOrdersSum = levelRatingMetrics.FailedOrders * PENALTY_PER_FAILED_ORDER;
            result.UnneededOrdersSum = levelRatingMetrics.UnneededOrders * PENALTY_PER_UNNEEDED_ORDER;
            result.InOrderOrdersSum = levelRatingMetrics.InOrderOrders * POINT_PER_IN_ORDER_ORDER;

            if (result.TotalSum >= levelRatingThresholds.ThreeStarRatingMinimum)
            {
                result.StarRating = 3;
            } 
            else if (result.TotalSum >= levelRatingThresholds.TwoStarRatingMinimum)
            {
                result.StarRating = 2;
            }
            else
            {
                result.StarRating = 1;
            }

            return result;
        }
    }
}