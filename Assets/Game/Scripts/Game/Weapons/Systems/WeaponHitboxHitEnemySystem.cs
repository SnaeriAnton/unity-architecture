namespace Game
{
    public class WeaponHitboxHitEnemySystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public WeaponHitboxHitEnemySystem(
            EcsWorld world,
            CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity requestEntity in _world.Filter().With<WeaponHitboxHitEnemyRequest>())
            {
                WeaponHitboxHitEnemyRequest request = _world.Get<WeaponHitboxHitEnemyRequest>(requestEntity);

                if (!_world.Has<WeaponHitboxTag>(request.WeaponHitbox))
                {
                    _commandBuffer.DestroyEntity(requestEntity);
                    continue;
                }

                if (!_world.Has<EnemyTag>(request.Enemy))
                {
                    _commandBuffer.DestroyEntity(requestEntity);
                    continue;
                }

                if (!_world.TryGet(request.WeaponHitbox, out Damage damage))
                {
                    _commandBuffer.DestroyEntity(requestEntity);
                    continue;
                }

                Entity damageRequest = _commandBuffer.CreateEntity();

                _commandBuffer.Add(damageRequest, new EnemyDamageRequest
                {
                    Target = request.Enemy,
                    Amount = damage.Value
                });

                _commandBuffer.DestroyEntity(requestEntity);
            }
        }
    }
}