using System;

namespace Shared
{
    public class LootEvents
    {
        public event Action CoinPicked;
        public event Action CrystalPicked;

        public void RaiseCoinPicked() => CoinPicked?.Invoke();
        public void RaiseCrystalPicked() => CrystalPicked?.Invoke();
    }
}
