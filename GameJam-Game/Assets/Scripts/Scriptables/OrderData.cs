using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Order Data", menuName = "Data/Order Data", order = 0)]
    public class OrderData : ScriptableObject
    {
        [SerializeField] private String m_name;
        [SerializeField] private Sprite m_icon;
        [SerializeField] private List<ComponentData> m_neededComponents;
        [SerializeField] private int m_frameTime;

        public Sprite Icon => this.m_icon;
        public List<ComponentData> NeededComponents => this.m_neededComponents;
        public int FrameTime => this.m_frameTime;
        public string Name => this.m_name;
    }
}