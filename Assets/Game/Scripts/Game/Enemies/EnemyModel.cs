using UniRx;
using UnityEngine;

namespace Game
{
    public class EnemyModel
    {
        private readonly ReactiveProperty<Vector3> _position = new();
        private readonly BoolReactiveProperty _isDead = new();
        
        private float _health;

        public EnemyModel(Stats stats)
        {
            Stats = stats;
            _isDead = new(false);
            _health = Stats.Health;
        }

        public IReadOnlyReactiveProperty<Vector3> Position => _position;
        public IReadOnlyReactiveProperty<bool> IsDead => _isDead;
        public Stats Stats { get; }
        
        public void SetPosition(Vector3 pos) => _position.Value = pos;
        
        public void TakeDamage(float damage)
        {
            if (_health <= 0) return;
            
            _health -= damage;

            if (_health <= 0)
                _isDead.Value = true;
        }
    }
}