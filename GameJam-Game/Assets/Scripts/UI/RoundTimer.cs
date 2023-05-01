using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerCount : MonoBehaviour
{

    private Timer m_timer;
    

    private TextMeshProUGUI m_cock;
    public Color32 normalFillColor;
    public Color32 warningFillColor;
    public float warningLimit = 20;

    private void Start()
    {
        
        m_cock = GetComponent<TextMeshProUGUI>();
        
        m_cock.color = normalFillColor;
    }

    private void FixedUpdate()
    {
        m_cock.text = (m_timer.RemainingFrameTime*Time.fixedDeltaTime).ToString("N0");

        if (m_timer.RemainingFrameTime < ((warningLimit / 100) * m_timer.InitialFrameTime))
        {
            m_cock.color = warningFillColor;
        }
    }
    private void Awake()
    {
        if (this.m_timer is null)
        {
            this.m_timer = FindObjectOfType<Timer>();
        }

        
    }
}
