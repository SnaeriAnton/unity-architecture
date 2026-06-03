using System.Collections.Generic;
using Leopotam.EcsLite;

namespace Game
{
    public class WeaponApi
    {
        private readonly EcsWorld _world;
        private readonly PlayerWeaponRegistry _weaponRegistry;

        public WeaponApi(EcsWorld world, PlayerWeaponRegistry weaponRegistry)
        {
            _world = world;
            _weaponRegistry = weaponRegistry;
        }

        public IReadOnlyDictionary<Weapons, Weapon> Weapons => _weaponRegistry.Weapons;

        public void RequestAddWeapon(Weapons name, Weapon weapon)
        {
            int request = _world.NewEntity();

            ref WeaponAddRequest data = ref _world.GetPool<WeaponAddRequest>().Add(request);

            data.Name = name;
            data.Weapon = weapon;
        }

        public void RequestSetStats(Weapons name, WeaponStats stats)
        {
            int request = _world.NewEntity();
            ref WeaponStatsSetRequest data = ref _world.GetPool<WeaponStatsSetRequest>().Add(request);

            data.Name = name;
            data.Stats = stats;
        }

        public void RequestResetWeapons()
        {
            int request = _world.NewEntity();
            _world.GetPool<WeaponResetRequest>().Add(request);
        }
    }
}