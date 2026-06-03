using Leopotam.EcsLite;

namespace Game
{
    public class PlayerShieldRechargeSystem : IEcsRunSystem
    {
        private readonly PlayerApi _playerApi;

        public PlayerShieldRechargeSystem(PlayerApi playerApi) => _playerApi = playerApi;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter killedFilter = world.Filter<EnemyKilledRequest>().End();

            EcsPool<PlayerShield> shieldPool = world.GetPool<PlayerShield>();
            EcsPool<HudRefreshRequest> hudPool = world.GetPool<HudRefreshRequest>();

            foreach (int killedRequest in killedFilter)
            {
                if (_playerApi.HasPlayer)
                {
                    int player = _playerApi.PlayerEntity;

                    if (shieldPool.Has(player))
                    {
                        ref PlayerShield shield = ref shieldPool.Get(player);

                        if (shield.IsActive && shield.Current < shield.Cooldown)
                        {
                            shield.Current++;

                            int hudRequest = world.NewEntity();
                            hudPool.Add(hudRequest);
                        }
                    }
                }

                world.DelEntity(killedRequest);
            }
        }
    }
}