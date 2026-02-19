using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "VikingStats", menuName = "Micro Vampire/Enemies/Viking stats")]
    public class VikingStats : EnemyStats
    {
        [field: SerializeField] public AxeStats AxeStats { get; private set; }
        [field: SerializeField] public Axe AxeTemplate { get; private set; }
    }

    [Serializable]
    public struct AxeStats
    {
        public float FlightSpeed;
        public float RotationSpeed;
        public float LifeTime;
        public float AttacksPerSecond;
        public int Damage;
    }
}