using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "WeaponLevelUpsData", menuName = "Micro Vampire/Level up/Level ups data")]
    public class WeaponLevelUpsData : LevelUpsData<WeaponStats>
    {
        [field: SerializeField] public WeaponView WeaponTemplate { get; private set; }
    }
}
