using Scriptables;

namespace DefaultNamespace.Order
{
    public class PackageOrder
    {
        public PackageOrder(OrderData orderData)
        {
            this.OrderData = orderData;
            this.CurrentFrameCountdown = orderData.FrameTime;
        }

        public int CurrentFrameCountdown { get; set; }
        public OrderData OrderData { get; set; }
    }
}