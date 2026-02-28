using UnityEngine;

namespace Shared
{
    public interface ILookFactory
    {
        public void CreateLoot(CurrencyType type, Vector3 position);
    }
}
