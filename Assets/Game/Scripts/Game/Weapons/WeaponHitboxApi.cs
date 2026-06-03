using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class WeaponHitboxApi
    {
        private readonly EcsWorld _world;

        public WeaponHitboxApi(EcsWorld world) => _world = world;

        public int RegisterWeaponHitbox(Transform transform, float damage)
        {
            int entity = _world.NewEntity();

            _world.GetPool<WeaponHitboxTag>().Add(entity);

            ref WeaponHitboxViewRef viewRef = ref _world
                .GetPool<WeaponHitboxViewRef>()
                .Add(entity);

            viewRef.Transform = transform;

            ref Damage damageComponent = ref _world
                .GetPool<Damage>()
                .Add(entity);

            damageComponent.Value = damage;

            return entity;
        }

        public void RequestWeaponHitboxHitEnemy(int weaponHitbox, int enemy)
        {
            int request = _world.NewEntity();

            ref WeaponHitboxHitEnemyRequest data = ref _world
                .GetPool<WeaponHitboxHitEnemyRequest>()
                .Add(request);

            data.WeaponHitbox = weaponHitbox;
            data.Enemy = enemy;
        }

        public void RequestSetWeaponHitboxDamage(int weaponHitbox, float damage)
        {
            int request = _world.NewEntity();

            ref WeaponHitboxDamageSetRequest data = ref _world
                .GetPool<WeaponHitboxDamageSetRequest>()
                .Add(request);

            data.WeaponHitbox = weaponHitbox;
            data.Damage = damage;
        }
    }
}