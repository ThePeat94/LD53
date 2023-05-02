using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nidavellir.Input
{
    public class InputProcessor : MonoBehaviour
    {

        private EventHandler m_interactTriggered;
        
        private PlayerInput m_playerInput;

        public Vector2 Movement { get; private set; }

        public bool QuitTriggered => this.m_playerInput.Actions.Quit.triggered;
        public bool BackToMainTriggered => this.m_playerInput.Actions.BackToMenu.triggered;
        public bool RetryTriggered => this.m_playerInput.Actions.Retry.triggered;
        public bool ConfirmInstructionsTriggered => this.m_playerInput.Actions.ConfirmInstructions.triggered;

        public bool IsBoosting { get; private set; }

        public event EventHandler InteractTriggered
        {
            add => this.m_interactTriggered += value;
            remove => this.m_interactTriggered -= value;
        }

        private void Awake()
        {
            this.m_playerInput = new PlayerInput();
            this.m_playerInput.Actions.Boost.started += this.OnBoostStarted;
            this.m_playerInput.Actions.Boost.canceled += this.OnBoostEnded;
            this.m_playerInput.Actions.Interact.performed += this.OnInteractPerformed;
        }
        
        private void Update()
        {
            this.Movement = this.m_playerInput.Actions.Move.ReadValue<Vector2>();
        }

        private void OnEnable()
        {
            this.m_playerInput?.Enable();
        }

        private void OnDisable()
        {
            this.m_playerInput?.Disable();
            this.Movement = Vector3.zero;
        }

        private void OnBoostEnded(InputAction.CallbackContext ctx)
        {
            this.IsBoosting = false;
        }

        private void OnBoostStarted(InputAction.CallbackContext ctx)
        {
            this.IsBoosting = true;
        }
        
        private void OnInteractPerformed(InputAction.CallbackContext obj)
        {
            this.m_interactTriggered?.Invoke(this, System.EventArgs.Empty);
        }

    }
}