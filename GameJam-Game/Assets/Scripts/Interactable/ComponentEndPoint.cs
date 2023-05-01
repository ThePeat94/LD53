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
        private EventHandler<PackageDeliveryEventArgs> m_packageDelivered;

        public event EventHandler<PackageDeliveryEventArgs> PackageDelivered
        {
            add => this.m_packageDelivered += value;
            remove => this.m_packageDelivered -= value;
        }

        public IInteractable Interact(InteractingEntity interactingEntity)
        {
            Debug.Log("Can not use");
            return interactingEntity.ComponentHolder.childCount == 1 ? interactingEntity.ComponentHolder.GetChild(0).GetComponent<IInteractable>() : null;
        }

        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable)
        {
            if (interactable is not ComponentPackage componentPackage)
                return interactable;
            
            this.m_packageDelivered?.Invoke(this,new PackageDeliveryEventArgs(componentPackage));
            
            Destroy(componentPackage.gameObject);
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
