using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "GeneratorData", menuName = "Micro Vampire/Generator data")]
    public class GeneratorData : ScriptableObject
    {
        [SerializeField] private List<GeneratorStage> _stages;
        
        [field: SerializeField] public float RadiusPlayer { get; private set; } = 3f;
        public IReadOnlyList<GeneratorStage> Stages => _stages;
    }
}