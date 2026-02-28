using UnityEngine;

namespace Shared
{
    public interface ITarget
    {
        public Vector3 Position { get; }
        public bool IsDead { get; }

        public void TakeDamage(int damage);
    }
}
