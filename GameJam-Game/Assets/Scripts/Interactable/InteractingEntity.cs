using UnityEngine;
using UnityEngine.Serialization;

namespace Interactable
{
    public class InteractingEntity : MonoBehaviour
    {
        [SerializeField] private Transform m_componentHolder;

        public Transform ComponentHolder => this.m_componentHolder;
    }
}