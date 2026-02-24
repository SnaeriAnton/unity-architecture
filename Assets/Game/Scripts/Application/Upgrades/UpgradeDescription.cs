using Domain;

namespace Application
{
    public readonly struct UpgradeDescription<TStats> where TStats : struct
    {
        public readonly CurrencyType Type;
        public readonly int Price;
        public readonly TStats Stats;

        public UpgradeDescription(CurrencyType type, int price, TStats stats)
        {
            Type = type;
            Price = price;
            Stats = stats;
        }
    }
}