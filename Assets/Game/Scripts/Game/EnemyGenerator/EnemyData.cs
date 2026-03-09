using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Micro Vampire/Enemy data")]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public EnemyView EnemyTemplate { get; private set; }
        [field: SerializeField] public EnemyStats Stats { get; private set; }
    }
}