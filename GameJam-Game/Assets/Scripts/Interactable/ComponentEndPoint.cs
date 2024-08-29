using System;
using Nidavellir.EventArgs;
using UnityEngine;

namespace Nidavellir.Interactable
{
    /// <summary>
    /// An end point for other components, which can receive packages and components
    /// Used to fulfill orders
    /// </summary>
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
            return interactingEntity.ComponentHolder.childCount == 1 ? interactingEntity.ComponentHolder.GetChild(0).GetComponent<IInteractable>() : null;
        }

        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable)
        {
            if (interactable is ComponentObject co)
            {
                Destroy(co.gameObject);
                return null;
            }
            
            if (interactable is not ComponentPackage componentPackage)
                return interactable;
            
            this.m_packageDelivered?.Invoke(this,new PackageDeliveryEventArgs(componentPackage));
            
            Destroy(componentPackage.gameObject);
            return null;
        }

        public bool CanInteractUsingInteractable(IInteractable interactable)
        {
            return interactable is ComponentPackage || interactable is ComponentObject;
        }
    }
}
