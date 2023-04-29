using UnityEngine;

namespace Interactable
{
    public class InteractingEntity : MonoBehaviour
    {
        [SerializeField] private Transform m_componentParent;

        public Transform ComponentParent => this.m_componentParent;
    }
}