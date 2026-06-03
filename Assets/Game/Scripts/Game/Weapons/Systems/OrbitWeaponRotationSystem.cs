using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class OrbitWeaponRotationSystem : IEcsRunSystem
    {
        private readonly PlayerApi _playerApi;

        public OrbitWeaponRotationSystem(PlayerApi playerApi)
        {
            _playerApi = playerApi;
        }

        public void Run(IEcsSystems systems)
        {
            if (!_playerApi.HasPlayingTag()) return;

            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<OrbitWeaponTag>().Inc<OrbitWeaponRotation>().End();
            EcsPool<OrbitWeaponRotation> rotationPool = world.GetPool<OrbitWeaponRotation>();

            float deltaTime = Time.deltaTime;

            foreach (int weapon in filter)
            {
                ref OrbitWeaponRotation rotation = ref rotationPool.Get(weapon);

                rotation.Angle += rotation.DegreesPerSecond * deltaTime;
            }
        }
    }
}