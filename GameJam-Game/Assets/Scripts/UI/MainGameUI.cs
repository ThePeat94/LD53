using UnityEngine;

namespace UI
{
    public class MainGameUI : MonoBehaviour
    {
        [SerializeField] private CurrentOrdersUI m_ordersUI;
        [SerializeField] private GameObject m_gameWonPanel;
        [SerializeField] private GameObject m_gameLostPanel;

        public void ShowGameWonPanel()
        {
            this.m_ordersUI.gameObject.SetActive(false);
            this.m_gameLostPanel.SetActive(false);
            this.m_gameWonPanel.SetActive(true);
        }

        public void ShowGameLostPanel()
        {
            this.m_ordersUI.gameObject.SetActive(false);
            this.m_gameLostPanel.SetActive(true);
            this.m_gameWonPanel.SetActive(false);
        }
    }
}