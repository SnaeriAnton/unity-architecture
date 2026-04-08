using Contracts;
using UniRx;

namespace Game
{
    public class Wallet : IWallet, IWalletWriter, ISpentable
    {
        private IntReactiveProperty _coins;
        private IntReactiveProperty _crystalses;

        public Wallet(int coins, int crystals)
        {
            _coins = new(coins);
            _crystalses = new(crystals);
        }

        public IReadOnlyReactiveProperty<int> Coins => _coins;
        public IReadOnlyReactiveProperty<int> Crystals => _crystalses;

        public void AddCoin()=>_coins.Value++;

        public void AddCrystal()=>
            _crystalses.Value++;

        public bool TrySpendCoin(int amount)
        {
            if (_coins.Value < amount) return false;

            _coins.Value -= amount;
            return true;
        }

        public bool TrySpendCrystal(int amount)
        {
            if (_crystalses.Value < amount) return false;

            _crystalses.Value -= amount;
            return true;
        }

        public void Reset()
        {
            _coins.Value = 0;
            _crystalses.Value = 0;
        }
    }
}