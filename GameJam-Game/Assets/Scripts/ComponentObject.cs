using Interactable;
using Scriptables;
using UnityEngine;

namespace DefaultNamespace
{
    public class ComponentObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private ComponentData m_componentData;

        public ComponentData ComponentData => this.m_componentData;
        public IInteractable Interact(InteractingEntity interactingEntity)
        {
            throw new System.NotImplementedException();
        }

        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable)
        {
            throw new System.NotImplementedException();
        }

        public bool CanInteractUsingInteractable(IInteractable interactable)
        {
            throw new System.NotImplementedException();
        }
    }
}