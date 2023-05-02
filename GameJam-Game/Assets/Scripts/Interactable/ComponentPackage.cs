using System;
using System.Collections.Generic;
using Nidavellir.EventArgs;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir.Interactable
{
    public class ComponentPackage: MonoBehaviour, IInteractable
    {
        [SerializeField] private Collider m_collider;
        [SerializeField] private ComponentData initialData;

        private EventHandler<ComponentAddedEventArgs> m_componentAdded;
        
        private List<ComponentData> m_componentDatas = new();

        
        public event EventHandler<ComponentAddedEventArgs> ComponentAdded
        {
            add => this.m_componentAdded += value;
            remove => this.m_componentAdded -= value;
        }

        public IReadOnlyList<ComponentData> ContainedComponents => this.m_componentDatas;
        public ComponentData ComponentData => this.initialData;


        private void Awake()
        {
            if (this.m_collider == null)
                this.m_collider = this.GetComponentInChildren<Collider>();
        }

        private void Start()
        {
            this.AddComponent(this.initialData);
        }

        public void AddComponent(ComponentData data)
        {
            this.m_componentDatas.Add(data);
            this.m_componentAdded?.Invoke(this, new(data));
        }

        public IInteractable Interact(InteractingEntity interactingEntity)
        {
            this.transform.SetParent(interactingEntity.ComponentHolder);
            this.transform.localPosition = Vector3.zero;
            this.Deactivate();
            return this;
        }
        
        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable)
        {
            this.AddComponent(interactingEntity.ComponentHolder.GetComponentInChildren<ComponentObject>().ComponentData);
            Destroy(interactingEntity.ComponentHolder.GetChild(0).gameObject);
            return null;
        }

        public bool CanInteractUsingInteractable(IInteractable interactable)
        {
            return interactable is ComponentObject;
        }

        public void Activate()
        {
            if (this.m_collider == null)
                return;
            this.m_collider.enabled = true;
        }

        public void Deactivate()
        {
            if (this.m_collider == null)
                return;
            this.m_collider.enabled = false;
        }
    }
}   