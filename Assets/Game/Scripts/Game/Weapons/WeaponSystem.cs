using System.Collections.Generic;
using ExtensionSystems;

namespace Game
{
    public class WeaponSystem
    {
        private readonly Dictionary<Weapons, WeaponController> _weapons = new();

        public IReadOnlyDictionary<Weapons, WeaponController> Weapons => _weapons;
        public ShieldModel Shield { get; private set; }

        public void AddWeapon(Weapons name, WeaponController controller)
        {
            _weapons[name] = controller;
            if (controller.Model is ShieldModel shield) Shield = shield;
        }

        public void Dispose() => _weapons.Values.ForEach(w => w.Dispose());
        public void Update() => _weapons.Values.ForEach(w => w.UpdateValues());
        public void ApplyAll() => _weapons.Values.ForEach(w => w.Apply());

        public void Reset()
        {
            Shield = null;
            _weapons.Values.ForEach(w => w.Model.Reset());
        }

        public void SetStats(Weapons name, WeaponStats stats) => _weapons[name].Model.SetStats(stats);
    }
}