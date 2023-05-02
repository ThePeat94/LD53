using Nidavellir.Order;

namespace Nidavellir.EventArgs
{
    public class PackageOrderChangeEventArgs : System.EventArgs
    {
        public PackageOrderChangeEventArgs(PackageOrder packageOrder)
        {
            this.PackageOrder = packageOrder;
        }
        
        public PackageOrder PackageOrder { get; }
    }
}