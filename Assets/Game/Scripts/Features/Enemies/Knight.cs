using System;
using UnityEngine;
using Shared;

namespace Enemies
{
    public class Knight : Enemy<KnightStats>
    {
        private bool _playerInRange;
        private float _lastTimeSpawn;

        protected virtual void Update()
        {
            if (_target.IsDead) return;

            if (_playerInRange)
            {
                float interval = Time.time - _lastTimeSpawn;

                if (interval >= _stats.Stats.AttacksPerSecond)
                {
                    _lastTimeSpawn = Time.time;
                    _target.TakeDamage(_stats.Stats.Damage);
                }
            }

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
                _lastTimeSpawn = Time.time;
                _playerInRange = true;
                _target.TakeDamage(_stats.Stats.Damage);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<ITarget>(out _) && _playerInRange)
                _playerInRange = false;
        }
    }
}