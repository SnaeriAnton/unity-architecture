using System;
using Contracts;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public class SamuraiView : EnemyView
    {
        private void Update()
        {
            if (_model.Target.IsDead) return;
            
            transform.position = Vector3.MoveTowards(transform.position, _model.Target.Transform.position, _model.Stats.Stats.Speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ITarget>(out _))
            {
                _model.OnHit();
                _model.Die();
            }
        }
    }
}