using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "VikingStats", menuName = "Micro Vampire/Enemies/Viking stats")]
    public class VikingStats : EnemyStats
    {
        [field: SerializeField] public AxeStats AxeStats { get; private set; }
        [field: SerializeField] public Axe AxeTemplate { get; private set; }
    }
}