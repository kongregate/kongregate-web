using System;

namespace Kongregate.Web
{
    [Serializable]
	internal struct PurchaseItemsResponse
    {
        public bool success;
        public string[] items;
    }
}
