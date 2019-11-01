using System;
using UnityEngine;

namespace Kongregate.Web
{
    [Serializable]
    public struct StoreItem
    {
        [SerializeField]
        private readonly int id;

        [SerializeField]
        private readonly string identifier;

        [SerializeField]
        private readonly string name;

        [SerializeField]
        private readonly string description;

        [SerializeField]
        private readonly int price;

        [SerializeField]
        private readonly string[] tags;

        [SerializeField]
        private readonly string image_url;

        public int Id => id;

        public string Identifier => identifier;

        public string Name => name;

        public string Description => description;

        public int Price => price;

        public string[] Tags => tags;

        public string ImageUrl => image_url;
    }
}
