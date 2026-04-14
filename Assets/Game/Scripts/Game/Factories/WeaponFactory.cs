using Contracts;
using UnityEngine;
using Zenject;

namespace Game
{
    public class WeaponFactory : IWeaponFactory
    {
        private readonly Transform _playerParent;
        private readonly DiContainer _container;
        
        public WeaponFactory([Inject(Id = "WeaponRoot")] Transform playerParent, DiContainer container)
        {
            _playerParent = playerParent;
            _container = container;
        }

        public Weapon CreateWeapon(Weapon template) => _container.InstantiatePrefabForComponent<Weapon>(template, _playerParent);
    }
}