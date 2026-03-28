using System;
using UnityEngine;

namespace Game
{
    public class EnemyModel
    {
        private float _health;

        public EnemyModel(Stats stats)
        {
            Stats = stats;
            _health = Stats.Health;
        }

        public Vector3 Position { get; private set; }
        public Stats Stats { get; }

        public event Action OnDie;

        public void SetPosition(Vector3 pos) => Position = pos;
        
        public void TakeDamage(float damage)
        {
            if (_health <= 0) return;
            
            _health -= damage;

            if (_health <= 0)
                OnDie?.Invoke();
        }
    }
}