using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class DestinationBandAnimator : MonoBehaviour
    {
        [SerializeField] private Material m_bandMaterial;

        private void Start()
        {
            this.m_bandMaterial.DOOffset(new Vector2(5, 0), 1f).SetLoops(-1).SetEase(Ease.Linear);
        }
    }
}