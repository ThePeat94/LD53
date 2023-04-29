using UnityEngine;
using UnityEngine.UI;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Component Data", menuName = "Data/Component Data", order = 0)]
    public class ComponentData: ScriptableObject
    {
        [SerializeField] private string m_componentName = "NoName";
        [SerializeField] private Sprite m_icon;
        [SerializeField] private GameObject m_model;

        public string ComponentName => this.m_componentName;
        public Sprite Icon => this.m_icon;
        public GameObject Model => this.m_model;
    }
}