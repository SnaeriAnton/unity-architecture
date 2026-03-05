using UnityEngine;
using Domain;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "WeaponLevelUpsData", menuName = "Micro Vampire/Level up/Level ups data")]
    public class WeaponLevelUpsData : LevelUpsData<WeaponStats>
    {
        [field: SerializeField] public Weapon WeaponTemplate { get; private set; }
    }
}
