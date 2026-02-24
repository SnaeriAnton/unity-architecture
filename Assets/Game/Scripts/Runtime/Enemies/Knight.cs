using System;
using UnityEngine;

namespace Runtime
{
    public class Knight : Enemy<KnightStats>
    {
        private bool _playerInRange;
        private float _lastTimeSpawn;

        protected virtual void Update()
        {
            if (_player.IsDead) return;

            if (_playerInRange)
            {
                float interval = Time.time - _lastTimeSpawn;

                if (interval >= _stats.Stats.AttacksPerSecond)
                {
                    _lastTimeSpawn = Time.time;
                    _player.TakeDamage(_stats.Stats.Damage);
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _stats.Stats.Speed * Time.deltaTime);
        }
        
        public override void Construct(Player player, Action<EnemyBase> obDiedCallBack, IProjectileSpawner spawner)
        {
            base.Construct(player, obDiedCallBack, spawner);
            _health = _stats.Stats.Health;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out _))
            {
                _lastTimeSpawn = Time.time;
                _playerInRange = true;
                _player.TakeDamage(_stats.Stats.Damage);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out _) && _playerInRange)
                _playerInRange = false;
        }
    }
}