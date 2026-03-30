using Domain;

namespace Application
{
    public readonly struct UpgradeButtonViewData
    {
        public readonly Weapons Name;
        public readonly CurrencyType Type;
        public readonly int CountLevels;
        public readonly int Price;
        public readonly int CurrentLevel;

        public UpgradeButtonViewData(
            Weapons name, 
            CurrencyType type,
            int countLevels, 
            int price, 
            int currentLevel
            )
        {
            Name = name;
            Type = type;
            CountLevels = countLevels;
            Price = price;
            CurrentLevel = currentLevel;
        }
    }
}
