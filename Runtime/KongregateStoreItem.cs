namespace Kongregate.Web {
    [Serializable]
    public class KongregateStoreItem
    {
        public readonly int id;
        public readonly string identifier;
        public readonly string name;
        public readonly string description;
        public readonly int price;
        public readonly string[] tags;
        public readonly string image_url;
    }
}
