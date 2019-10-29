namespace Kongregate.Web {
    [Serializable]
    public class KongregateStoreItem
    {
        [SerializeField]
        private readonly int id;
        public readonly int Id => id;

        [SerializeField]
        private readonly string identifier;
        public readonly string Identifier => identifier;

        [SerializeField]
        private readonly string name;
        public readonly string Name => name;

        [SerializeField]
        private readonly string description;
        public readonly string Description => description;

        [SerializeField]
        private readonly int price;
        public readonly int Price => price;

        [SerializeField]
        private readonly string[] tags;
        public readonly string[] Tags => tags;

        [SerializeField]
        private readonly string image_url;
        public readonly string ImageUrl => image_url;
    }
}
