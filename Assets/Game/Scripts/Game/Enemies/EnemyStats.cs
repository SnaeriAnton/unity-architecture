using System;
using UnityEngine;

namespace Game
{
    public class EnemyStats : ScriptableObject
    {
        [field: SerializeField] public Stats Stats { get; private set; }
    }
    
    [Serializable]
    public struct Stats
    {
        public float Health;
        public float Speed;
        public float AttacksPerSecond;
        public int Damage;
    }
}