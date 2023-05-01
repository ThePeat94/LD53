using System;
using DefaultNamespace.Order;
using EventArgs;
using UnityEngine;

public class Timer : MonoBehaviour{
    
    [SerializeField] private int m_initalFrameTime;
    private EventHandler m_timeUp;
    private int m_remainingFrameTime;
    private OrderManager m_orderManager;

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

    private void Awake()
    {
        if (this.m_orderManager is null)
        {
            this.m_orderManager = FindObjectOfType<OrderManager>();
        }

        this.m_orderManager.OrderExpired += this.OnPackageOrderExpired;
        this.m_orderManager.OrderDelivered += this.OnPackageOrderDelivered;
    }

    private void OnPackageOrderExpired(object sender, PackageOrderChangeEventArgs eventArgs)
    {
        Debug.Log("Package Expired");
        this.m_remainingFrameTime -= eventArgs.PackageOrder.OrderData.PunishFrames;
    }
    
    private void OnPackageOrderDelivered(object sender, PackageOrderChangeEventArgs eventArgs)
    {
        Debug.Log("Package Expired");
        this.m_remainingFrameTime += eventArgs.PackageOrder.OrderData.RewardFrames;
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
