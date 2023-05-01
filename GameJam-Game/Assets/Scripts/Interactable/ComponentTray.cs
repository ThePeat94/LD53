using DefaultNamespace;
using UnityEngine;

namespace Interactable
{
    public class ComponentTray : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform m_componentHolderParent;
        
        private IInteractable m_currentDepositedObject;

        public IInteractable Interact(InteractingEntity interactingEntity)
        {
            var toReturn = this.m_currentDepositedObject;
            switch (this.m_currentDepositedObject)
            {
                case null:
                    return null;
                case ComponentPackage cp:
                    cp.transform.SetParent(interactingEntity.ComponentHolder);
                    cp.transform.localPosition = Vector3.zero;
                    this.m_currentDepositedObject = null;
                    return toReturn;
                case ComponentObject co:
                    co.transform.SetParent(interactingEntity.ComponentHolder);
                    co.transform.localPosition = Vector3.zero;
                    this.m_currentDepositedObject = null;
                    return toReturn;
                default:
                    return null;
            }
        }

        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable)
        {
            if (this.m_currentDepositedObject == null)
                return this.TryDepositInteractable(interactable);

            ComponentObject co = null;
            if (this.m_currentDepositedObject is ComponentObject dco)
                co = dco;
            else if (interactable is ComponentObject ico)
                co = ico;

            ComponentPackage cp = null;
            if (this.m_currentDepositedObject is ComponentPackage dcp)
                cp = dcp;
            else if (interactable is ComponentPackage icp)
                cp = icp;

            if (cp is not null && co is not null)
            {
                this.AddComponentToComponentPackage(cp, co);
            }

            return null;
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

        private IInteractable TryDepositInteractable(IInteractable interactable)
        {
            var go = interactable switch
            {
                ComponentPackage cp => cp.gameObject,
                ComponentObject co => co.gameObject,
                _ => null
            };

            if (go == null)
                return interactable;

            this.m_currentDepositedObject = interactable;
            go.transform.SetParent(this.m_componentHolderParent);
            go.transform.localPosition = Vector3.zero;
            return null;
        }

        private void AddComponentToComponentPackage(ComponentPackage cp, ComponentObject co)
        {
            cp.AddComponent(co.ComponentData);
            Destroy(co.gameObject);

            if (this.m_currentDepositedObject is not ComponentPackage)
            {
                this.TryDepositInteractable(cp);
            }
        }
    }
}