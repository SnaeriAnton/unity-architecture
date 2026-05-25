namespace Game
{
    public class PickupApi
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public PickupApi(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void RequestCoinPickup(Coin coin)
        {
            Entity request = _commandBuffer.CreateEntity();

            _commandBuffer.Add(request, new CoinPickupRequest
            {
                Coin = coin
            });
        }

        public void RequestCrystalPickup(Crystal crystal)
        {
            Entity request = _commandBuffer.CreateEntity();

            _commandBuffer.Add(request, new CrystalPickupRequest
            {
                Crystal = crystal
            });
        }
    }
}