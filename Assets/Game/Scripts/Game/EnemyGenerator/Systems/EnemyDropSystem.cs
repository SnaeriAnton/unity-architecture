namespace Game
{
    public class EnemyDropSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EnemyDeathHandler _deathHandler;
        private readonly CommandBuffer _commandBuffer;

        public EnemyDropSystem(EcsWorld world, EnemyDeathHandler deathHandler, CommandBuffer commandBuffer)
        {
            _world = world;
            _deathHandler = deathHandler;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity requestEntity in _world.Filter().With<EnemyDropRequest>())
            {
                EnemyDropRequest request = _world.Get<EnemyDropRequest>(requestEntity);
                _deathHandler.DropLoot(request.Position);
                _commandBuffer.DestroyEntity(requestEntity);
            }
        }
    }
}