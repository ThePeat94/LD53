using Interactable;
using UnityEngine.PlayerLoop;

namespace EventArgs
{
    public class PackageDeliveryEventArgs : System.EventArgs
    {
        public readonly ComponentPackage EventPackage;
        
        public PackageDeliveryEventArgs(ComponentPackage eventPackage)
        {
            this.EventPackage = eventPackage;
        }
    }
}