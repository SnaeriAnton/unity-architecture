using System;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public class Knight : Enemy<KnightStats>
    {
        private bool _playerInRange;
        private float _lastTimeSpawn;

        protected virtual void Update()
        {
            if (Player.IsDead) return;

            if (_playerInRange)
            {
                float interval = Time.time - _lastTimeSpawn;

                if (interval >= _stats.Stats.AttacksPerSecond)
                {
                    _lastTimeSpawn = Time.time;
                    Player.TakeDamage(_stats.Stats.Damage);
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, _stats.Stats.Speed * Time.deltaTime);
        }
        
        public override void Construct(Action<EnemyBase> obDiedCallBack)
        {
            base.Construct( obDiedCallBack);
            _health = _stats.Stats.Health;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out _))
            {
                _lastTimeSpawn = Time.time;
                _playerInRange = true;
                Player.TakeDamage(_stats.Stats.Damage);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out _) && _playerInRange)
                _playerInRange = false;
        }
    }
}