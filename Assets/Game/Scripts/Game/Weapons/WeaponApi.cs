using System.Collections.Generic;

namespace Game
{
    public class WeaponApi
    {
        private readonly CommandBuffer _commandBuffer;
        private readonly PlayerWeaponRegistry _playerWeaponRegistry;

        public WeaponApi(CommandBuffer commandBuffer, PlayerWeaponRegistry playerWeaponRegistry)
        {
            _commandBuffer = commandBuffer;
            _playerWeaponRegistry = playerWeaponRegistry;
        }

        public IReadOnlyDictionary<Weapons, Weapon> Weapons => _playerWeaponRegistry.Weapons;

        public void RequestAddWeapon(Weapons name, Weapon weapon)
        {
            Entity request = _commandBuffer.CreateEntity();

            _commandBuffer.Add(request, new WeaponAddRequest
            {
                Name = name,
                Weapon = weapon
            });
        }

        public void RequestSetStats(Weapons name, WeaponStats stats)
        {
            Entity request = _commandBuffer.CreateEntity();

            _commandBuffer.Add(request, new WeaponStatsSetRequest
            {
                Name = name,
                Stats = stats
            });
        }
        
        public void RequestResetWeapons()
        {
            Entity request = _commandBuffer.CreateEntity();

            _commandBuffer.Add(request, new WeaponResetRequest());
        }
    }
}