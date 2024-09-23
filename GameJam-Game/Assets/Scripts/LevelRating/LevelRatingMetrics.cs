namespace Nidavellir.LevelRating
{
    /// <summary>
    /// The actual metrics of a performed level
    /// </summary>
    public class LevelRatingMetrics
    {
        public int FailedOrders { get; set; }
        public int InOrderOrders { get; set; }
        public int LeftTimeFrames { get; set; }
        public int UnneededOrders { get; set; }
    }
}