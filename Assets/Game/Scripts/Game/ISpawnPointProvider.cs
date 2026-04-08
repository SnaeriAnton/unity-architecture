using UnityEngine;

namespace Contracts
{
    public interface ISpawnPointProvider
    {
        public Vector3 PickPoint(Vector3 playerPosition, float radiusPlayer);
    }
}