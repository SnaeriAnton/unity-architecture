using UnityEngine;

namespace Enemies
{
    public class EnemyStats : ScriptableObject
    {
        [field: SerializeField] public Stats Stats { get; private set; }
    }
}