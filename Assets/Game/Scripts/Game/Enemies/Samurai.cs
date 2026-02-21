using System;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public class Samurai : Enemy<SamuraiStats>
    {
        private void Update()
        {
            if (Player.IsDead) return;
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, _stats.Stats.Speed * Time.deltaTime);
        }

        public override void Construct(Action<EnemyBase> obDiedCallBack)
        {
            base.Construct(obDiedCallBack);
            _health = _stats.Stats.Health;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out _))
            {
                Player.TakeDamage(_stats.Stats.Damage);
                Die();
            }
        }
    }
}