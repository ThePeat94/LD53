using Nidavellir.Order;

namespace Nidavellir.EventArgs
{
    public class PackageOrderChangeEventArgs : System.EventArgs
    {
        public PackageOrderChangeEventArgs(PackageOrder packageOrder)
        {
            this.PackageOrder = packageOrder;
        }

        public PackageOrderChangeEventArgs(PackageOrder packageOrder, bool isFirstOrder)
        {
            this.PackageOrder = packageOrder;
            this.IsFirstOrder = isFirstOrder;
        }

        public PackageOrder PackageOrder { get; }
        
        public bool IsFirstOrder { get; }
    }
}