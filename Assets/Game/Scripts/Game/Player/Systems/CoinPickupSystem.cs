using Leopotam.EcsLite;

namespace Game
{
    public class CoinPickupSystem : IEcsRunSystem
    {
        private readonly Wallet _wallet;

        public CoinPickupSystem(Wallet wallet) => _wallet = wallet;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<CoinPickupRequest>().End();

            EcsPool<CoinPickupRequest> coinPickupPool = world.GetPool<CoinPickupRequest>();

            EcsPool<HudRefreshRequest> hudRefreshPool = world.GetPool<HudRefreshRequest>();

            foreach (int entity in filter)
            {
                ref CoinPickupRequest coinRequest = ref coinPickupPool.Get(entity);

                _wallet.AddCoin();

                int hudRequest = world.NewEntity();
                hudRefreshPool.Add(hudRequest);

                coinRequest.Coin.PickUp();

                world.DelEntity(entity);
            }
        }
    }
}