using System;
using UnityEngine;
using Contracts;

namespace Game
{
    public class SamuraiPresenter : EnemyPresenter
    {
        public SamuraiPresenter(EnemyView view, EnemyModel model, IEnemyDeathProcessor enemyDeathProcessor, ITarget target) : base(view, model, enemyDeathProcessor, target)
        {
        }

        public override void Tick(float dt)
        {
            if (_target.IsDead) return;
            _view.SetPosition(_target.Position, _model.Stats.Speed, dt);
        }

        protected override void OnTrigger(Collider2D other)
        {
            if (other.TryGetComponent<ITarget>(out _))
            {
                _target.TakeDamage(_model.Stats.Damage);
                Die();
            }
        }
    }
}