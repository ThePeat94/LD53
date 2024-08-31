namespace Nidavellir.LevelRating
{
    public class LevelRatingMetrics
    {
        public int FailedOrders { get; }
        public int InOrderOrders { get; }
        public int LeftTimeFrames { get; }
        public int UnneededOrders { get; }

        public LevelRatingMetrics(int failedOrders, int inOrderOrders, int leftTimeFrames, int unneededOrders)
        {
            this.FailedOrders = failedOrders;
            this.InOrderOrders = inOrderOrders;
            this.LeftTimeFrames = leftTimeFrames;
            this.UnneededOrders = unneededOrders;
        }
    }
}