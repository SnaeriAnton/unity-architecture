using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class LevelUpsData<TStats> : ScriptableObject where TStats : struct
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Weapons Name { get; private set; }
        
        [SerializeField] private List<LevelUpDescription<TStats>> _levelUps;
        
        public IReadOnlyList<LevelUpDescription<TStats>> LevelUps => _levelUps;
        public int Count => _levelUps.Count;
    }
}