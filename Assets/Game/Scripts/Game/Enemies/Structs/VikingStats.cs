using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "VikingStats", menuName = "Micro Vampire/Enemies/Viking stats")]
    public class VikingStats : EnemyStats
    {
        [field: SerializeField] public AxeStats AxeStats { get; private set; }
        [field: SerializeField] public AxeView AxeTemplate { get; private set; }
    }
}