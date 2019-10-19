using Newtonsoft.Json;

namespace Kongregate.Web {
    public class KongregateStoreItem
    {
        [JsonProperty("id")]
        public readonly int Id;

        [JsonProperty("identifier")]
        public readonly string Identifier;

        [JsonProperty("name")]
        public readonly string Name;

        [JsonProperty("description")]
        public readonly string Description;

        [JsonProperty("price")]
        public readonly int Price;

        [JsonProperty("tags")]
        public readonly string[] Tags;

        [JsonProperty("image_url")]
        public readonly string ImageUrl;
    }
}
