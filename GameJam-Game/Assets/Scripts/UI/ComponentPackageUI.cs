using System;
using EventArgs;
using Interactable;
using UnityEngine;

namespace UI
{
    public class ComponentPackageUI : MonoBehaviour
    {
        [SerializeField] private ComponentPackage m_componentPackage;
        [SerializeField] private Transform m_componentUiParent;
        [SerializeField] private MiniComponentUI m_miniComponentUiPrefab;
        
        
        
        private void Awake()
        {
            if (this.m_componentPackage == null)
            {
                this.m_componentPackage = this.GetComponentInParent<ComponentPackage>();
            }
            this.m_componentPackage.ComponentAdded += this.OnComponentAdded;
        }

        private void Update()
        {
            this.transform.LookAt(Camera.main.transform);
        }

        private void OnComponentAdded(object sender, ComponentAddedEventArgs e)
        {
            var instantiated = Instantiate(this.m_miniComponentUiPrefab, this.m_componentUiParent);
            instantiated.Init(e.ComponentData);
        }
    }
}