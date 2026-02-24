using System;

namespace Domain
{
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