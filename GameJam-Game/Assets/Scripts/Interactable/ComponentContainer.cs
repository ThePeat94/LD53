using Scriptables;
using UnityEngine;

namespace Interactable
{
    public class ComponentContainer : MonoBehaviour, IInteractable
    {
        [SerializeField] private ComponentData m_containedComponent;

        public IInteractable Interact(InteractingEntity interactingEntity)
        {
            return Instantiate(this.m_containedComponent.Model, interactingEntity.ComponentParent).GetComponent<IInteractable>();
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