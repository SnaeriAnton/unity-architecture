using System;

namespace Game
{
    public interface IShieldReadModel
    {
        public ShieldModel Shield { get; }

        public event Action OnShieldStateChanged;
    }
}