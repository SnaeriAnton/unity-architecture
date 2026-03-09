using Contracts;
using UnityEngine;

namespace Game
{
    public class KnightModel : EnemyModel
    {
        public KnightModel(EnemyStats stats, ITarget target) : base(stats, target)
        {
        }
        
        public bool PlayerInRange { get; private set; }
        public float LastTimeSpawn { get; private set; }

        public void OnHit()
        {
            LastTimeSpawn = Time.time;
            PlayerInRange = true;
            Target.TakeDamage(Stats.Stats.Damage);
        }

        public void OnStopHit()
        {
            PlayerInRange = false;
        }
    }
}