using System;
using Contracts;

namespace Game
{
    public class Wallet : IWallet, IWalletWriter, ISpentable
    {
        public int Coins { get; private set; }
        public int Crystals { get; private set; }
        
        public Wallet(int coins, int crystals)
        {
            Coins = coins;
            Crystals = crystals;
        }
        
        public event Action OnChanged;

        public void AddCoin()
        {
            Coins++;
            OnChanged?.Invoke();
        }

        public void AddCrystal()
        {
            Crystals++;
            OnChanged?.Invoke();
        }

        public bool TrySpendCoin(int amount)
        {
            if (Coins < amount) return false;

            Coins -= amount;
            OnChanged?.Invoke();
            return true;
        }

        public bool TrySpendCrystal(int amount)
        {
            if (Crystals < amount) return false;

            Crystals -= amount;
            OnChanged?.Invoke();
            return true;
        }

        public void Reset()
        {
            Coins = 0;
            Crystals = 0;
            
            OnChanged?.Invoke();
        }
    }
}