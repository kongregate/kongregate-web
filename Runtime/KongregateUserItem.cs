using Newtonsoft.Json;

namespace Kongregate.Web {
    public class KongregateUserItem
    {
        [JsonProperty("id")]
        public readonly int Id;

        [JsonProperty("identifier")]
        public readonly string Identifier;

        [JsonProperty("data")]
        public readonly string Data;

        [JsonProperty("remaining_uses")]
        public readonly int RemainingUses;
    }
}
