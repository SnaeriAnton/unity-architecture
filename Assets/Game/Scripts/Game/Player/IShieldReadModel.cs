using System;

namespace Game
{
    public interface IShieldReadModel
    {
        public bool HasShield { get; }
        public int CurrentCoolDownCount { get; }
        public int CoolDown { get; }

        public event Action OnShieldStateChanged;
    }
}