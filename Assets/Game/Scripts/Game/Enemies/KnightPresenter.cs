using System;
using Contracts;
using UnityEngine;

namespace Game
{
    public class KnightPresenter : EnemyPresenter
    {
        private readonly KnightModel _knightModel;

        public KnightPresenter(EnemyView view, EnemyModel model, IEnemyDeathProcessor enemyDeathProcessor, ITarget target)
            : base(view, model, enemyDeathProcessor, target)
        {
            _knightModel = model as KnightModel;   
            (_view as KnightView).OnTriggerExit += OnTriggerExit;
        }

        public override void Tick(float dt)
        {
            if (_target.IsDead) return;

            if (_knightModel.PlayerInRange)
            {
                _knightModel.SetAttackTimer(_knightModel.AttackTimer + dt);
                if (_knightModel.AttackTimer >= _model.Stats.AttacksPerSecond)
                {
                    _knightModel.SetAttackTimer(0);
                    _target.TakeDamage(_model.Stats.Damage);
                }
            }

            _view.SetPosition(_target.Position, _model.Stats.Speed, dt);
        }

        protected override void OnTrigger(Collider2D other)
        {
            if (other.TryGetComponent<ITarget>(out _))
            {
                _knightModel.SetAttackTimer(0);
                _knightModel.ChangePlayerInRange(true);
                _target.TakeDamage(_model.Stats.Damage);
            }
        }

        protected override void Die()
        {
            (_view as KnightView).OnTriggerExit -= OnTriggerExit;
            base.Die();
        }

        private void OnTriggerExit(Collider2D other)
        {
            if (other.TryGetComponent<ITarget>(out _) && _knightModel.PlayerInRange)
                _knightModel.ChangePlayerInRange(false);
        }
    }
}