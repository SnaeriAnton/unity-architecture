using System;
using UnityEngine;
using R3;
using Contracts;

namespace Game
{
    public class PlayerPickupHandler : IPickupReceiver
    {
        private readonly IWalletWriter _wallet;
        private readonly Subject<Unit> _onPickedUpCrystal = new();
        
        public PlayerPickupHandler(IWalletWriter wallet) => _wallet = wallet;
        
        public Observable<Unit> OnPickedUpCrystal => _onPickedUpCrystal;
        
        public void Handle(Collider2D other)
        {
            if (other.TryGetComponent(out IPickup pickup))
                pickup.PickUp(this);
        }

        public void AddCoin() => _wallet.AddCoin();
        public void AddExperience() => _onPickedUpCrystal.OnNext(Unit.Default);
    }
}