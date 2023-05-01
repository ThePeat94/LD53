using UnityEngine;

namespace Interactable
{
    public class Highlighter : MonoBehaviour
    {
        [SerializeField] private Outline m_outline;
        
        public void Highlight()
        {
            this.m_outline.enabled = true;
        }
        
        public void RemoveHighlight()
        {
            this.m_outline.enabled = false;
        }
    }
}