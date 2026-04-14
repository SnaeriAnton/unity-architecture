
using System;

namespace Game
{
    public interface IPlayerReadModel
    {
        public bool IsPlaying { get; }
        public bool IsDead { get; }
        public int CurrentHealth { get; }
        public int MaxHealth { get; }

        public event Action OnHealthChanged;
        public event Action OnDied;
    }
}