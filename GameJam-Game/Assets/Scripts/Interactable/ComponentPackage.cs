using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using Scriptables;
using UnityEngine;

namespace Interactable
{
    public class ComponentPackage: MonoBehaviour, IInteractable
    {
        private List<ComponentData> m_componentDatas = new();

        public IReadOnlyList<ComponentData> ContainedComponents => this.m_componentDatas;

        private void AddComponent(ComponentData data)
        {
            m_componentDatas.Add(data);
        }

        public IInteractable Interact(InteractingEntity interactingEntity)
        {
            this.transform.SetParent(interactingEntity.ComponentParent);
            this.transform.localPosition = Vector3.zero;
            return this;
        }
        
        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable)
        {
            AddComponent(interactingEntity.ComponentParent.GetComponentInChildren<ComponentObject>().ComponentData);
            Destroy(interactingEntity.ComponentParent.GetChild(0).gameObject);
            return null;
        }

        public bool CanInteractUsingInteractable(IInteractable interactable)
        {
            return interactable is ComponentObject;
        }
    }
}   