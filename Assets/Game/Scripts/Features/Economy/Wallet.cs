using Shared;

namespace Economy
{
    public class Wallet : IWallet
    {
        private int _coins;
        private int _crystals;

        public Wallet(int coins, int crystals)
        {
            _coins = coins;
            _crystals = crystals;
            _coins = 100;
            _crystals = 100;
        }

        public void Init()
        {
            GameEvents.Wallet.RaiseCoinsChanged(_coins);
            GameEvents.Wallet.RaiseCrystalsChanged(_crystals);
        }

        public void AddCoin()
        {
            _coins++;
            GameEvents.Wallet.RaiseCoinsChanged(_coins);
        }

        public void AddCrystal()
        {
            _crystals++;
            GameEvents.Wallet.RaiseCrystalsChanged(_crystals);
        }

        public bool TrySpendCoin(int amount)
        {
            if (_coins < amount) return false;

            _coins -= amount;
            GameEvents.Wallet.RaiseCoinsChanged(_coins);
            return true;
        }

        public bool TrySpendCrystal(int amount)
        {
            if (_crystals < amount) return false;

            _crystals -= amount;
            GameEvents.Wallet.RaiseCrystalsChanged(_crystals);
            return true;
        }

        public void Reset()
        {
            _coins = 0;
            _crystals = 0;
            GameEvents.Wallet.RaiseCoinsChanged(_coins);
            GameEvents.Wallet.RaiseCrystalsChanged(_crystals);
        }
    }
}