﻿using System;
using System.Linq;
using Input;
using Interactable;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField] private Collider m_interactionCollider;

        private InteractingEntity m_interactingEntity;
        private InputProcessor m_inputProcessor;

        private IInteractable m_currentInteractable;

        private void Awake()
        {
            this.m_inputProcessor = this.GetOrAddComponent<InputProcessor>();
            this.m_inputProcessor.InteractTriggered += this.OnInteractTriggered;

            this.m_interactingEntity = this.GetComponent<InteractingEntity>();
        }

        private void OnDestroy()
        {
            this.m_inputProcessor.InteractTriggered -= this.OnInteractTriggered;
        }

        private void OnInteractTriggered(object sender, System.EventArgs e)
        {
            this.CheckForInteractable();
        }

        private void CheckForInteractable()
        {
            var bounds = this.m_interactionCollider.bounds;
            var overlappedByCollider = Physics.OverlapBox(bounds.center, bounds.extents, this.m_interactionCollider.transform.rotation);
            
            var foundInteractable = overlappedByCollider.FirstOrDefault(c => c.GetComponentInParent<IInteractable>() != null);
            if (foundInteractable is null)
                return;
            
            var interactableTarget = foundInteractable.GetComponentInParent<IInteractable>();
            if (this.m_currentInteractable is not null)
            {
                if (interactableTarget.CanInteractUsingInteractable(this.m_currentInteractable))
                {
                    Debug.Log($"Interacting with \"{foundInteractable.name}\" using \"{this.m_currentInteractable}\"");
                    this.m_currentInteractable = interactableTarget.InteractUsingInteractable(this.m_interactingEntity, this.m_currentInteractable);
                }
                return;
            }
            Debug.Log($"Interacting with \"{foundInteractable.name}\"");
            this.m_currentInteractable = interactableTarget.Interact(this.m_interactingEntity);
        }
    }
}