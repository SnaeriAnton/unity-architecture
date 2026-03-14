using System;
using Contracts;
using UnityEngine;

namespace Game
{
    public abstract class EnemyPresenter : ITickable, IEnemyTarget
    {
        protected readonly EnemyView _view;
        protected readonly EnemyModel _model;
        protected readonly ITarget _target;

        private readonly IEnemyDeathProcessor _enemyDeathProcessor;

        public EnemyPresenter(EnemyView view, EnemyModel model, IEnemyDeathProcessor enemyDeathProcessor, ITarget target)
        {
            _view = view;
            _model = model;
            _enemyDeathProcessor = enemyDeathProcessor;
            _target = target;
            _view.OnTrigger += OnTrigger;
            _model.OnDie += Die;
        }


        public abstract void Tick(float dt);

        protected virtual void OnTrigger(Collider2D other)
        {
            if (other.TryGetComponent<ITarget>(out _))
            {
                _target.TakeDamage(_model.Stats.Damage);
                Die();
            }
        }

        protected virtual void Die()
        {
            _enemyDeathProcessor.Handle(_view, this);
            _view.OnTrigger -= OnTrigger;
            _model.OnDie -= Die;
            _view.Die();
        }

        public void TakeDamage(float damage) => _model.TakeDamage(damage);
    }
}