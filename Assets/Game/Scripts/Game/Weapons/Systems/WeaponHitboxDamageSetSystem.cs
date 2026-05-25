namespace Game
{
    public class WeaponHitboxDamageSetSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public WeaponHitboxDamageSetSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity requestEntity in _world.Filter().With<WeaponHitboxDamageSetRequest>())
            {
                WeaponHitboxDamageSetRequest request = _world.Get<WeaponHitboxDamageSetRequest>(requestEntity);

                if (_world.Has<WeaponHitboxTag>(request.WeaponHitbox) &&
                    _world.Has<Damage>(request.WeaponHitbox))
                {
                    Damage damage = _world.Get<Damage>(request.WeaponHitbox);
                    damage.Value = request.Damage;

                    _world.Set(request.WeaponHitbox, damage);
                }

                _commandBuffer.DestroyEntity(requestEntity);
            }
        }
    }
}