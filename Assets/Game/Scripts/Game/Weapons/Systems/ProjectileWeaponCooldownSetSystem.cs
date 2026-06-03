using Leopotam.EcsLite;

namespace Game
{
    public class ProjectileWeaponCooldownSetSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<ProjectileWeaponCooldownSetRequest>().End();

            EcsPool<ProjectileWeaponCooldownSetRequest> requestPool = world.GetPool<ProjectileWeaponCooldownSetRequest>();

            EcsPool<ProjectileWeaponAttack> attackPool = world.GetPool<ProjectileWeaponAttack>();

            foreach (int requestEntity in filter)
            {
                ref ProjectileWeaponCooldownSetRequest request = ref requestPool.Get(requestEntity);

                if (attackPool.Has(request.Weapon))
                {
                    ref ProjectileWeaponAttack attack = ref attackPool.Get(request.Weapon);

                    attack.Cooldown = request.Cooldown;

                    if (attack.Timer > attack.Cooldown)
                        attack.Timer = attack.Cooldown;
                }

                world.DelEntity(requestEntity);
            }
        }
    }
}