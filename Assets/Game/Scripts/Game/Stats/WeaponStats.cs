using System;

namespace Game
{
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