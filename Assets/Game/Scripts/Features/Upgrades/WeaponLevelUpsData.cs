using UnityEngine;
using Shared;

namespace Upgrades
{
    [CreateAssetMenu(fileName = "WeaponLevelUpsData", menuName = "Micro Vampire/Level up/Level ups data")]
    public class WeaponLevelUpsData : LevelUpsData<WeaponStats>
    {
        [field: SerializeField] public Shared.Weapons WeaponTemplate { get; private set; }
    }
}
