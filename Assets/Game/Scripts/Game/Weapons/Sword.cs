using UnityEngine;

namespace Game
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] private WeaponHitboxEcsLink _link;
        
        private WeaponHitboxApi _weaponHitboxApi;
        
        public void Init(WeaponHitboxApi weaponHitboxApi, float damage)
        {
            _weaponHitboxApi = weaponHitboxApi;

            if (!_link.IsRegistered)
            {
                int entity = _weaponHitboxApi.RegisterWeaponHitbox(transform, damage);
                _link.Bind(entity);
            }
            else
            {
                _weaponHitboxApi.RequestSetWeaponHitboxDamage(_link.Entity, damage);
            }
        }
        
        public void ResetEcsLink() => _link.Clear();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out EnemyEcsLink enemyLink)) return;
            if (!enemyLink.IsRegistered) return;
            if (!_link.IsRegistered) return;

            _weaponHitboxApi.RequestWeaponHitboxHitEnemy(_link.Entity, enemyLink.Entity);
        }
    }
}
