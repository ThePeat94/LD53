using DefaultNamespace.Order;

namespace EventArgs
{
    public class PackageOrderChangeEventArgs : System.EventArgs
    {
        public PackageOrderChangeEventArgs(PackageOrder packageOrder)
        {
            this.PackageOrder = packageOrder;
        }
        
        public PackageOrder PackageOrder { get; set; }
    }
}