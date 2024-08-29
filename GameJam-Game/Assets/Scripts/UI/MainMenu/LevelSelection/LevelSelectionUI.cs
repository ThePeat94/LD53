using System;
using System.Collections.Generic;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir.UI
{
    public class LevelSelectionUI : MonoBehaviour
    {
        [SerializeField] private LevelSelectionDisplay m_levelSelectionDisplayPrefab;
        [SerializeField] private RectTransform m_content;
        [SerializeField] private List<LevelData> m_availableLevels;

        private void Awake()
        {
            foreach (var level in this.m_availableLevels)
            {
                var levelSelectionDisplay = Instantiate(this.m_levelSelectionDisplayPrefab, this.m_content);
                levelSelectionDisplay.Init(level);
            }
        }
    }
}