using Scriptables;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MiniComponentUI : MonoBehaviour
    {
        [SerializeField] private Image m_image;

        public void Init(ComponentData componentData)
        {
            this.m_image.sprite = componentData.Icon;
        }
    }
}