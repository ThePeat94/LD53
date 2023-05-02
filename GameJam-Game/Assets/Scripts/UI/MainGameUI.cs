using Nidavellir.UI.Orders;
using UnityEngine;

namespace Nidavellir.UI
{
    public class MainGameUI : MonoBehaviour
    {
        [SerializeField] private CurrentOrdersUI m_ordersUI;
        [SerializeField] private GameObject m_gameWonPanel;
        [SerializeField] private GameObject m_gameLostPanel;
        [SerializeField] private GameObject m_instructionsPanel;


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

        public void ShowInstructionsPanel()
        {
            this.m_instructionsPanel.SetActive(true);
            this.m_ordersUI.gameObject.SetActive(false);
        }

        public void HideInstructionsPanel()
        {
            this.m_instructionsPanel.SetActive(false);
            this.m_ordersUI.gameObject.SetActive(true);
        }
    }
}