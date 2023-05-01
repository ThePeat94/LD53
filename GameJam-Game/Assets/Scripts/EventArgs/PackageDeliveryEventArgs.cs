using Interactable;
using UnityEngine.PlayerLoop;

namespace EventArgs
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