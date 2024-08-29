namespace Nidavellir.Interactable
{

    /// <summary>
    /// Used to enable objects to be interacted with
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Performs an interaction with an object
        /// </summary>
        /// <param name="interactingEntity">The entity which is interacting with the interacted object</param>
        /// <returns>The new interactable</returns>
        public IInteractable Interact(InteractingEntity interactingEntity);
        
        /// <summary>
        /// Performs an interaction with an object with another interactable
        /// </summary>
        /// <param name="interactingEntity">The entity which is interacting with the interacted object</param>
        /// <param name="interactable">The interactable which is used to interact with the interacted object</param>
        /// <returns>The new interactable</returns>
        public IInteractable InteractUsingInteractable(InteractingEntity interactingEntity, IInteractable interactable);
        
        /// <summary>
        /// Determines, if the interactable can be interacted with using another interactable.
        /// Should be used before calling InteractUsingInteractable
        /// </summary>
        /// <param name="interactable">The other interactable which should be checked against</param>
        /// <returns>true, if interaction is possible, false if not</returns>
        public bool CanInteractUsingInteractable(IInteractable interactable);
    }
}