using UnityEngine;

namespace Game
{
    public class AxeFactory
    {
        private readonly CommandBuffer _commandBuffer;

        public AxeFactory(CommandBuffer commandBuffer) => _commandBuffer = commandBuffer;

        public Entity CreateAxe(Axe axe, Vector2 position, Vector2 direction, Quaternion rotation, float speed, float lifetime, int damage, float rotationSpeed)
        {
            Entity entity = _commandBuffer.CreateEntity();

            _commandBuffer.Add(entity, new AxeTag());
            _commandBuffer.Add(entity, new Position { Value = position });
            _commandBuffer.Add(entity, new Rotation { Value = rotation });
            _commandBuffer.Add(entity, new MoveSpeed { Value = speed });
            _commandBuffer.Add(entity, new RotationSpeed { Value = rotationSpeed });
            _commandBuffer.Add(entity, new Direction { Value = direction.normalized });
            _commandBuffer.Add(entity, new Lifetime { TimeLeft = lifetime });
            _commandBuffer.Add(entity, new Damage { Value = damage });
            _commandBuffer.Add(entity, new AxeViewRef { Axe = axe });
            _commandBuffer.Add(entity, new GameRuntimeTag());

            return entity;
        }
    }
}