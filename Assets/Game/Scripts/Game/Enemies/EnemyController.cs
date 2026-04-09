using System;
using UnityEngine;
using Contracts;
using R3;

namespace Game
{
    public abstract class EnemyController : IEnemyTarget, IDisposable
    {
        protected readonly EnemyView _view;
        protected readonly EnemyModel _model;
        protected readonly EnemyViewModel _viewModel;
        protected readonly ITarget _target;
        protected readonly IUpdateStream _updateStream;

        private readonly CompositeDisposable _disposable = new();
        private readonly IEnemyDeathProcessor _enemyDeathProcessor;

        public EnemyController(EnemyViewModel viewModel, EnemyView view, EnemyModel model, IEnemyDeathProcessor enemyDeathProcessor, ITarget target, IUpdateStream updateStream)
        {
            _viewModel = viewModel;
            _view = view;
            _model = model;
            _enemyDeathProcessor = enemyDeathProcessor;
            _target = target;
            _updateStream = updateStream;
            _view.OnTrigger.Subscribe(OnTrigger).AddTo(_disposable);
            _updateStream.OnUpdate.Subscribe(Tick).AddTo(_disposable);
            _model.IsDead.Skip(1).Subscribe(_ => Die()).AddTo(_disposable);
        }

        public void TakeDamage(float damage) => _model.TakeDamage(damage);
        public void Dispose() => _disposable?.Dispose();

        protected abstract void Tick(float dt);

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
            _enemyDeathProcessor.Handle(_view);
            Dispose();
        }
    }
}