using UnityEngine;

namespace Shared
{
    public readonly struct UpgradeInfo
    {
        public readonly Weapons Name;
        public readonly CurrencyType CurrencyType;
        public readonly Sprite Icon;
        public readonly int Price;
        public readonly int CurrentLevel;
        public readonly int MaxLevels;
        
        public UpgradeInfo(Weapons name, CurrencyType currencyType, Sprite icon, int price, int currentLevel, int maxLevels)
        {
            Name = name;
            CurrencyType = currencyType;
            Icon = icon;
            Price = price;
            CurrentLevel = currentLevel;
            MaxLevels = maxLevels;
        }
    }
}
