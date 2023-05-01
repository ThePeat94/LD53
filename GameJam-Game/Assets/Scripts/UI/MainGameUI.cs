﻿using System;
using Audio;
using Scriptables.Audio;
using UnityEngine;

namespace UI
{
    public class MainGameUI : MonoBehaviour
    {
        [SerializeField] private CurrentOrdersUI m_ordersUI;
        [SerializeField] private GameObject m_gameWonPanel;
        [SerializeField] private GameObject m_gameLostPanel;
        [SerializeField] private GameObject m_instructionsPanel;
        [SerializeField] private SfxData loosingSoundData;
        private SfxPlayer m_sfxPlayer;

        private void Awake()
        {
            if (this.m_sfxPlayer is null)
            {
                this.m_sfxPlayer = FindObjectOfType<SfxPlayer>();
            } 
        }


        public void ShowGameWonPanel()
        {
            this.m_ordersUI.gameObject.SetActive(false);
            this.m_gameLostPanel.SetActive(false);
            this.m_gameWonPanel.SetActive(true);
        }

        public void ShowGameLostPanel()
        {
            this.m_sfxPlayer.PlayOneShot(this.loosingSoundData);
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