using System;

namespace Application
{
    public interface IWalletReadModel
    {
        int Coins { get; }
        int Crystals { get; }

        event Action OnCoinsChanged;
        event Action OnCrystalsChanged;
    }
}
