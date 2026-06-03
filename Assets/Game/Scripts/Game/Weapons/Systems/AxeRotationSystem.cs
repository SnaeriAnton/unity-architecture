using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class AxeRotationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<AxeTag>().Inc<Rotation>().Inc<RotationSpeed>().Exc<AxeDespawnRequestedTag>().End();

            EcsPool<Rotation> rotationPool = world.GetPool<Rotation>();
            EcsPool<RotationSpeed> rotationSpeedPool = world.GetPool<RotationSpeed>();

            float deltaTime = Time.deltaTime;

            foreach (int axe in filter)
            {
                ref Rotation rotation = ref rotationPool.Get(axe);
                ref RotationSpeed rotationSpeed = ref rotationSpeedPool.Get(axe);

                rotation.Value *= Quaternion.Euler(0f, 0f, rotationSpeed.Value * deltaTime);
            }
        }
    }
}