using UnityEngine;

namespace Shared
{
    public interface IWorldBounds
    {
        public Vector2 Size { get; }

        public Vector3 PickPoint(Vector3 playerPosition, float radiusPlayer);
    }
}
