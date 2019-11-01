using System;
using UnityEngine;

namespace Kongregate.Web
{
    /// <summary>
    /// An item in the user's inventory.
    /// </summary>
    [Serializable]
    public struct UserItem
    {
        [SerializeField]
        private int id;

        [SerializeField]
        private string identifier;

        [SerializeField]
        private string data;

        [SerializeField]
        private int remaining_uses;

        public int Id => id;

        public string Identifier => identifier;

        public string Data => data;

        public int RemainingUses => remaining_uses;

        public bool IsUnlimitedUseItem => RemainingUses == 0;

        public UserItem(int id, string identifier, string data, int remainingUses)
        {
            this.id = id;
            this.identifier = identifier;
            this.data = data;
            remaining_uses = remainingUses;
        }
    }
}
