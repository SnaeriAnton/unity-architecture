using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class CurrencyDescription
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public CurrencyType Type { get; private set; }
    }
}