using System;
using Domain;

namespace Application
{
    public class WalletService
    {
        private readonly Wallet _wallet;

        public event Action<int, int> OnWalletChanged;

        public WalletService(Wallet wallet) => _wallet = wallet;

        public void AddCoin()
        {
            _wallet.AddCoin();
            OnWalletChanged?.Invoke(_wallet.Coins, _wallet.Crystals);
        }

        public void AddCrystal()
        {
            _wallet.AddCrystal();
            OnWalletChanged?.Invoke(_wallet.Coins, _wallet.Crystals);
        }

        public bool TrySpendCrystal(int amount)
        {
            bool result = _wallet.TrySpendCrystal(amount);
            OnWalletChanged?.Invoke(_wallet.Coins, _wallet.Crystals);

            return result;
        }

        public bool TrySpendCoin(int amount)
        {
            bool result = _wallet.TrySpendCoin(amount);
            OnWalletChanged?.Invoke(_wallet.Coins, _wallet.Crystals);

            return result;
        }

        public void Reset()
        {
            _wallet.Reset();
            OnWalletChanged?.Invoke(_wallet.Coins, _wallet.Crystals);
        }
    }
}