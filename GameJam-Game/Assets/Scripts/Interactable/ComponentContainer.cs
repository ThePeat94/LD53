using Scriptables;
using UnityEngine;

namespace Interactable
{
    public class ComponentContainer : MonoBehaviour, IInteractable
    {
        [SerializeField] private ComponentData m_containedComponent;

        public void Interact(InteractingEntity interactingEntity)
        {
            Instantiate(this.m_containedComponent.Model, interactingEntity.ComponentParent);
        }
    }
}