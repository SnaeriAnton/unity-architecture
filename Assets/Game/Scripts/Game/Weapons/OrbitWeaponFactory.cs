using UnityEngine;

namespace Game
{
    public class OrbitWeaponFactory
    {
        private readonly CommandBuffer _commandBuffer;

        public OrbitWeaponFactory(CommandBuffer commandBuffer) => _commandBuffer = commandBuffer;

        public Entity Create(Transform transform, float degreesPerSecond)
        {
            Entity entity = _commandBuffer.CreateEntity();

            _commandBuffer.Add(entity, new PlayerWeaponRuntimeTag());
            _commandBuffer.Add(entity, new OrbitWeaponTag());
            _commandBuffer.Add(entity, new OrbitWeaponViewRef { Transform = transform });
            _commandBuffer.Add(entity, new GameRuntimeTag());
            _commandBuffer.Add(entity, new OrbitWeaponRotation
            {
                Angle = transform.localEulerAngles.z,
                DegreesPerSecond = degreesPerSecond
            });

            return entity;
        }
    }
}