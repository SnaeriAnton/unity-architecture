namespace Game
{
    public class EnemyDespawnSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EnemyDeathHandler _deathHandler;
        private readonly CommandBuffer _commandBuffer;

        public EnemyDespawnSystem(EcsWorld world, EnemyDeathHandler deathHandler, CommandBuffer commandBuffer)
        {
            _world = world;
            _deathHandler = deathHandler;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity requestEntity in _world.Filter().With<EnemyDespawnRequest>())
            {
                EnemyDespawnRequest request = _world.Get<EnemyDespawnRequest>(requestEntity);
                EnemyEcsLink ecsLink = request.Enemy.GetComponent<EnemyEcsLink>();

                if (ecsLink.IsRegistered)
                {
                    _commandBuffer.DestroyEntity(ecsLink.Entity);
                    ecsLink.Clear();
                }

                _deathHandler.Handle(request.Enemy);
                _commandBuffer.DestroyEntity(requestEntity);
            }
        }
    }
}