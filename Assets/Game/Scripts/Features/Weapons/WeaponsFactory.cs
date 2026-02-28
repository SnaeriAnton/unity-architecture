using UnityEngine;
using Object = UnityEngine.Object;
using Shared;

namespace Weapons
{
    public class WeaponsFactory : IWeaponFactory
    {
        private readonly WeaponSystem _weapon;
        private readonly WeaponCatalog _catalog;
        private readonly IObjectPool _pool;

        public WeaponsFactory(WeaponSystem weapon, WeaponCatalog catalog, IObjectPool pool)
        {
            _weapon = weapon;
            _catalog = catalog;
            _pool = pool;
        }

        public void CreateWeapon(Shared.Weapons name, Transform parent)
        {
            Weapon weapon = Object.Instantiate(_catalog.GetWeapon(name), parent);
            weapon.Construct(_pool);
            _weapon.AddWeapon(name, weapon);
        }
    }
}