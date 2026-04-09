using R3;

namespace Game
{
    public interface IWallet
    {
        public ReadOnlyReactiveProperty<int> Coins { get; }
        public ReadOnlyReactiveProperty<int> Crystals { get; }
    }
}