using Contracts;
using R3;

namespace Game
{
    public class Wallet : IWallet, IWalletWriter, ISpentable
    {
        private ReactiveProperty<int> _coins;
        private ReactiveProperty<int> _crystalses;

        public Wallet(int coins, int crystals)
        {
            _coins = new(coins);
            _crystalses = new(crystals);
            
            _coins = new(100);
            _crystalses = new(100);
        }

        public ReadOnlyReactiveProperty<int> Coins => _coins;
        public ReadOnlyReactiveProperty<int> Crystals => _crystalses;

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
            
            _coins.Value = 100;
            _crystalses.Value = 100;
        }
    }
}