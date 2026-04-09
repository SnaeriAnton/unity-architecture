using System.Collections.Generic;
using Contracts;
using ExtensionSystems;

namespace Game
{
    public class WeaponSystem : IWeaponSystem
    {
        private readonly Dictionary<Weapons, Weapon> _weapons = new();

        private ShieldModel _model;
        
        public WeaponSystem(ShieldModel model) => _model = model;
        
        public Shield Shield { get; private set; }

        public void AddWeapon(Weapons name, Weapon weapon)
        {
            _weapons[name] = weapon;
            if (weapon is Shield shield)
            {
                Shield = shield;
                Shield.SetShieldModel(_model);
            }
        }

        public bool HasWeapon(Weapons name) => _weapons.ContainsKey(name);
        public void RefreshWeapons() => _weapons.Values.ForEach(w => w.RefreshState());
        public void ApplyAll(float dt) => _weapons.Values.ForEach(w => w.Tick(dt));

        public void Reset()
        {
            Shield = null;
            _weapons.Values.ForEach(w => w.Reset());
        }

        public void SetStats(Weapons name, WeaponStats stats) => _weapons[name].SetStats(stats);
    }
}