using UnityEngine;

namespace Game
{
    public class Bow : Weapon
    {
        [SerializeField] private Arrow _arrowTemplate;
        
        private int _projectileWeaponEntity;
        private bool _isRegistered;
        
        public override void SetStats(WeaponStats stats)
        {
            base.SetStats(stats);

            if (!_isRegistered)
            {
                _projectileWeaponEntity = ProjectileWeaponApi.RegisterProjectileWeapon(_stats.AttacksPerSecond, SpawnProjectileByEcs);
                _isRegistered = true;
            }
            else
            {
                ProjectileWeaponApi.RequestSetProjectileWeaponCooldown(_projectileWeaponEntity, _stats.AttacksPerSecond);
            }
        }
        
        public override void Reset()
        {
            base.Reset();

            _projectileWeaponEntity = default;
            _isRegistered = false;
        }
        
        private void SpawnProjectileByEcs()
        {
            if (_stats.Equals(default)) return;

            for (int i = 0; i < _stats.Count; i++)
            {
                Vector2 direction = Random.insideUnitCircle.normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Arrow arrow = _poolManager.Spawn(_arrowTemplate, transform.position, Quaternion.Euler(0f, 0f, angle));
                arrow.Init(ProjectileApi, _stats, direction);
            }
        }
    }
}