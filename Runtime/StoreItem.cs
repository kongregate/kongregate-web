using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kongregate.Web
{
    [Serializable]
    public struct StoreItem : IEquatable<StoreItem>
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

        public override bool Equals(object obj)
        {
            if (obj is StoreItem item)
            {
                return Equals(item);
            }

            return false;
        }

        public bool Equals(StoreItem other)
        {
            return id == other.id
                && identifier == other.identifier
                && name == other.name
                && description == other.description
                && price == other.price
                && image_url == other.image_url
                && tags.Length == other.tags.Length
                && tags.SequenceEqual(other.tags);
        }

        public override int GetHashCode()
        {
            var hashCode = 316061847;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(identifier);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(description);
            hashCode = hashCode * -1521134295 + price.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(tags);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(image_url);
            return hashCode;
        }
    }
}
