using DG.Tweening;
using UnityEngine;

namespace Nidavellir
{
    public class DestinationBandAnimator : MonoBehaviour
    {
        [SerializeField] private float m_bandSpeed;
        
        private void Start()
        {
            var material = this.GetComponent<MeshRenderer>().material;
            material.DOOffset(new Vector2(-1f, 0), this.m_bandSpeed).SetLoops(-1).SetEase(Ease.Linear);
        }
    }
}