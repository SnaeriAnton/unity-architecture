using UnityEngine;

namespace Game
{
    public class Spears : Weapon
    {
        [SerializeField] private Spear _spearTemplate;
        
        private Entity _projectileWeaponEntity;
        private bool _isRegistered;
        
        public override void SetStats(WeaponStats stats)
        {
            base.SetStats(stats);

            if (!_isRegistered)
            {
                _projectileWeaponEntity = _gameApi.RegisterProjectileWeapon(_stats.AttacksPerSecond, SpawnProjectileByEcs);
                _isRegistered = true;
            }
            else
            {
                _gameApi.RequestSetProjectileWeaponCooldown(_projectileWeaponEntity, _stats.AttacksPerSecond);
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
                Spear spear = _poolManager.Spawn(_spearTemplate, transform.position, Quaternion.Euler(0f, 0f, angle));
                spear.Init(_gameApi, _stats, direction);
            }
        }
    }
}