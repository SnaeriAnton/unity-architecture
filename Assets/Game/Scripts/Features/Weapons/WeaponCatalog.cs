using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "WeaponCatalog", menuName = "Micro Vampire/Weapons catalog")]
    public class WeaponCatalog : ScriptableObject
    {
        [SerializeField] private List<WeaponData> _weapons;

        public Weapon GetWeapon(Shared.Weapons name) => _weapons.First(w=>w.Name == name).Template;
    }

    [Serializable]
    public struct WeaponData
    {
        public Shared.Weapons Name;
        public Weapon Template;
    }
}