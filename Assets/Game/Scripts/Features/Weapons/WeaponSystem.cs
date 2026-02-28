using System.Collections.Generic;
using Shared;
using ExtensionSystems;

namespace Weapons
{
    public class WeaponSystem : IWeapon
    {
        private readonly Dictionary<Shared.Weapons, Weapon> _weapons = new();
        public IShield Shield { get; private set; }

        public bool HasWeapon(Shared.Weapons name) => _weapons.ContainsKey(name);
        public void Update() => _weapons.Values.ForEach(w => w.UpdateValues());
        public void ApplyAll() => _weapons.Values.ForEach(w => w.Apply());
        public void SetStats(Shared.Weapons name, WeaponStats stats) => _weapons[name].SetStats(stats);
        
        public void AddWeapon(Shared.Weapons name, Weapon weapon)
        {
            _weapons[name] = weapon;
            if (weapon is Shield shield) Shield = shield;
        }

        public void Reset()
        {
            Shield = null;
            _weapons.Values.ForEach(w => w.Reset());
        }
    }
}