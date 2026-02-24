using UnityEngine;

namespace Runtime
{
    public abstract class Enemy<TStats> : EnemyBase where TStats : EnemyStats
    {
        [SerializeField] protected TStats _stats;
    }
}