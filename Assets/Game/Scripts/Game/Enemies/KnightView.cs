using System;
using UnityEngine;

namespace Game
{
    public class KnightView : EnemyView
    {
        public event Action<Collider2D> OnTriggerExit;
        
        public override void SetPosition(Vector3 targetPosition, float speed, float dt) => transform.position =  Vector3.MoveTowards(transform.position, targetPosition, speed * dt);

        private void OnTriggerExit2D(Collider2D other) => OnTriggerExit?.Invoke(other);
    }
}