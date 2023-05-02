using Nidavellir.EventArgs;
using Nidavellir.Interactable;
using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class ComponentMachineUI : MonoBehaviour
    {
        [SerializeField] private InteractableMachine m_interactableMachine;
        [SerializeField] private Image m_componentImage;
        

        private void Awake()
        {
            if (this.m_interactableMachine == null)
            {
                this.m_interactableMachine = this.GetComponentInParent<InteractableMachine>();
            }
            
            this.m_interactableMachine.ComponentAdded += this.OnComponentAdded;
            this.m_interactableMachine.ComponentConsumed += this.OnComponentConsumed;
        }
        
        private void Update()
        {
            this.transform.LookAt(Camera.main.transform);
        }

        private void OnComponentConsumed(object sender, ComponentAddedEventArgs e)
        {
            this.m_componentImage.gameObject.SetActive(false);
        }

        private void OnComponentAdded(object sender, ComponentAddedEventArgs e)
        {
            this.m_componentImage.gameObject.SetActive(true);
            this.m_componentImage.sprite = e.ComponentData.Icon;
        }
    }
}