using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class LevelUpDescription<TStats> where TStats : struct
    {
        [field: SerializeField] public CurrencyType Type { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public TStats Stats { get; private set; }
    }
}