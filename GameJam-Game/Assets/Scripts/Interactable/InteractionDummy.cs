using UnityEngine;

namespace Interactable
{
    public class InteractionDummy : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Hello world!");
        }

        public void Highlight()
        {
            throw new System.NotImplementedException();
        }

        public void UnHighlight()
        {
            throw new System.NotImplementedException();
        }
    }
}