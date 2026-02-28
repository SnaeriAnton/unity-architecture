using Shared;
using UnityEngine;

namespace Loot
{
    public class LootFactory : ILookFactory
    {
        private readonly LooCatalog _catalog;
        private readonly IObjectPool _pool;

        public LootFactory(LooCatalog catalog, IObjectPool pool)
        {
            _catalog = catalog;
            _pool = pool;
        }

        public void CreateLoot(CurrencyType type, Vector3 position) =>
            _pool.Spawn(_catalog.GetLoot(type), position, Quaternion.identity);
    }
}
