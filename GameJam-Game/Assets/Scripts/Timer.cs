using System;
using DefaultNamespace;
using DefaultNamespace.Order;
using EventArgs;
using Scriptables;
using UnityEngine;

public class Timer : MonoBehaviour{
    
    [SerializeField] private LevelData m_levelData;

    private int m_initialFrameTime;
    private EventHandler m_timeUp;
    private int m_remainingFrameTime;
    private OrderManager m_orderManager;
    private GameStateManager m_gameStateManager;

    public event EventHandler TimeUp
    {
        add => this.m_timeUp += value;
        remove => this.m_timeUp -= value;
    }

    public int InitialFrameTime => this.m_initialFrameTime;

    public int RemainingFrameTime => this.m_remainingFrameTime;
    
    private void Awake()
    {
        this.m_initialFrameTime = this.m_levelData.InitialFrameTime;
        if (this.m_orderManager is null)
        {
            this.m_orderManager = FindObjectOfType<OrderManager>();
        }
        if (this.m_gameStateManager is null)
        {
            this.m_gameStateManager = FindObjectOfType<GameStateManager>();
        }
        this.m_orderManager.OrderExpired += this.OnPackageOrderExpired;
        this.m_orderManager.OrderDelivered += this.OnPackageOrderDelivered;
    }
    
    private void Start()
    {
        this.m_remainingFrameTime = this.m_initialFrameTime;
    }

    private void OnPackageOrderExpired(object sender, PackageOrderChangeEventArgs eventArgs)
    {
        Debug.Log("Package Expired");
        this.m_remainingFrameTime -= eventArgs.PackageOrder.OrderData.PunishFrames;
        if (this.m_remainingFrameTime <= 0)
        {
            this.m_timeUp?.Invoke(this, System.EventArgs.Empty);
        }
    }
    
    private void OnPackageOrderDelivered(object sender, PackageOrderChangeEventArgs eventArgs)
    {
        Debug.Log("Package Deliverd");
        this.m_remainingFrameTime += eventArgs.PackageOrder.OrderData.RewardFrames;
    }

    private void FixedUpdate()
    {
        if (m_gameStateManager.CurrentState != GameStateManager.State.Playing)
        {
            return;
        }
        if (this.m_remainingFrameTime <= 0)
        {
            return;
        }

        this.m_remainingFrameTime--;

        if (this.m_remainingFrameTime <= 0)
        {
            this.m_timeUp?.Invoke(this, System.EventArgs.Empty);
        }
    }
    
}
