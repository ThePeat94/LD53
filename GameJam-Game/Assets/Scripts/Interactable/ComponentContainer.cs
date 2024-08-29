using Nidavellir.Audio;
using Nidavellir.Scriptables;
using Nidavellir.Scriptables.Audio;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir.Interactable
{
    /// <summary>
    /// A container, which can be used to generate other components (e. g. a table which generates paper)
    /// </summary>
    public class ComponentContainer : MonoBehaviour, IInteractable
    {
        [SerializeField] private ComponentData m_containedComponent;
        [SerializeField] private Collider m_collider;
        [SerializeField] private SfxData m_takeSfxData;
        [SerializeField] private SfxPlayer m_sfxPlayer;
        
        private void Awake()
        {
            if (this.m_collider is null)
                this.m_collider = this.GetComponentInChildren<Collider>();

            this.m_sfxPlayer = this.GetOrAddComponent<SfxPlayer>();
        }

        public IInteractable Interact(InteractingEntity interactingEntity)
        {
            this.m_sfxPlayer.PlayOneShot(this.m_takeSfxData);
            return Instantiate(this.m_containedComponent.Model, interactingEntity.ComponentHolder).GetComponent<IInteractable>();
        }

        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable)
        {
            if (interactable is ComponentPackage || interactable is ComponentObject)
            {
                Destroy(interactingEntity.ComponentHolder.GetChild(0).gameObject);
                return null;
            }
            return interactable;
        }

        public bool CanInteractUsingInteractable(IInteractable interactable)
        {
            return (interactable is ComponentPackage componentPackage &&
                    componentPackage.ComponentData == this.m_containedComponent) ||
                   (interactable is ComponentObject co &&
                    co.ComponentData == this.m_containedComponent);
        }
    }
}