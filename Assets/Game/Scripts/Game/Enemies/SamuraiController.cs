using UnityEngine;
using Contracts;

namespace Game
{
    public class SamuraiController : EnemyController
    {
        public SamuraiController(EnemyViewModel viewModel, EnemyView view, EnemyModel model, IEnemyDeathProcessor enemyDeathProcessor, ITarget target, IUpdateStream updateStream) : 
            base(viewModel, view, model, enemyDeathProcessor, target, updateStream)
        { }

        protected override void Tick(float dt)
        {
            if (_target.IsDead) return;
            Vector3 position = Vector3.MoveTowards(_view.Position, _target.Position, _model.Stats.Speed * dt);
            _model.SetPosition(position);
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