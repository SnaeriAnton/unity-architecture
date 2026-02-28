using System;
using System.Collections.Generic;
using Shared;
using UnityEngine;

namespace Upgrades
{
    public abstract class LevelUpsData<TStats> : ScriptableObject where TStats : struct
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Weapons Name { get; private set; }
        
        [SerializeField] private List<LevelUpDescription<TStats>> _levelUps;
        
        public IReadOnlyList<LevelUpDescription<TStats>> LevelUps => _levelUps;
        public int Count => _levelUps.Count;
    }
    
    [Serializable]
    public class LevelUpDescription<TStats> where TStats : struct
    {
        [field: SerializeField] public CurrencyType Type { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public TStats Stats { get; private set; }
    }
}