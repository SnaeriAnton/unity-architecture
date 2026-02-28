using UnityEngine;

namespace Shared
{
    public interface IWeaponFactory
    {
        public void CreateWeapon(Shared.Weapons name, Transform parent);
    }
}
