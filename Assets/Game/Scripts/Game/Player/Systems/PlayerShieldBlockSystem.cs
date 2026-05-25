namespace Game
{
    public class PlayerShieldBlockSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public PlayerShieldBlockSystem(
            EcsWorld world,
            CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity requestEntity in _world.Filter().With<DamageRequest>())
            {
                DamageRequest request = _world.Get<DamageRequest>(requestEntity);

                if (!_world.Has<PlayerTag>(request.Target)) continue;
                if (_world.Has<DeadTag>(request.Target)) continue;
                if (!_world.TryGet(request.Target, out PlayerShield shield)) continue;
                if (!shield.IsActive) continue;
                if (shield.Current < shield.Cooldown) continue;

                shield.Current = 0;
                _world.Set(request.Target, shield);

                Entity hudRequest = _commandBuffer.CreateEntity();
                _commandBuffer.Add(hudRequest, new HudRefreshRequest());

                _commandBuffer.DestroyEntity(requestEntity);
            }
        }
    }
}