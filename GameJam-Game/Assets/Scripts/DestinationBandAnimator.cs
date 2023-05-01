using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class DestinationBandAnimator : MonoBehaviour
    {
        private void Start()
        {
            var material = this.GetComponent<MeshRenderer>().material;
            material.DOOffset(new Vector2(-1f, 0), 1f).SetLoops(-1).SetEase(Ease.Linear);
        }
    }
}