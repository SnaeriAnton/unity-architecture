using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class ProjectileWeaponAttackSystem : IEcsRunSystem
    {
        private readonly PlayerApi _playerApi;

        public ProjectileWeaponAttackSystem(PlayerApi playerApi) => _playerApi = playerApi;

        public void Run(IEcsSystems systems)
        {
            if (!_playerApi.HasPlayingTag())
                return;

            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<ProjectileWeaponTag>().Inc<ProjectileWeaponAttack>().Inc<ProjectileWeaponSpawnRef>().End();

            EcsPool<ProjectileWeaponAttack> attackPool = world.GetPool<ProjectileWeaponAttack>();
            EcsPool<ProjectileWeaponSpawnRef> spawnPool = world.GetPool<ProjectileWeaponSpawnRef>();

            float deltaTime = Time.deltaTime;

            foreach (int weapon in filter)
            {
                ref ProjectileWeaponAttack attack = ref attackPool.Get(weapon);

                attack.Timer -= deltaTime;

                if (attack.Timer > 0f)
                    continue;

                ref ProjectileWeaponSpawnRef spawnRef = ref spawnPool.Get(weapon);
                spawnRef.Spawn.Invoke();

                attack.Timer = attack.Cooldown;
            }
        }
    }
}