using System;
using System.Collections.Generic;
using Contracts;
using UnityEngine;

namespace Game
{
    public class PlayerModel : ITickable, IShieldReadModel, IPlayerReadModel
    {
        private readonly WeaponSystem _weapon;
        private readonly float _iFramesDuration;
        
        private SpartanStats _spartanStats;
        private float _invulnTimer;

        public PlayerModel( float iFramesDuration)
        {
            _iFramesDuration = iFramesDuration;
            _weapon = new();
        }
        
        public IReadOnlyDictionary<Weapons, WeaponController> Weapons => _weapon.Weapons;
        public ShieldModel Shield => _weapon.Shield;
        private bool IsInvulnerable => _invulnTimer > 0f;
        public bool IsPlaying { get; private set; } = false;
        public bool IsDead => CurrentHealth <= 0;
        public int CurrentHealth { get; private set; }
        public int MaxHealth => _spartanStats.Health;

        public event Action OnHealthChanged;
        public event Action OnDied;
        public event Action OnShieldStateChanged;
        public event Action OnRestart;

        public void Dispose() => _weapon.Dispose();
        
        public void StartPlay() => IsPlaying = true;
        public void AddWeapon(Weapons name, WeaponController weapon) => _weapon.AddWeapon(name, weapon);
        public void SetWeaponStats(Weapons name, WeaponStats stats) => _weapon.SetStats(name, stats);
        
        public void Restart()
        {
            OnRestart?.Invoke();
            _spartanStats = default;
            _weapon.Reset();
            _invulnTimer = 0;
        }

        public void KillEnemy()
        {
            _weapon.Update();
            OnShieldStateChanged?.Invoke();
        }

        public void Tick()
        {
            if (IsDead || !IsPlaying) return;

            _weapon.ApplyAll();

            if (IsInvulnerable)
                _invulnTimer -= Time.deltaTime;
        }

        public void SetPlayerStats(SpartanStats stats)
        {
            _spartanStats = stats;
            CurrentHealth = _spartanStats.Health;
        }

        public void TakeDamage(int damage)
        {
            if (IsInvulnerable) return;
            if (Shield != null && Shield.TryApply())
            {
                OnShieldStateChanged?.Invoke();
                return;
            }

            _invulnTimer = _iFramesDuration;
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