using System;
using UnityEngine;

namespace Kongregate.Web
{
    [Serializable]
    public struct UserItem
    {
        [SerializeField]
        private readonly int id;

        [SerializeField]
        private readonly string identifier;

        [SerializeField]
        private readonly string data;

        [SerializeField]
        private readonly int remaining_uses;

        public int Id => id;

        public string Identifier => identifier;

        public string Data => data;

        public int RemainingUses => remaining_uses;
    }
}
