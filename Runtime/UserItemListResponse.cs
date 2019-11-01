using System;

namespace Kongregate.Web
{
    [Serializable]
    public struct UserItemListResponse
    {
        public bool success;
        public UserItem[] data;
    }
}
