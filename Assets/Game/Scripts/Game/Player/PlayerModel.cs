using System;
using UnityEngine;

namespace Game
{
    public class PlayerModel : IPlayerReadModel
    {
        private SpartanStats _spartanStats;
        
        public bool IsPlaying { get; private set; } = false;
        public bool IsDead => CurrentHealth <= 0;
        public int CurrentHealth { get; private set; }
        public int MaxHealth => _spartanStats.Health;
        
        public event Action OnHealthChanged;
        public event Action OnDied;
        
        public void StartPlay() => IsPlaying = true;
        public void Restart() => _spartanStats = default;
        
        public void SetPlayerStats(SpartanStats stats)
        {
            _spartanStats = stats;
            CurrentHealth = _spartanStats.Health;
        }
        
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _spartanStats.Health);
            OnHealthChanged?.Invoke();

            if (CurrentHealth == 0) Die();
        }
        
        private void Die()
        {
            IsPlaying = false;
            OnDied?.Invoke();
        }
    }
}