using System;
using UnityEngine;
using Shared;

namespace Enemies
{
    public class Viking : Enemy<VikingStats>
    {
        private float _currentAxeInterval;

        private void Update()
        {
            if (_target.IsDead) return;
            _currentAxeInterval -= Time.deltaTime;

            if (_currentAxeInterval <= 0)
            {
                _currentAxeInterval = _stats.AxeStats.AttacksPerSecond;
                ShootAxe();
            }
        }
        
        public override void Construct(ITarget target, Action<EnemyBase> obDiedCallBack, IObjectPool pool)
        {
            base.Construct(target, obDiedCallBack, pool);
            _currentAxeInterval = _stats.AxeStats.AttacksPerSecond;
            _health = _stats.Stats.Health;
        }

        private void ShootAxe()
        {
            Vector2 direction = _target.Position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Axe axe = _pool.Spawn(_stats.AxeTemplate, transform.position, Quaternion.Euler(0f, 0f, angle));
            axe.Init(_stats.AxeStats, direction.normalized);
        }
    }
}