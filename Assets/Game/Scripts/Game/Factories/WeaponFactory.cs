using UnityEngine;
using VContainer;
using VContainer.Unity;
using Contracts;

namespace Game
{
    public class WeaponFactory : IWeaponFactory
    {
        private readonly Transform _playerParent;
        private readonly IObjectResolver _resolver;

        public WeaponFactory([Key("WeaponRoot")] Transform playerParent, IObjectResolver resolver)
        {
            _playerParent = playerParent;
            _resolver = resolver;
        }

        public Weapon CreateWeapon(Weapon template) => _resolver.Instantiate(template, _playerParent);
    }
}