using System.Collections.Generic;
using UnityEngine;
using Shared;

namespace GameFlow
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Micro Vampire/Game config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private List<Weapons> _startWeapons;
        [field: SerializeField] public int StartCrystalValues { get; private set; } = 0;
        [field: SerializeField] public int StartCoinValues { get; private set; } = 0;
        
        public IReadOnlyList<Weapons> StartWeapons => _startWeapons;
    }
}