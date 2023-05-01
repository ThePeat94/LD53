using System;
using Input;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameStateManager : MonoBehaviour
    {
        private InputProcessor m_inputProcessor;
        private GameWinner m_gameWinner;
        private MainGameUI m_mainGameUI;

        private void Awake()
        {
            this.m_inputProcessor = this.GetOrAddComponent<InputProcessor>();
            this.m_gameWinner = FindObjectOfType<GameWinner>();
            this.m_mainGameUI = FindObjectOfType<MainGameUI>();
            this.m_gameWinner.GameWon += this.OnGameWon;
        }

        private void OnGameWon(object sender, System.EventArgs e)
        {
            this.m_mainGameUI.ShowGameWonPanel();
        }

        private void Update()
        {
            if (this.m_inputProcessor.RetryTriggered)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }

            if (this.m_inputProcessor.BackToMainTriggered)
            {
                SceneManager.LoadScene(0);
                return;
            }
        }

        public enum State
        {
            Playing,
            Won,
            Lost
        }
    }
}