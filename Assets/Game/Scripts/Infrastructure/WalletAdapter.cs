using System;
using Application;
using Domain;

namespace Infrastructure
{
    public class WalletAdapter : IWallet, IWalletReadModel
    {
        private readonly Wallet _wallet;

        public WalletAdapter(Wallet wallet) => _wallet = wallet;

        public int Coins => _wallet.Coins;
        public int Crystals => _wallet.Crystals;

        public event Action OnCoinsChanged;
        public event Action OnCrystalsChanged;

        public void AddCoin()
        {
            _wallet.AddCoin();
            OnCoinsChanged?.Invoke();
        }

        public void AddCrystal()
        {
            _wallet.AddCrystal();
            OnCrystalsChanged?.Invoke();
        }

        public bool TrySpendCoins(int value)
        {
            bool result = _wallet.TrySpendCoin(value);
            if (result) OnCoinsChanged?.Invoke();
            return result;
        }

        public bool TrySpendCrystals(int value)
        {
            bool result = _wallet.TrySpendCrystal(value);
            if (result) OnCrystalsChanged?.Invoke();
            return result;
        }

        public void Reset() => _wallet.Reset();
    }
}