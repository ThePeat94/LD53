using Nidavellir.Scriptables;

namespace Nidavellir.LevelRating
{
    public class LevelRatingCalculator
    {
        private const float POINT_PER_LEFT_FRAME = 0.5f;
        private const int POINT_PER_IN_ORDER_ORDER = 75;
        private const int PENALTY_PER_FAILED_ORDER = 75;
        private const int PENALTY_PER_UNNEEDED_ORDER = 100;
        
        public static int CalculateLevelRating(LevelRatingMetrics levelRatingMetrics, LevelRatingThresholds levelRatingThresholds)
        {
            var calculatedRating = 0f;

            calculatedRating += levelRatingMetrics.LeftTimeFrames * POINT_PER_LEFT_FRAME;
            calculatedRating += levelRatingMetrics.FailedOrders * PENALTY_PER_FAILED_ORDER;
            calculatedRating += levelRatingMetrics.UnneededOrders * PENALTY_PER_UNNEEDED_ORDER;
            calculatedRating += levelRatingMetrics.InOrderOrders * POINT_PER_IN_ORDER_ORDER;

            if (calculatedRating >= levelRatingThresholds.ThreeStarRatingMinimum)
            {
                return 3;
            }

            if (calculatedRating >= levelRatingThresholds.TwoStarRatingMinimum)
            {
                return 2;
            }

            return 1;
        }
    }
}