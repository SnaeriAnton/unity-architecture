using System;

namespace Application
{
    public interface IWallet
    {
        public void AddCoin();
        public void AddCrystal();
        public bool TrySpendCoins(int value);
        public bool TrySpendCrystals(int value);
        public void Reset();
    }
}
