using System;

namespace Domain
{
    [Serializable]
    public struct Stats
    {
        public float Health;
        public float Speed;
        public float AttacksPerSecond;
        public int Damage;
    }
}