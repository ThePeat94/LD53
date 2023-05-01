using System;
using Audio;
using Input;
using Scriptables.Audio;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField] private bool m_hasNewInstructions = true;
        [SerializeField] private SfxPlayer m_sfxPlayer;
        [SerializeField] private SfxData m_instructionsSfx;
        [SerializeField] private SfxData m_gameWonSfxData;
        [SerializeField] private SfxData m_gameWonVoiceSfxData;
        [SerializeField] private SfxData m_gameLostSfxData;
        [SerializeField] private SfxData m_gameLostVoiceSfxData;
        

        private State m_currentState;

        private InputProcessor m_inputProcessor;
        private GameWinner m_gameWinner;
        private MainGameUI m_mainGameUI;
        private Timer m_timer;

        public State CurrentState => this.m_currentState;

        private void Awake()
        {
            this.m_inputProcessor = this.GetOrAddComponent<InputProcessor>();
            this.m_gameWinner = FindObjectOfType<GameWinner>();
            this.m_mainGameUI = FindObjectOfType<MainGameUI>();
            this.m_timer = FindObjectOfType<Timer>();
            this.m_gameWinner.GameWon += this.OnGameWon;
            this.m_timer.TimeUp += this.OnTimeUp;
            
            if(this.m_sfxPlayer == null)
                this.m_sfxPlayer = this.GetOrAddComponent<SfxPlayer>();
        }

        private void Start()
        {
            this.m_currentState = this.m_hasNewInstructions ? State.ShowInstructions : State.Playing;
            if (this.m_hasNewInstructions)
            {
                this.m_mainGameUI.ShowInstructionsPanel();
                if (this.m_sfxPlayer != null)
                    this.m_sfxPlayer.PlayOneShot(this.m_instructionsSfx);
            }
        }

        private void OnTimeUp(object sender, System.EventArgs e)
        {
            this.m_currentState = State.Lost;
            this.m_mainGameUI.ShowGameLostPanel();
            this.m_sfxPlayer.PlayOneShot(this.m_gameLostSfxData);
            this.m_sfxPlayer.PlayOneShot(this.m_gameLostVoiceSfxData);
        }

        private void OnGameWon(object sender, System.EventArgs e)
        {
            this.m_currentState = State.Won;
            this.m_mainGameUI.ShowGameWonPanel();
            this.m_sfxPlayer.PlayOneShot(this.m_gameWonSfxData);
            this.m_sfxPlayer.PlayOneShot(this.m_gameWonVoiceSfxData);
        }

        private void Update()
        {
            if (this.m_inputProcessor.QuitTriggered)
            {
                if (Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    SceneManager.LoadScene(0);
                    return;
                }

                Application.Quit();
                return;
            }

            if (this.m_inputProcessor.RetryTriggered)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene()
                    .buildIndex);
                return;
            }

            if (this.m_inputProcessor.BackToMainTriggered)
            {
                SceneManager.LoadScene(0);
                return;
            }

            if (this.m_currentState == State.ShowInstructions)
            {
                if (this.m_inputProcessor.ConfirmInstructionsTriggered)
                {
                    this.m_currentState = State.Playing;
                    this.m_mainGameUI.HideInstructionsPanel();
                    if (this.m_instructionsSfx != null)
                        this.m_sfxPlayer.MuteAll();
                }
            }

            if (this.m_currentState == State.Won)
            {
                if (this.m_inputProcessor.ConfirmInstructionsTriggered)
                {
                    var nextSceneIndex = SceneManager.GetActiveScene()
                        .buildIndex + 1;
                    if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
                    {
                        SceneManager.LoadScene(0);
                        return;
                    }

                    SceneManager.LoadScene(nextSceneIndex);
                }
            }
        }

        public enum State
        {
            ShowInstructions,
            Playing,
            Won,
            Lost
        }
    }
}