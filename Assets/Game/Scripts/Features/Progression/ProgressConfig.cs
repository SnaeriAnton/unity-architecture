using UnityEngine;

namespace Progression
{
    [CreateAssetMenu(fileName = "ProgressConfig", menuName = "Micro Vampire/Progress config")]
    public class ProgressConfig : ScriptableObject
    {
        [field: SerializeField] public float ExperienceMultiplier { get; private set; } = 1.5f;
        [field: SerializeField] public int LevelUpStageStep { get; private set; } = 3;
        [field: SerializeField] public int ExperienceBeforeLevelUp { get; private set; } = 5;
    }
}