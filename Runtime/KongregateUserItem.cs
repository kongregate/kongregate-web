namespace Kongregate.Web {
[Serializable]
    public class KongregateUserItem
    {
        [SerializeField]
        private readonly int id;
        public readonly int Id => id;

        [SerializeField]         
        private readonly string identifier;
        public readonly string Identifier => identifier;

        [SerializeField]
        private readonly string data;
        public readonly string Data => data;

        [SerializeField]
        private readonly int remaining_uses;
        public readonly int RemainingUses => remaining_uses;
    }
}
