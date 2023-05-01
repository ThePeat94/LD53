using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{
    
    [SerializeField] private int m_initalFrameTime;
    private EventHandler m_timeUp;
    private int m_remainingFrameTime;

    public event EventHandler TimeUp
    {
        add => this.m_timeUp += value;
        remove => this.m_timeUp -= value;
    }

    public int InitalFrameTime => m_initalFrameTime;

    public int RemainingFrameTime => m_remainingFrameTime;

    private void Start()
    {
        m_remainingFrameTime = m_initalFrameTime;
    }

    private void FixedUpdate()
    {
        if (m_remainingFrameTime == 0)
        {
            return;
        }
        m_remainingFrameTime--;

        if (m_remainingFrameTime == 0)
        {
            m_timeUp?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
