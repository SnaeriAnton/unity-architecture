namespace Game
{
    public class AxeDespawnSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public AxeDespawnSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity requestEntity in _world.Filter().With<AxeDespawnRequest>())
            {
                AxeDespawnRequest request = _world.Get<AxeDespawnRequest>(requestEntity);
                AxeEcsLink link = request.Axe.GetComponent<AxeEcsLink>();

                if (link.IsRegistered)
                {
                    _commandBuffer.DestroyEntity(link.Entity);
                    link.Clear();
                }

                request.Axe.DespawnByEcs();
                _commandBuffer.DestroyEntity(requestEntity);
            }
        }
    }
}