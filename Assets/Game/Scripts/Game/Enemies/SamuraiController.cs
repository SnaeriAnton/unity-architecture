using UnityEngine;
using Contracts;

namespace Game
{
    public class SamuraiController : EnemyController
    {
        public SamuraiController(EnemyViewModel viewModel, EnemyView view, EnemyModel model, IEnemyDeathProcessor enemyDeathProcessor, ITarget target) : base(viewModel, view, model, enemyDeathProcessor, target)
        {
        }

        public override void Tick(float dt)
        {
            if (_target.IsDead) return;
            Vector3 position = Vector3.MoveTowards(_view.Position, _target.Position, _model.Stats.Speed * dt);
            _model.SetPosition(position);
            _viewModel.SetPosition(position);
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