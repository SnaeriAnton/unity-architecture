namespace Game
{
    public class HudRefreshSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;
        private readonly GameUiBridge _uiBridge;

        public HudRefreshSystem(EcsWorld world, CommandBuffer commandBuffer, GameUiBridge uiBridge)
        {
            _world = world;
            _commandBuffer = commandBuffer;
            _uiBridge = uiBridge;
        }

        public void Run(float deltaTime)
        {
            bool needRefresh = false;

            foreach (Entity request in _world.Filter().With<HudRefreshRequest>())
            {
                needRefresh = true;
                _commandBuffer.DestroyEntity(request);
            }

            if (needRefresh) _uiBridge.RefreshHud();
        }
    }
}