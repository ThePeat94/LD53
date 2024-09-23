namespace Nidavellir.LevelRating
{
    /// <summary>
    /// Contains the calculated results about a level to be evaluated in further operations (e. g. Level Rating)
    /// </summary>
    public class LevelRatingCalculationResult
    {
        public float FailedOrdersSum { get; set; }
        public float InOrderOrdersSum { get; set; }
        public float LeftTimeFramesSum { get; set; }
        public float UnneededOrdersSum { get; set; }
        public int StarRating { get; set; }
        public float TotalSum => this.FailedOrdersSum + this.InOrderOrdersSum + this.LeftTimeFramesSum + this.UnneededOrdersSum;
    }
}