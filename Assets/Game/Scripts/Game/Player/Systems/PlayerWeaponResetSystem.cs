using Leopotam.EcsLite;

namespace Game
{
    public class PlayerWeaponResetSystem : IEcsRunSystem
    {
        private readonly PlayerWeaponRegistry _weaponRegistry;
        private readonly PlayerApi _playerApi;

        public PlayerWeaponResetSystem(PlayerWeaponRegistry weaponRegistry, PlayerApi playerApi)
        {
            _weaponRegistry = weaponRegistry;
            _playerApi = playerApi;
        }

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<WeaponResetRequest>().End();

            foreach (int request in filter)
            {
                _weaponRegistry.Reset();
                _playerApi.RequestSetShield(false, 0);

                world.DelEntity(request);
            }
        }
    }
}