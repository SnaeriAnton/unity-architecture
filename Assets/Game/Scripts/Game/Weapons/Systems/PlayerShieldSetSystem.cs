using Leopotam.EcsLite;

namespace Game
{
    public class PlayerShieldSetSystem : IEcsRunSystem
    {
        private readonly PlayerApi _playerApi;

        public PlayerShieldSetSystem(PlayerApi playerApi) => _playerApi = playerApi;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<PlayerShieldSetRequest>().End();

            EcsPool<PlayerShieldSetRequest> requestPool = world.GetPool<PlayerShieldSetRequest>();
            EcsPool<PlayerShield> shieldPool = world.GetPool<PlayerShield>();
            EcsPool<HudRefreshRequest> hudPool = world.GetPool<HudRefreshRequest>();

            foreach (int requestEntity in filter)
            {
                ref PlayerShieldSetRequest request = ref requestPool.Get(requestEntity);

                if (_playerApi.HasPlayer)
                {
                    int player = _playerApi.PlayerEntity;

                    if (shieldPool.Has(player))
                    {
                        ref PlayerShield shield = ref shieldPool.Get(player);

                        shield.IsActive = request.IsActive;
                        shield.Cooldown = request.Cooldown;

                        if (!shield.IsActive)
                            shield.Current = 0;

                        if (shield.Current > shield.Cooldown)
                            shield.Current = shield.Cooldown;

                        int hudRequest = world.NewEntity();
                        hudPool.Add(hudRequest);
                    }
                }

                world.DelEntity(requestEntity);
            }
        }
    }
}