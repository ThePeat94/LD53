using UnityEngine;

namespace Nidavellir.LevelRating
{
    /// <summary>
    /// Describes the minimum thresholds to reach this amount of stars per level
    /// </summary>
    [System.Serializable]
    public class LevelRatingThresholds
    {
        [SerializeField] private int m_oneStarRatingMinimum;
        [SerializeField] private int m_twoStarRatingMinimum;
        [SerializeField] private int m_threeStarRatingMinimum;

        public int OneStarRatingMinimum => this.m_oneStarRatingMinimum;
        public int TwoStarRatingMinimum => this.m_twoStarRatingMinimum;
        public int ThreeStarRatingMinimum => this.m_threeStarRatingMinimum;
    }
}