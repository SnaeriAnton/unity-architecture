using UnityEngine;

namespace Game
{
    public sealed class EcsDiagnostics
    {
        private readonly EcsWorld _world;

        public EcsDiagnostics(EcsWorld world) => _world = world;

        public int AliveEntitiesCount => _world.AliveEntitiesCount;
        
        public void LogRuntimeState()
        {
            Debug.Log(
                $"ECS Runtime:" +
                $"\nEnemy: {_world.CountEntitiesWith<EnemyTag>()}" +
                $"\nAxe: {_world.CountEntitiesWith<AxeTag>()}" +
                $"\nProjectile: {_world.CountEntitiesWith<ProjectileTag>()}" +
                $"\nRuntime: {_world.CountEntitiesWith<GameRuntimeTag>()}" +
                $"\nDamageRequest: {_world.CountEntitiesWith<DamageRequest>()}" +
                $"\nEnemyDamageRequest: {_world.CountEntitiesWith<EnemyDamageRequest>()}"
            );
        }
        
        public void ValidateRuntimeClean()
        {
            int runtime = _world.CountEntitiesWith<GameRuntimeTag>();
            int enemy = _world.CountEntitiesWith<EnemyTag>();
            int axe = _world.CountEntitiesWith<AxeTag>();
            int projectile = _world.CountEntitiesWith<ProjectileTag>();

            int damageRequest = _world.CountEntitiesWith<DamageRequest>();
            int enemyDamageRequest = _world.CountEntitiesWith<EnemyDamageRequest>();

            if (runtime > 0)
                Debug.LogError($"[ECS] Runtime entities leak: {runtime}");

            if (enemy > 0)
                Debug.LogError($"[ECS] Enemy entities leak: {enemy}");

            if (axe > 0)
                Debug.LogError($"[ECS] Axe entities leak: {axe}");

            if (projectile > 0)
                Debug.LogError($"[ECS] Projectile entities leak: {projectile}");

            if (damageRequest > 0)
                Debug.LogError($"[ECS] DamageRequest leak: {damageRequest}");

            if (enemyDamageRequest > 0)
                Debug.LogError($"[ECS] EnemyDamageRequest leak: {enemyDamageRequest}");
        }
    }
}