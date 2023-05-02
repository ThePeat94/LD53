using System.Linq;
using Nidavellir.Input;
using Nidavellir.Interactable;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir.Player
{
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField] private Collider m_interactionCollider;
        [SerializeField] private GameStateManager m_gameStateManager;
        

        private InteractingEntity m_interactingEntity;
        private InputProcessor m_inputProcessor;

        private IInteractable m_currentInteractable;
        private Highlighter m_currentInteractableHighlight;

        private void Awake()
        {
            this.m_inputProcessor = this.GetOrAddComponent<InputProcessor>();
            this.m_inputProcessor.InteractTriggered += this.OnInteractTriggered;

            this.m_interactingEntity = this.GetComponent<InteractingEntity>();
            if (this.m_gameStateManager == null)
            {
                this.m_gameStateManager = FindObjectOfType<GameStateManager>();
            }
        }

        private void FixedUpdate()
        {
            if (this.m_gameStateManager.CurrentState != GameStateManager.State.Playing)
                return;
            
            var foundOverlapped = this.FindOverlappedObjectsByInteractionCollider();
            var foundInteractable = foundOverlapped.FirstOrDefault(c => c.GetComponentInParent<Highlighter>() != null);
            var previous = this.m_currentInteractableHighlight;
            this.m_currentInteractableHighlight = foundInteractable?.GetComponentInParent<Highlighter>();
            this.ApplyHighlights(previous, this.m_currentInteractableHighlight);
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
            var overlappedByCollider = this.FindOverlappedObjectsByInteractionCollider();
            
            var foundInteractable = overlappedByCollider.FirstOrDefault(c => c.GetComponentInParent<IInteractable>() != null);
            if (foundInteractable is null)
            {
                return;
            }
            
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

        private Collider[] FindOverlappedObjectsByInteractionCollider()
        {
            return Physics.OverlapBox(this.m_interactionCollider.bounds.center, this.m_interactionCollider.bounds.extents, this.m_interactionCollider.transform.rotation);
        }

        private void ApplyHighlights(Highlighter previous, Highlighter current)
        {
            if (previous == current)
                return;

            if (previous is not null && current is not null)
            {
                previous.RemoveHighlight();
                current.Highlight();
                return;
            }

            if (previous is null && current is not null)
            {
                current.Highlight();
                return;
            }

            if (previous is not null && current is null)
            {
                previous.RemoveHighlight();
                return;
            }
        }
    }
}