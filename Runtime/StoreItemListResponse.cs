using System;

namespace Kongregate.Web
{
	[Serializable]
    public struct StoreItemListResponse
    {
        public bool success;
        public StoreItem[] data;
    }
}
