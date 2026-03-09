using UnityEngine;

namespace Contracts
{
    public interface ITarget
    {
        public Transform Transform { get; }
        public bool IsDead { get; }
        
        public void TakeDamage(int damage);
    }
}