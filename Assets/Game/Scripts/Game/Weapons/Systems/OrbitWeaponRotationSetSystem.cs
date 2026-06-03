using Leopotam.EcsLite;

namespace Game
{
    public class OrbitWeaponRotationSetSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<OrbitWeaponRotationSetRequest>().End();

            EcsPool<OrbitWeaponRotationSetRequest> requestPool = world.GetPool<OrbitWeaponRotationSetRequest>();
            EcsPool<OrbitWeaponRotation> rotationPool = world.GetPool<OrbitWeaponRotation>();

            foreach (int requestEntity in filter)
            {
                ref OrbitWeaponRotationSetRequest request = ref requestPool.Get(requestEntity);

                if (rotationPool.Has(request.Weapon))
                {
                    ref OrbitWeaponRotation rotation = ref rotationPool.Get(request.Weapon);

                    rotation.DegreesPerSecond = request.DegreesPerSecond;
                }

                world.DelEntity(requestEntity);
            }
        }
    }
}