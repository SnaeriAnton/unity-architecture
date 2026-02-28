using UnityEngine;

namespace Shared
{
    public interface IUpgradable
    {
        public Transform Transform { get; }

        public bool HasWeapon(Weapons name);
        public void SetWeaponStats(Weapons name, WeaponStats stats);
        public void SetPlayerStats(SpartanStats stats);
    }
}
