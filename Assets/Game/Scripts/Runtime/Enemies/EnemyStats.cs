using Domain;
using UnityEngine;

namespace Runtime
{
    public class EnemyStats : ScriptableObject
    {
        [field: SerializeField] public Stats Stats { get; private set; }
    }
}