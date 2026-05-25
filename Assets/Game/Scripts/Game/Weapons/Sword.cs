using UnityEngine;

namespace Game
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] private WeaponHitboxEcsLink _link;
        
        private GameApi _gameApi;
        
        public void Init(GameApi gameApi, float damage)
        {
            _gameApi = gameApi;

            if (!_link.IsRegistered)
            {
                Entity entity = _gameApi.RegisterWeaponHitbox(transform, damage);
                _link.Bind(entity);
            }
            
            else
            {
                _gameApi.RequestSetWeaponHitboxDamage(_link.Entity, damage);
            }
        }
        
        public void ResetEcsLink() => _link.Clear();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out EnemyEcsLink enemyLink)) return;
            if (!enemyLink.IsRegistered) return;
            if (!_link.IsRegistered) return;

            _gameApi.RequestWeaponHitboxHitEnemy(_link.Entity, enemyLink.Entity);
        }
    }
}
