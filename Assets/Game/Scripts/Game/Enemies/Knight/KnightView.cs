using System;
using Contracts;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public class KnightView : EnemyView
    {
        private KnightModel _knightModel;

        public virtual void Init(EnemyModel model)
        {
            base.Init(model);
            _knightModel = model as KnightModel;
        }

        protected virtual void Update()
        {
            if (_model.Target.IsDead) return;

            if (_knightModel.PlayerInRange)
            {
                float interval = Time.time - _knightModel.LastTimeSpawn;

                if (interval >= _model.Stats.Stats.AttacksPerSecond)
                    _model.OnHit();
            }

            transform.position = Vector3.MoveTowards(transform.position, _model.Target.Transform.position, _model.Stats.Stats.Speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ITarget>(out _))
                _model.OnHit();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<ITarget>(out _) && _knightModel.PlayerInRange)
                _knightModel.OnStopHit();
        }
    }
}