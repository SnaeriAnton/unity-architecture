using System.Linq;

namespace Game
{
    public class ProjectileWeaponAttackSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;

        public ProjectileWeaponAttackSystem(EcsWorld world) => _world = world;

        public void Run(float deltaTime)
        {
            if (!_world.Filter().With<PlayingTag>().Any()) return;
            
            foreach (Entity weapon in _world.Filter().With<ProjectileWeaponTag>().With<ProjectileWeaponAttack>().With<ProjectileWeaponSpawnRef>())
            {
                ProjectileWeaponAttack attack = _world.Get<ProjectileWeaponAttack>(weapon);
                attack.Timer -= deltaTime;

                if (attack.Timer <= 0f)
                {
                    ProjectileWeaponSpawnRef spawnRef = _world.Get<ProjectileWeaponSpawnRef>(weapon);

                    spawnRef.Spawn.Invoke();
                    attack.Timer = attack.Cooldown;
                }

                _world.Set(weapon, attack);
            }
        }
    }
}