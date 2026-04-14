namespace Game
{
    public class WalletSettings
    {
        public int StartCoins { get; }
        public int StartCrystals { get; }

        public WalletSettings(int startCoins, int startCrystals)
        {
            StartCoins = startCoins;
            StartCrystals = startCrystals;
        }
    }
}