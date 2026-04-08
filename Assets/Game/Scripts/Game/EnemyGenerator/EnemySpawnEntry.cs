using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "EnemySpawnEntry", menuName = "Micro Vampire/Enemy spawn entry")]
    public class EnemySpawnEntry : ScriptableObject
    {
        [field: SerializeField] public EnemyNames Type { get; private set; }
        [field: SerializeField] public EnemyView ViewPrefab { get; private set; }
        [field: SerializeField] public EnemyStats Stats { get; private set; }
    }
}