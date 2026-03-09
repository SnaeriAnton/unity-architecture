using System;
using Shared;

namespace Loot
{
    [Serializable]
    public struct LootData
    {
        public CurrencyType Type;
        public LootEntity Template;
    }
}