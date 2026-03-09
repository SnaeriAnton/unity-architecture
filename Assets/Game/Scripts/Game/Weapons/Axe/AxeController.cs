using System;
using Contracts;
using UnityEngine;

namespace Game
{
    public class AxeController : ProjectileController<AxeModel, AxeView>
    {
        private float _liveTime;

        public AxeController(AxeModel model, AxeView view) : base(model, view)
        {
            _view.OnHit += OnHit;
            _liveTime = _model.Stats.LifeTime;
        }

        public event Action<AxeController> OnDisable;

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