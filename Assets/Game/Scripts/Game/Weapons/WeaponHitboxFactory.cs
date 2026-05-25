using UnityEngine;

namespace Game
{
    public class WeaponHitboxFactory
    {
        private readonly CommandBuffer _commandBuffer;

        public WeaponHitboxFactory(CommandBuffer commandBuffer) => _commandBuffer = commandBuffer;

        public Entity Create(Transform transform, float damage)
        {
            Entity entity = _commandBuffer.CreateEntity();

            _commandBuffer.Add(entity, new PlayerWeaponRuntimeTag());
            _commandBuffer.Add(entity, new WeaponHitboxTag());
            _commandBuffer.Add(entity, new WeaponHitboxViewRef { Transform = transform });
            _commandBuffer.Add(entity, new Damage { Value = damage });
            _commandBuffer.Add(entity, new GameRuntimeTag());

            return entity;
        }
    }
}