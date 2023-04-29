using Scriptables;
using UnityEngine;

namespace DefaultNamespace
{
    public class ComponentObject : MonoBehaviour
    {
        [SerializeField] private ComponentData m_componentData;

        public ComponentData ComponentData => this.m_componentData;
    }
}