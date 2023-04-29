using System;
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

        private InputProcessor m_inputProcessor;

        private void Awake()
        {
            this.m_inputProcessor = this.GetOrAddComponent<InputProcessor>();
            this.m_inputProcessor.InteractTriggered += this.OnInteractTriggered;
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
            if (foundInteractable != null)
            {
                Debug.Log($"Interacting with \"{foundInteractable.name}\"");
                foundInteractable.GetComponentInParent<IInteractable>().Interact();
            }
        }
    }
}