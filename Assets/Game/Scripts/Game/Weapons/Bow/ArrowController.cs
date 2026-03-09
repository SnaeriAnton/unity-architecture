using System;
using UnityEngine;
using Contracts;

namespace Game
{
    public class ArrowController : ProjectileController<ArrowModel, ArrowView>
    {
        private float _liveTime;

        public ArrowController(ArrowModel model, ArrowView view) : base(model, view)
        {
            _view.OnHit += OnHit;
            _liveTime = _model.Stats.LifeTime;
        }

        public event Action<ArrowController> OnDisable;

        public override void Update()
        {
            base.Update();

            _liveTime -= Time.deltaTime;
            _model.Move();

            if (_liveTime <= 0)
            {
                _model.Disable();
                _view.OnHit -= OnHit;
                OnDisable?.Invoke(this);
            }
        }

        private void OnHit(IEnemyTarget target)
        {
            target.TakeDamage(_model.Stats.Damage);
            _model.Disable();
            OnDisable?.Invoke(this);
            _view.OnHit -= OnHit;
        }
    }
}