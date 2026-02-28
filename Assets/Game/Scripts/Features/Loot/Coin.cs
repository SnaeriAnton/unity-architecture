using Shared;

namespace Loot
{
    public class Coin : LootEntity
    {
        public override void PickUp()
        {
            GameEvents.Loot.RaiseCoinPicked();
            base.PickUp();
        }
    }
}
