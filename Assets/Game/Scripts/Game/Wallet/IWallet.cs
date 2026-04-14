using System;

namespace Game
{
    public interface IWallet
    {
        public int Coins { get; }
        public int Crystals { get; }
        
        
        public event Action OnChanged;
    }
}