using System.Collections.Generic;
using Domain;

namespace Infrastructure
{
    public class WeaponSystem
    {
        private readonly Dictionary<Weapons, Weapon> _weapons = new();

        public IReadOnlyDictionary<Weapons, Weapon> Weapons => _weapons;
        public Shield Shield { get; private set; }

        public void AddWeapon(Weapons name, Weapon weapon)
        {
            _weapons[name] = weapon;
            if (weapon is Shield shield) Shield = shield;
        }

        public void Update() => _weapons.Values.ForEach(w => w.UpdateValues());
        public void ApplyAll() => _weapons.Values.ForEach(w => w.Apply());

        public void Reset()
        {
            Shield = null;
            _weapons.Values.ForEach(w => w.Reset());
        }

        public void SetStats(Weapons name, WeaponStats stats) => _weapons[name].SetStats(stats);
    }
}