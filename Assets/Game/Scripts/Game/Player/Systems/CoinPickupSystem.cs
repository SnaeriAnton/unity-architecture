namespace Game
{
    public class CoinPickupSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly Wallet _wallet;
        private readonly CommandBuffer _commandBuffer;

        public CoinPickupSystem(EcsWorld world, Wallet wallet, CommandBuffer commandBuffer)
        {
            _world = world;
            _wallet = wallet;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity entity in _world.Filter().With<CoinPickupRequest>())
            {
                CoinPickupRequest coinRequest = _world.Get<CoinPickupRequest>(entity);

                _wallet.AddCoin();
                Entity request = _commandBuffer.CreateEntity();
                _commandBuffer.Add(request, new HudRefreshRequest());
                coinRequest.Coin.PickUp();
                _commandBuffer.DestroyEntity(entity);

            }
        }
    }
}