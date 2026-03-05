using UnityEngine;

namespace Application
{
    public readonly struct UpgradeInfo
    {
        public readonly int NameID;
        public readonly int CurrencyID;
        public readonly Sprite Icon;
        public readonly int Price;
        public readonly int CurrentLevel;
        public readonly int MaxLevels;

        public UpgradeInfo(int nameID, int currencyID, Sprite icon, int price, int currentLevel, int maxLevels)
        {
            NameID = nameID;
            CurrencyID = currencyID;
            Icon = icon;
            Price = price;
            CurrentLevel = currentLevel;
            MaxLevels = maxLevels;
        }
    }
}