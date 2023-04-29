using UnityEngine;

namespace Interactable
{
    public class InteractionDummy : MonoBehaviour, IInteractable
    {
        public void Interact(InteractingEntity interactingEntity)
        {
            Debug.Log("Hello world!");
        }
    }
}