using UnityEngine;

namespace Game
{
    public class PlayerPickupTrigger : MonoBehaviour
    {
        private PickupApi _pickupApi;

        public void Construct(PickupApi pickupApi) => _pickupApi = pickupApi;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Coin coin))
            {
                _pickupApi.RequestCoinPickup(coin);
                return;
            }

            if (other.TryGetComponent(out Crystal crystal))
                _pickupApi.RequestCrystalPickup(crystal);
        }
    }
}