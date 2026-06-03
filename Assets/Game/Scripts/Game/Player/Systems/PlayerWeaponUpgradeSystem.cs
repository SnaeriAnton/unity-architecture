using Leopotam.EcsLite;

namespace Game
{
    public class PlayerWeaponUpgradeSystem : IEcsRunSystem
    {
        private readonly PlayerWeaponRegistry _weaponRegistry;
        private readonly PlayerApi _playerApi;

        public PlayerWeaponUpgradeSystem(PlayerWeaponRegistry weaponRegistry, PlayerApi playerApi)
        {
            _weaponRegistry = weaponRegistry;
            _playerApi = playerApi;
        }

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsPool<WeaponAddRequest> addPool = world.GetPool<WeaponAddRequest>();
            EcsPool<WeaponStatsSetRequest> statsPool = world.GetPool<WeaponStatsSetRequest>();

            foreach (int requestEntity in world.Filter<WeaponAddRequest>().End())
            {
                ref WeaponAddRequest request = ref addPool.Get(requestEntity);

                _weaponRegistry.AddWeapon(request.Name, request.Weapon);

                world.DelEntity(requestEntity);
            }

            foreach (int requestEntity in world.Filter<WeaponStatsSetRequest>().End())
            {
                ref WeaponStatsSetRequest request = ref statsPool.Get(requestEntity);

                _weaponRegistry.SetStats(request.Name, request.Stats);

                if (request.Name == Weapons.Shield)
                    _playerApi.RequestSetShield(true, request.Stats.CoolDown);

                world.DelEntity(requestEntity);
            }
        }
    }
}