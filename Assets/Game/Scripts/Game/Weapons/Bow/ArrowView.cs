using Contracts;
using UnityEngine;

namespace Game
{
    public class ArrowView : ProjectileView
    {
        private ArrowModel _model;

        public override void Init(ProjectileModel model)
        {
            _model = model as ArrowModel;
            _model.OnStateChanged += Move;
            _model.OnDisable += Disable;
        }

        public override void Disable()
        {
            _model.OnStateChanged -= Move;
            _model.OnDisable -= Disable;
            base.Disable();
        }

        private void Move() => transform.position = Vector3.MoveTowards(transform.position, transform.position + _model.Direction, _model.Stats.FlightSpeed * Time.deltaTime);
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEnemyTarget target))
                InvokeOnHit(target);
        }
    }
}