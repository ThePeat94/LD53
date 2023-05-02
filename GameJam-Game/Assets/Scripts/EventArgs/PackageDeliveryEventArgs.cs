using Nidavellir.Interactable;

namespace Nidavellir.EventArgs
{
    public class PackageDeliveryEventArgs : System.EventArgs
    {
        public PackageDeliveryEventArgs(ComponentPackage eventPackage)
        {
            this.EventPackage = eventPackage;
        }

        public ComponentPackage EventPackage { get; }
    }
}