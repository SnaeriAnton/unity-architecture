using System;
using UnityEngine;
using Shared;

namespace Enemies
{
    public class Samurai : Enemy<SamuraiStats>
    {
        private void Update()
        {
            if (_target.IsDead) return;
            transform.position = Vector3.MoveTowards(transform.position, _target.Position, _stats.Stats.Speed * Time.deltaTime);
        }

        public override void Construct(ITarget target, Action<EnemyBase> obDiedCallBack, IObjectPool pool)
        {
            base.Construct(target, obDiedCallBack, pool);
            _health = _stats.Stats.Health;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ITarget>(out _))
            {
                _target.TakeDamage(_stats.Stats.Damage);
                Die();
            }
        }
    }
}