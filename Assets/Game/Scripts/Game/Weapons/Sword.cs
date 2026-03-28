using Contracts;
using UnityEngine;

namespace Game
{
    public class Sword : MonoBehaviour
    {
        private float _damage;
        
        public void SetDamage(float damage) => _damage = damage;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEnemyTarget enemy))
                enemy.TakeDamage(_damage);
        }
    }
}
