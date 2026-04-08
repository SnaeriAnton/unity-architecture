using UniRx;

namespace Game
{
    public interface IWallet
    {
        public IReadOnlyReactiveProperty<int> Coins { get; }
        public IReadOnlyReactiveProperty<int> Crystals { get; }
    }
}