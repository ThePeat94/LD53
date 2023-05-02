using UnityEngine;

namespace Nidavellir.Interactable
{
    public class InteractionDummy : MonoBehaviour, IInteractable
    {
        public IInteractable Interact(InteractingEntity interactingEntity)
        {
            Debug.Log("Hello world!");
            return null;
        }

        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable)
        {
            throw new System.NotImplementedException();
        }

        public bool CanInteractUsingInteractable(IInteractable interactable)
        {
            throw new System.NotImplementedException();
        }

        public void Activate()
        {
        }

        public void Deactivate()
        {
        }
    }
}