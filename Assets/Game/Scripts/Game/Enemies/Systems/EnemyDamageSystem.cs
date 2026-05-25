using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemyDamageSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public EnemyDamageSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity requestEntity in _world.Filter().With<EnemyDamageRequest>())
            {
                EnemyDamageRequest request = _world.Get<EnemyDamageRequest>(requestEntity);
                Entity enemy = request.Target;

                if (!_world.Has<EnemyTag>(enemy))
                {
                    _commandBuffer.DestroyEntity(requestEntity);
                    continue;
                }

                if (!_world.Has<EnemyHealth>(enemy))
                {
                    _commandBuffer.DestroyEntity(requestEntity);
                    continue;
                }

                
                if (!_world.TryGet(enemy, out EnemyHealth health))
                {
                    _commandBuffer.DestroyEntity(requestEntity);
                    continue;
                }
                
                health.Current -= request.Amount;
                health.Current = Mathf.Clamp(health.Current, 0, health.Max);

                _world.Set(enemy, health);

                _commandBuffer.DestroyEntity(requestEntity);
            }
        }
    }
}