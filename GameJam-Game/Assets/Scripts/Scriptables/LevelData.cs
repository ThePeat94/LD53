using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "Data/Level Data", order = 0)]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int m_initialFrameTime;
        [SerializeField] private List<OrderData> m_availableOrders;
        [SerializeField] private int m_neededOrdersToFulfill;
        [SerializeField] private int m_minFramesForOrderSpawn;
        [SerializeField] private int m_maxFramesForOrderSpawn;
        [SerializeField] private int m_firstOrderAfterFrames;
        [SerializeField] private OrderData m_safeOrderData;


        public int InitialFrameTime => this.m_initialFrameTime;
        public IReadOnlyList<OrderData> AvailableOrders => this.m_availableOrders;
        public int NeededOrdersToFulfill => this.m_neededOrdersToFulfill;
        public int MinFramesForOrderSpawn => this.m_minFramesForOrderSpawn;
        public int MaxFramesForOrderSpawn => this.m_maxFramesForOrderSpawn;
        public int FirstOrderAfterFrames => this.m_firstOrderAfterFrames;
        public OrderData SafeOrderData => this.m_safeOrderData;
    }
}