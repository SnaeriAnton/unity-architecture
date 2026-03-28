using System;
using Contracts;
using UnityEngine;

namespace Game
{
    public class PlayerPickupHandler : IPickupReceiver
    {
        private readonly IWalletWriter _wallet;
        
        public PlayerPickupHandler(IWalletWriter wallet) => _wallet = wallet;
        
        public event Action OnPickedUpCrystal;
        
        public void Handle(Collider2D other)
        {
            if (other.TryGetComponent(out IPickup pickup))
                pickup.PickUp(this);
        }

        public void AddCoin() => _wallet.AddCoin();
        public void AddExperience() => OnPickedUpCrystal?.Invoke();
    }
}