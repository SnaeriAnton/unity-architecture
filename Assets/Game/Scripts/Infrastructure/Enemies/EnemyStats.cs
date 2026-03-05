using Domain;
using UnityEngine;

namespace Infrastructure
{
    public class EnemyStats : ScriptableObject
    {
        [field: SerializeField] public Stats Stats { get; private set; }
    }
}