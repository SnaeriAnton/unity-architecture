using UnityEngine;
using Shared;

namespace Weapons
{
    public class Sword : MonoBehaviour
    {
        private float _damage;
        
        public void SetDamage(float damage) => _damage = damage;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damage);
        }
    }
}
