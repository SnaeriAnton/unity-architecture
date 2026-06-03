using Leopotam.EcsLite;

namespace Game
{
    public class AxeHitPlayerSystem : IEcsRunSystem
    {
        private readonly PlayerApi _playerApi;

        public AxeHitPlayerSystem(PlayerApi playerApi) => _playerApi = playerApi;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<AxeTag>().Inc<Damage>().Inc<AxeHitPlayerTag>().Exc<AxeDespawnRequestedTag>().End();

            EcsPool<Damage> damagePool = world.GetPool<Damage>();
            EcsPool<AxeDespawnRequestedTag> despawnRequestedPool = world.GetPool<AxeDespawnRequestedTag>();

            foreach (int axe in filter)
            {
                ref Damage damage = ref damagePool.Get(axe);

                _playerApi.RequestPlayerDamage(damage.Value);

                if (!despawnRequestedPool.Has(axe))
                    despawnRequestedPool.Add(axe);
            }
        }
    }
}