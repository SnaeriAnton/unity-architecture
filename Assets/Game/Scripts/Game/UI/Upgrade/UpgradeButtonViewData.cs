using UnityEngine;

namespace Game
{
    public readonly struct UpgradeButtonViewData
    {
        public readonly Sprite CurrencyIcon;
        public readonly Sprite WeaponIcon;
        public readonly Weapons Name;
        public readonly string LevelText;
        public readonly bool IsMax;
        public readonly bool IsUnlock;
        public readonly int Price;

        public UpgradeButtonViewData(
            Sprite weaponIcon,
            Sprite currencyIcon, 
            Weapons name, 
            string levelText, 
            bool isMax, 
            bool isUnlock, 
            int price
            )
        {
            WeaponIcon = weaponIcon;
            CurrencyIcon = currencyIcon;
            Name = name;
            LevelText = levelText;
            IsMax = isMax;
            IsUnlock = isUnlock;
            Price = price;
        }
    }
}