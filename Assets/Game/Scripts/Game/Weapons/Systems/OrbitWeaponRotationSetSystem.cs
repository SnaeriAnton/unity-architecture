namespace Game
{
    public class OrbitWeaponRotationSetSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public OrbitWeaponRotationSetSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity requestEntity in _world.Filter().With<OrbitWeaponRotationSetRequest>())
            {
                OrbitWeaponRotationSetRequest request = _world.Get<OrbitWeaponRotationSetRequest>(requestEntity);

                if (_world.Has<OrbitWeaponRotation>(request.Weapon))
                {
                    OrbitWeaponRotation rotation = _world.Get<OrbitWeaponRotation>(request.Weapon);
                    rotation.DegreesPerSecond = request.DegreesPerSecond;
                    _world.Set(request.Weapon, rotation);
                }

                _commandBuffer.DestroyEntity(requestEntity);
            }
        }
    }
}