using UnityEngine;

namespace Game
{
    public class SamuraiView : EnemyView
    {
        public override void SetPosition(Vector3 targetPosition, float speed, float dt) => transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * dt);
    }
}