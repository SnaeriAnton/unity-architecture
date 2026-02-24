using UnityEngine;

namespace Presentation
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Micro Vampire/Player stats")]
    public class PlayerStats : ScriptableObject
    {
        [field: SerializeField] public float IFramesDuration { get; private set; } = 0.5f;
        [field: SerializeField] public float Speed { get; private set; } = 2.5f;
    }
}
