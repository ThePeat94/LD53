using System;
using Scriptables;
using UnityEngine;

namespace Interactable
{
    public class ComponentContainer : MonoBehaviour, IInteractable
    {
        [SerializeField] private ComponentData m_containedComponent;
        [SerializeField] private Collider m_collider;

        private void Awake()
        {
            if (this.m_collider is null)
                this.m_collider = this.GetComponentInChildren<Collider>();
        }

        public IInteractable Interact(InteractingEntity interactingEntity)
        {
            return Instantiate(this.m_containedComponent.Model, interactingEntity.ComponentHolder).GetComponent<IInteractable>();
        }

        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable)
        {
            return interactable;
        }

        public bool CanInteractUsingInteractable(IInteractable interactable)
        {
            return false;
        }

        public void Activate()
        {
            this.m_collider.enabled = true;
        }

        public void Deactivate()
        {
            this.m_collider.enabled = false;
        }
    }
}