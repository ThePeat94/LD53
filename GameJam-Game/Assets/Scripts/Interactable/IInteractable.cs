namespace Nidavellir.Interactable
{
    public interface IInteractable
    {
        public IInteractable Interact(InteractingEntity interactingEntity);
        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable);
        public bool CanInteractUsingInteractable(IInteractable interactable);
        public void Activate();
        public void Deactivate();
    }
}