using System;

namespace Shared
{
    public class WalletEvents
    {
        public event Action<int> CoinsChanged;
        public event Action<int> CrystalsChanged;
        
        public void RaiseCoinsChanged(int coins) => CoinsChanged?.Invoke(coins);
        public void RaiseCrystalsChanged(int crystals) => CrystalsChanged?.Invoke(crystals);
    }
}
