using System.Collections.Generic;
using ExtensionSystems;

namespace Game
{
    public class PlayerWeaponRegistry
    {
        private readonly Dictionary<Weapons, Weapon> _weapons = new();

        public IReadOnlyDictionary<Weapons, Weapon> Weapons => _weapons;

        public void AddWeapon(Weapons name, Weapon weapon) => _weapons[name] = weapon;
        public void Reset() => _weapons.Values.ForEach(w => w.Reset());
        public void SetStats(Weapons name, WeaponStats stats) => _weapons[name].SetStats(stats);
    }
}