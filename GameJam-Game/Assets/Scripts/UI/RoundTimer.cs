using DefaultNamespace.Order;
using EventArgs;
using TMPro;
using UnityEngine;

namespace UI
{
    public class RoundTimer : MonoBehaviour
    {

        private Timer m_timer;
    

        [SerializeField] private TextMeshProUGUI m_mainTextField;
        [SerializeField] private TextMeshProUGUI changeVisualiseField;
        [SerializeField] private int changeFrameCount = 60;
        public Color32 normalFillColor;
        public Color32 warningFillColor;
        public float warningLimit = 20;
        private OrderManager m_orderManager;

        private int m_currentChangeDisplayFrame;
        

        private void Start()
        {
            this.m_mainTextField.color = this.normalFillColor;
        }

        private void FixedUpdate()
        {
            if (this.m_currentChangeDisplayFrame <= 0)
            {
                this.changeVisualiseField.text = "";
            }
            
            this.m_mainTextField.text = (this.m_timer.RemainingFrameTime*Time.fixedDeltaTime).ToString("N0");

            if (this.m_timer.RemainingFrameTime < ((this.warningLimit / 100) * this.m_timer.InitialFrameTime))
            {
                this.m_mainTextField.color = this.warningFillColor;
            }
            this.m_currentChangeDisplayFrame--;
        }

        private void OnOrderExpired(object sender, PackageOrderChangeEventArgs eventArgs)
        {
            this.m_currentChangeDisplayFrame = this.changeFrameCount;
            this.changeVisualiseField.text = (-eventArgs.PackageOrder.OrderData.PunishFrames*Time.fixedDeltaTime).ToString("N0");
            this.changeVisualiseField.color = this.warningFillColor;
        }
        
        private void OnOrderDelivered(object sender, PackageOrderChangeEventArgs eventArgs)
        {
            this.m_currentChangeDisplayFrame = this.changeFrameCount;
            this.changeVisualiseField.text = (eventArgs.PackageOrder.OrderData.RewardFrames*Time.fixedDeltaTime).ToString("+0");
            this.changeVisualiseField.color = this.normalFillColor;
        }

        private void Awake()
        {
            if (this.m_timer is null)
            {
                this.m_timer = FindObjectOfType<Timer>();
            }
            
            if (this.m_orderManager is null)
            {
                this.m_orderManager = FindObjectOfType<OrderManager>();
            }

            this.m_orderManager.OrderExpired += this.OnOrderExpired;
            
            this.m_orderManager.OrderDelivered += this.OnOrderDelivered;
        }
    }
}
