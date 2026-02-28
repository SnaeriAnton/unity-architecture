using Shared;

namespace Loot
{
    public class Crystal : LootEntity
    {
        public override void PickUp()
        {
            GameEvents.Loot.RaiseCrystalPicked();
            base.PickUp();
        }
    }
}