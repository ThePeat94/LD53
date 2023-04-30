using System;
using Interactable;
using Scriptables;
using UnityEngine;

namespace DefaultNamespace
{
    public class ComponentObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private ComponentData m_componentData;
        [SerializeField] private Collider m_collider;

        public ComponentData ComponentData => this.m_componentData;


        private void Awake()
        {
            if (this.m_collider is null)
                this.m_collider = this.GetComponentInChildren<Collider>();
        }

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