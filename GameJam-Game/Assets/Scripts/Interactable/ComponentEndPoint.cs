using System;
using System.Collections.Generic;
using System.Linq;
using EventArgs;
using Scriptables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interactable
{
    public class ComponentEndPoint : MonoBehaviour, IInteractable
    {
        private EventHandler m_PackageDelivered;

        public IInteractable Interact(InteractingEntity interactingEntity)
        {
            Debug.Log("Can not use");
            return interactingEntity.ComponentHolder.childCount == 1 ? interactingEntity.ComponentHolder.GetChild(0).GetComponent<IInteractable>() : null;
        }
        
        public event EventHandler PackageDelivered
        {
            add => this.m_PackageDelivered += value;
            remove => this.m_PackageDelivered -= value;
        }

        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable)
        {
            ComponentPackage componentPackage = interactingEntity.ComponentHolder.GetComponentInChildren<ComponentPackage>();
            m_PackageDelivered?.Invoke(this,new PackageDeliveryEventArgs(componentPackage));
            
            Destroy(interactingEntity.ComponentHolder.GetChild(0).gameObject);
            return null;
        }

        public bool CanInteractUsingInteractable(IInteractable interactable)
        {
            return interactable is ComponentPackage;
        }

        public void Activate() { }

        public void Deactivate() { }
    }
}
