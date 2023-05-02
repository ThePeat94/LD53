using Nidavellir.Scriptables;
using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.UI.Orders
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