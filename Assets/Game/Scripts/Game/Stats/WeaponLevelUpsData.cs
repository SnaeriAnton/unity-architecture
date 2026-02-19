using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "WeaponLevelUpsData", menuName = "Micro Vampire/Level up/Level ups data")]
    public class WeaponLevelUpsData : LevelUpsData<WeaponStats>
    {
        [field: SerializeField] public Weapon WeaponTemplate { get; private set; }
    }
    
    [Serializable]
    public struct WeaponStats
    {
        public float Damage;
        public float RoundSpeed;
        public float FlightSpeed;
        public float LifeTime;
        public float AttacksPerSecond;
        public int CoolDown;
        public int Count;
        public int Strength;
    }
}
