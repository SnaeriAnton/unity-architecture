using System.Collections.Generic;
using Application;
using UnityEngine;
using Domain;

namespace Presentation
{
    public class WeaponFactory : IWeaponFactory
    {
        private readonly Dictionary<Weapons, Weapon> _weaponTemplates;
        private readonly Player _player;
        private readonly ProjectileSpawner _projectileSpawner;

        public WeaponFactory(IReadOnlyDictionary<Weapons, Weapon> weaponTemplates, Player player, ProjectileSpawner projectileSpawner)
        {
            _weaponTemplates = new(weaponTemplates);
            _player = player;
            _projectileSpawner = projectileSpawner;
        }

        public void CreateWeapon(Weapons name)
        {
            Weapon weapon = Object.Instantiate(_weaponTemplates[name], _player.transform);
            weapon.Construct(_projectileSpawner);
            _player.AddWeapon(name, weapon);
        }
    }
}