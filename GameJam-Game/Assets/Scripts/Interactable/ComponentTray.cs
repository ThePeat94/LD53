using Nidavellir.Audio;
using Nidavellir.Scriptables.Audio;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir.Interactable
{
    /// <summary>
    /// An object which can be used to temporarily store components or to be used as an intermediary for combining components
    /// </summary>
    public class ComponentTray : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform m_componentHolderParent;
        [SerializeField] private SfxData m_combineSfxData;
        [SerializeField] private SfxPlayer m_sfxPlayer;
        
        private IInteractable m_currentDepositedObject;

        private void Awake()
        {
            this.m_sfxPlayer = this.GetOrAddComponent<SfxPlayer>();
        }

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

            switch (interactable)
            {
                case ComponentObject when this.m_currentDepositedObject is ComponentObject:
                    return interactable;
                case ComponentPackage when this.m_currentDepositedObject is ComponentPackage:
                    return interactable;
            }

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

            if (cp is null || co is null)
                return null;
            
            if (!cp.ComponentData.IsCompatibleWith(co.ComponentData))
                return interactable;
            
            this.AddComponentToComponentPackage(cp, co);
            return null;
        }

        public bool CanInteractUsingInteractable(IInteractable interactable)
        {
            return interactable is ComponentPackage or ComponentObject;
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
            this.m_sfxPlayer.PlayOneShot(this.m_combineSfxData);

            if (this.m_currentDepositedObject is not ComponentPackage)
            {
                this.TryDepositInteractable(cp);
            }
        }
    }
}