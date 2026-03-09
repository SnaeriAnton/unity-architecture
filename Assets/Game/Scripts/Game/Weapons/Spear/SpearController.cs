using System;
using Contracts;
using UnityEngine;

namespace Game
{
    public class SpearController : ProjectileController<SpearModel, SpearView>
    {
        private float _liveTime;

        public SpearController(SpearModel model, SpearView view) : base(model, view)
        {
            _view.OnHit += OnHit;
            _liveTime = _model.Stats.LifeTime;
        }

        public event Action<SpearController> OnDisable;

        public override void Update()
        {
            base.Update();

            _liveTime -= Time.deltaTime;
            _model.Move();

            if (_liveTime <= 0)
            {
                _model.Disable();
                OnDisable?.Invoke(this);
            }
        }

        private void OnHit(IEnemyTarget target)
        {
            target.TakeDamage(_model.Stats.Damage);
            _model.ChangeStrength();

            if (_model.CurrentStrength == 0)
            {
                _model.Disable();
                OnDisable?.Invoke(this);
                _view.OnHit -= OnHit;
            }
        }
    }
}