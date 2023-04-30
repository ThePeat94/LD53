using System;
using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using Scriptables;
using UnityEngine;

namespace Interactable
{
    public class ComponentPackage: MonoBehaviour, IInteractable
    {
        [SerializeField] private Collider m_collider;

        private List<ComponentData> m_componentDatas = new();

        public IReadOnlyList<ComponentData> ContainedComponents => this.m_componentDatas;

        private void Awake()
        {
            if (this.m_collider == null)
                this.m_collider = this.GetComponentInChildren<Collider>();
        }

        public void AddComponent(ComponentData data)
        {
            this.m_componentDatas.Add(data);
        }

        public IInteractable Interact(InteractingEntity interactingEntity)
        {
            this.transform.SetParent(interactingEntity.ComponentParent);
            this.transform.localPosition = Vector3.zero;
            return this;
        }
        
        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable)
        {
            this.AddComponent(interactingEntity.ComponentParent.GetComponentInChildren<ComponentObject>().ComponentData);
            Destroy(interactingEntity.ComponentParent.GetChild(0).gameObject);
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