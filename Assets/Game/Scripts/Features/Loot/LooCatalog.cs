using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Shared;

namespace Loot
{
    [CreateAssetMenu(fileName = "LooCatalog", menuName = "Micro Vampire/Loot catalog")]
    public class LooCatalog : ScriptableObject
    {
        [SerializeField] private List<LootData> _loots;

        public LootEntity GetLoot(CurrencyType type) => _loots.First(w=>w.Type == type).Template;
    }

    [Serializable]
    public struct LootData
    {
        public CurrencyType Type;
        public LootEntity Template;
    }
}
