using System;
using UnityEngine;

namespace Kongregate.Web
{
    [Serializable]
    public struct StoreItem
    {
        [SerializeField]
        private int id;

        [SerializeField]
        private string identifier;

        [SerializeField]
        private string name;

        [SerializeField]
        private string description;

        [SerializeField]
        private int price;

        [SerializeField]
        private string[] tags;

        [SerializeField]
        private string image_url;

        public int Id => id;

        public string Identifier => identifier;

        public string Name => name;

        public string Description => description;

        public int Price => price;

        public string[] Tags => tags;

        public string ImageUrl => image_url;

        public StoreItem(
            int id,
            string identifier,
            string name,
            string description,
            int price,
            string[] tags,
            string imageUrl)
        {
            this.id = id;
            this.identifier = identifier;
            this.name = name;
            this.description = description;
            this.price = price;
            this.tags = tags;
            this.image_url = imageUrl;
        }
    }
}
