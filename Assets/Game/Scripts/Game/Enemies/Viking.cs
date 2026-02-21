using System;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public class Viking : Enemy<VikingStats>
    {
        private float _currentAxeInterval;

        private void Update()
        {
            if (Player.IsDead) return;
            _currentAxeInterval -= Time.deltaTime;

            if (_currentAxeInterval <= 0)
            {
                _currentAxeInterval = _stats.AxeStats.AttacksPerSecond;
                ShootAxe();
            }
        }
        
        public override void Construct(Action<EnemyBase> obDiedCallBack)
        {
            base.Construct( obDiedCallBack);
            _currentAxeInterval = _stats.AxeStats.AttacksPerSecond;
            _health = _stats.Stats.Health;
        }

        private void ShootAxe()
        {
            Vector2 direction = Player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Axe axe = Pool.Spawn(_stats.AxeTemplate, transform.position, Quaternion.Euler(0f, 0f, angle));
            axe.Init(_stats.AxeStats, direction.normalized);
        }
    }
}