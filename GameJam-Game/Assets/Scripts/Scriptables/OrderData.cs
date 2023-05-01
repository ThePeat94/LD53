﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Order Data", menuName = "Data/Order Data", order = 0)]
    public class OrderData : ScriptableObject
    {
        [SerializeField] private String m_name;
        [SerializeField] private Sprite m_icon;
        [SerializeField] private List<ComponentData> m_neededComponents;
        [SerializeField] private int m_frameTime;
        [SerializeField] private int m_punishFrames = 200;
        [SerializeField] private int m_rewardFrames = 100;

        public Sprite Icon => this.m_icon;
        public IReadOnlyList<ComponentData> NeededComponents => this.m_neededComponents;
        public int FrameTime => this.m_frameTime;
        public string Name => this.m_name;

        public int PunishFrames => this.m_punishFrames;
        public int RewardFrames => this.m_rewardFrames;

    }
}