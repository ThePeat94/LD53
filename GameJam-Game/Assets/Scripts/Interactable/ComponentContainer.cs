using System;
using Audio;
using Scriptables;
using Scriptables.Audio;
using Unity.VisualScripting;
using UnityEngine;

namespace Interactable
{
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
            return interactable;
        }

        public bool CanInteractUsingInteractable(IInteractable interactable)
        {
            return false;
        }

        public void Activate()
        {
            this.m_collider.enabled = true;
        }

        public void Deactivate()
        {
            this.m_collider.enabled = false;
        }
    }
}