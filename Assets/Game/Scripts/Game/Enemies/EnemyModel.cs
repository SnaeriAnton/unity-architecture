using System;
using Contracts;
using UnityEngine;

namespace Game
{
    public abstract class EnemyModel
    {
        protected float _health;

        public EnemyModel(EnemyStats stats, ITarget target)
        {
            Stats = stats;
            Target = target;
            _health = Stats.Stats.Health;
        }

        public ITarget Target { get; private set; }
        public EnemyStats Stats { get; }
        
        public event Action<EnemyModel> OnDiedCallBack;
        
        public void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0) OnDiedCallBack?.Invoke(this);
        }
        
        public virtual void OnHit() { }
        
        public void Die() => OnDiedCallBack?.Invoke(this);
    }
}