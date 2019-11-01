// Disable warning for fields never being assigned to. This class is deserialized from
// JSON, and is never otherwise instantiated.
#pragma warning disable CS0649

using System;

namespace Kongregate.Web
{
    [Serializable]
    internal struct UserItemListResponse
    {
        public bool success;
        public UserItem[] data;
    }
}
