namespace Shared
{
    public class GameEvents
    {
        public static readonly WalletEvents Wallet = new();
        public static readonly PlayerEvents Player = new();
        public static readonly ProgressionEvents Progression = new();
        public static readonly GameFlowEvents GameFlow = new();
        public static readonly LootEvents Loot = new();
    }
}
