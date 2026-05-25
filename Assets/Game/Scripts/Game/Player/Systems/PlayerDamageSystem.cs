using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerDamageSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public PlayerDamageSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity requestEntity in _world.Filter().With<DamageRequest>())
            {
                DamageRequest damageRequest = _world.Get<DamageRequest>(requestEntity);
                Entity target = damageRequest.Target;
                Entity request;

                if (!_world.Has<PlayerTag>(target))
                {
                    _commandBuffer.DestroyEntity(requestEntity);
                    continue;
                }

                if (_world.Has<DeadTag>(target))
                {
                    _commandBuffer.DestroyEntity(requestEntity);
                    continue;
                }

                Invulnerability invulnerability = _world.Get<Invulnerability>(target);

                if (invulnerability.TimeLeft > 0f)
                {
                    _commandBuffer.DestroyEntity(requestEntity);
                    continue;
                }

                if (!_world.TryGet(target, out Health health))
                {
                    _commandBuffer.DestroyEntity(requestEntity);
                    continue;
                }

                health.Current -= (int)damageRequest.Amount;
                health.Current = Mathf.Clamp(health.Current, 0, health.Max);

                _world.Set(target, health);

                invulnerability.TimeLeft = invulnerability.Duration;
                _world.Set(target, invulnerability);

                request = _commandBuffer.CreateEntity();
                _commandBuffer.Add(request, new HudRefreshRequest());

                if (health.Current <= 0 && !_world.Has<DeadTag>(target))
                    _commandBuffer.AddIfMissing(target, new DeadTag());

                _commandBuffer.DestroyEntity(requestEntity);
            }
        }
    }
}