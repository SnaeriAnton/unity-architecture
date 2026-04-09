using UnityEngine;
using Contracts;
using R3;

namespace Game
{
    public class KnightController : EnemyController
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly KnightModel _knightModel;

        public KnightController(EnemyViewModel viewModel, EnemyView view, EnemyModel model, IEnemyDeathProcessor enemyDeathProcessor, ITarget target, IUpdateStream updateStream)
            : base(viewModel, view, model, enemyDeathProcessor, target, updateStream)
        {
            _knightModel = model as KnightModel;   
            (_view as KnightView).OnTriggerExit.Subscribe(OnTriggerExit).AddTo(_disposable);
        }

        protected override void Tick(float dt)
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
            
            Vector3 position = Vector3.MoveTowards(_view.Position, _target.Position, _model.Stats.Speed * dt);
            _model.SetPosition(position);
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
            _disposable.Dispose();
            base.Die();
        }

        private void OnTriggerExit(Collider2D other)
        {
            if (other.TryGetComponent<ITarget>(out _) && _knightModel.PlayerInRange)
                _knightModel.ChangePlayerInRange(false);
        }
    }
}