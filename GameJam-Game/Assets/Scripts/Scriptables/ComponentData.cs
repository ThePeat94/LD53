using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "Component Data", menuName = "Data/Component", order = 0)]
    public class ComponentData: ScriptableObject
    {
        [SerializeField] private string m_componentName = "NoName";
        [SerializeField] private Sprite m_icon;
        [SerializeField] private GameObject m_model;
        [SerializeField] private List<ComponentData> m_compatibleComponents;
        

        public string ComponentName => this.m_componentName;
        public Sprite Icon => this.m_icon;
        public GameObject Model => this.m_model;

        public bool IsCompatibleWith(ComponentData componentData)
        {
            return this.m_compatibleComponents.Contains(componentData);
        }
    }
}