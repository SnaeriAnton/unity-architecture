using Leopotam.EcsLite;

namespace Game
{
    public class PickupApi
    {
        private readonly EcsWorld _world;

        public PickupApi(EcsWorld world) => _world = world;

        public void RequestCoinPickup(Coin coin)
        {
            int request = _world.NewEntity();

            ref CoinPickupRequest data = ref _world.GetPool<CoinPickupRequest>().Add(request);

            data.Coin = coin;
        }

        public void RequestCrystalPickup(Crystal crystal)
        {
            int request = _world.NewEntity();

            ref CrystalPickupRequest data = ref _world.GetPool<CrystalPickupRequest>().Add(request);

            data.Crystal = crystal;
        }
    }
}