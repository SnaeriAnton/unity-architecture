using System;
using UnityEngine;

namespace Infrastructure
{
    public class Samurai : Enemy<SamuraiStats>
    {
        private void Update()
        {
            if (_player.IsDead) return;
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _stats.Stats.Speed * Time.deltaTime);
        }

        public override void Construct(Player player, Action<EnemyBase> obDiedCallBack, PoolManager spawner)
        {
            base.Construct(player, obDiedCallBack, spawner);
            _health = _stats.Stats.Health;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out _))
            {
                _player.TakeDamage(_stats.Stats.Damage);
                Die();
            }
        }
    }
}