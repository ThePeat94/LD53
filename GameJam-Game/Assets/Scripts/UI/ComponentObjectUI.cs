using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class ComponentObjectUI : MonoBehaviour
    {
        [SerializeField] private Image m_componentObjectImage;
        [SerializeField] private ComponentObject m_componentObject;

        private void Awake()
        {
            if (this.m_componentObject == null)
            {
                this.m_componentObject = this.GetComponentInParent<ComponentObject>();
            }
        }
        
        private void Start()
        {
            this.m_componentObjectImage.sprite = this.m_componentObject.ComponentData.Icon;
        }
        
        private void Update()
        {
            this.transform.LookAt(Camera.main.transform);
        }
    }
}