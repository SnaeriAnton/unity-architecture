using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "GeneratorData", menuName = "Micro Vampire/Generator data")]
    public class GeneratorData : ScriptableObject
    {
        [SerializeField] private List<GeneratorStage> _stages;
        
        [field: SerializeField] public float CoinsChanceOnSpawn { get; private set; } = 0.8f;
        [field: SerializeField] public float CrystalsChanceOnSpawn { get; private set; } = 0.1f;
        [field: SerializeField] public float RadiusPlayer { get; private set; } = 3f;
        public IReadOnlyList<GeneratorStage> Stages => _stages;
    }
}