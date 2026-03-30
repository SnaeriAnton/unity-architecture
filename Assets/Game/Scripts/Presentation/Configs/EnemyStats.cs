using Domain;
using UnityEngine;

namespace Presentation
{
    public class EnemyStats : ScriptableObject
    {
        [field: SerializeField] public Stats Stats { get; private set; }
    }
}