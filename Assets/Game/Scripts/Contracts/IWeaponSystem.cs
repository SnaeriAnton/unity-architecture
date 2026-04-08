using System.Collections.Generic;
using Game;

namespace Contracts
{
    public interface IWeaponSystem
    {
        public bool HasWeapon(Weapons name);
        public void SetStats(Weapons name, WeaponStats stats);
        public void AddWeapon(Weapons name, Weapon weapon);
        public void RefreshWeapons();
    }
}