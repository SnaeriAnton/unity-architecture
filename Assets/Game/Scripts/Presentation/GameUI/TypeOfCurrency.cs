using System;
using System.Collections.Generic;
using Domain;
using UnityEngine;

namespace Presentation
{
    [CreateAssetMenu(fileName = "TypeOfCurrency", menuName = "Micro Vampire/Type of currency")]
    public class TypeOfCurrency : ScriptableObject
    {
        [SerializeField] private List<CurrencyDescription> _currencyDescriptions;

        public Sprite GetSprite(CurrencyType type)
        {
            foreach (CurrencyDescription currency in _currencyDescriptions)
                if (currency.Type == type) return currency.Icon;
            
            throw new Exception($"{type} is not a currency");
        }
    }

    [Serializable]
    public class CurrencyDescription
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public CurrencyType Type { get; private set; }
    }
}
