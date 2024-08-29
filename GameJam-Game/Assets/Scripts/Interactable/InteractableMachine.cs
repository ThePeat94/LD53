using System;
using Nidavellir.Audio;
using Nidavellir.EventArgs;
using Nidavellir.Scriptables;
using Nidavellir.Scriptables.Audio;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir.Interactable
{
    /// <summary>
    /// An object to combine a component object with a component package
    /// </summary>
    public class InteractableMachine : MonoBehaviour, IInteractable
    {
        [SerializeField] private ComponentData m_needsComponent;
        [SerializeField] private Transform m_componentPackagePlace;
        [SerializeField] private SfxData m_usageSfxData;
        [SerializeField] private SfxData m_noComponentPackageSfxData;
        [SerializeField] private SfxData m_noComponentObjectSfxData;
        [SerializeField] private SfxPlayer m_sfxPlayer;

        private EventHandler<ComponentAddedEventArgs> m_componentAdded;
        private EventHandler<ComponentAddedEventArgs> m_componentConsumed;
        
        private Animator m_animator;

        private ComponentData m_currentComponentData;
        private ComponentPackage m_currentComponentPackage; 
        
        public event EventHandler<ComponentAddedEventArgs> ComponentAdded
        {
            add => this.m_componentAdded += value;
            remove => this.m_componentAdded -= value;
        } 

        public event EventHandler<ComponentAddedEventArgs> ComponentConsumed
        {
            add => this.m_componentConsumed += value;
            remove => this.m_componentConsumed -= value;
        }

        private void Awake()
        {
            this.m_sfxPlayer = this.GetOrAddComponent<SfxPlayer>();
        }

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
            
            if (this.m_currentComponentData is null)
            {
                Debug.Log("Empty machine, nothing to do.");
                this.m_sfxPlayer.PlayOneShot(this.m_noComponentObjectSfxData);
                return null;
            }

            if (this.m_currentComponentPackage is null)
            {
                Debug.Log("No component package");
                this.m_sfxPlayer.PlayOneShot(this.m_noComponentPackageSfxData);
                return null;
            }
            
            Debug.Log("Applying component");
            this.m_currentComponentPackage.AddComponent(this.m_currentComponentData);
            var consumed = this.m_currentComponentData;
            this.m_currentComponentData = null;
            this.m_componentConsumed?.Invoke(this, new(consumed));
            this.m_sfxPlayer.PlayOneShot(this.m_usageSfxData);
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

                if (this.m_currentComponentData != null)
                {
                    Debug.Log("Already has component object.");
                    return interactable;
                }
                
                Debug.Log("Inserting Component object");
                this.m_currentComponentData = co.ComponentData;
                Destroy(co.gameObject);
                this.m_componentAdded?.Invoke(this, new(co.ComponentData));
                return null;
            }
            
            if (interactable is ComponentPackage cp)
            {
                Debug.Log("Inserting Component package");
                this.m_currentComponentPackage = cp;
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
    }
}