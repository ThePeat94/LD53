using DefaultNamespace;
using Scriptables;
using UnityEngine;

namespace Interactable
{
    public class InteractableMachine : MonoBehaviour, IInteractable
    {
        [SerializeField] private ComponentData m_needsComponent;
        [SerializeField] private Transform m_componentPackagePlace;

        private Animator m_animator;

        private ComponentData m_currentComponentData;
        private ComponentPackage m_currentComponentPackage; 

        public IInteractable Interact(InteractingEntity interactingEntity)
        {
            if (this.m_currentComponentPackage is not null && this.m_currentComponentData is null)
            {
                this.m_currentComponentPackage.transform.SetParent(interactingEntity.ComponentHolder);
                this.m_currentComponentPackage.transform.localPosition = Vector3.zero;
                var toReturn = this.m_currentComponentPackage;
                this.m_currentComponentPackage = null;
                Debug.Log("Taking component package");
                return toReturn;
            }
            
            if (this.m_currentComponentData is null || this.m_currentComponentPackage is null)
            {
                Debug.Log("Empty machine, nothing to do.");
                return null;
            }
            
            Debug.Log("Applying component");
            this.m_currentComponentPackage.AddComponent(this.m_currentComponentData);
            this.m_currentComponentData = null;
            
            return null;
        }

        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable)
        {
            if (interactable is ComponentObject co)
            {
                if (co.ComponentData != this.m_needsComponent)
                {
                    Debug.Log("Invalid component object");
                    return interactable;
                }
                
                Debug.Log("Inserting Component object");
                this.m_currentComponentData = co.ComponentData;
                Destroy(co.gameObject);
                return null;
            }
            
            if (interactable is ComponentPackage)
            {
                Debug.Log("Inserting Component package");
                this.m_currentComponentPackage = interactable as ComponentPackage;
                this.m_currentComponentPackage.transform.SetParent(this.m_componentPackagePlace);
                this.m_currentComponentPackage.transform.localPosition = Vector3.zero;
                this.m_currentComponentPackage.Deactivate();
                return null;
            }

            return interactable;
        }

        public bool CanInteractUsingInteractable(IInteractable interactable)
        {
            return interactable is ComponentPackage or ComponentObject;
        }

        public void Activate()
        {
        }

        public void Deactivate()
        {
        }
    }
}