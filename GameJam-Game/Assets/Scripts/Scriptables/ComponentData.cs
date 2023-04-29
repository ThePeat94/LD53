using UnityEngine;
using UnityEngine.UI;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Component Data", menuName = "Data/Player Data", order = 0)]
    public class ComponentData: ScriptableObject
    {
        [SerializeField] public string componentName = "NoName";
        [SerializeField] public RawImage icon;
    }
}