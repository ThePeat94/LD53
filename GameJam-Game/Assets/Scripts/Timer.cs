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

    public int InitalFrameTime => this.m_initalFrameTime;

    public int RemainingFrameTime => this.m_remainingFrameTime;

    private void Start()
    {
        this.m_remainingFrameTime = this.m_initalFrameTime;
    }

    private void FixedUpdate()
    {
        if (this.m_remainingFrameTime == 0)
        {
            return;
        }

        this.m_remainingFrameTime--;

        if (this.m_remainingFrameTime == 0)
        {
            this.m_timeUp?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
