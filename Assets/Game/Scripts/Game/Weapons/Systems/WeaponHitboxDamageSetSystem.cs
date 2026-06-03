using Leopotam.EcsLite;

namespace Game
{
    public class WeaponHitboxDamageSetSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world
                .Filter<WeaponHitboxDamageSetRequest>()
                .End();

            EcsPool<WeaponHitboxDamageSetRequest> requestPool =
                world.GetPool<WeaponHitboxDamageSetRequest>();

            EcsPool<Damage> damagePool =
                world.GetPool<Damage>();

            foreach (int requestEntity in filter)
            {
                ref WeaponHitboxDamageSetRequest request =
                    ref requestPool.Get(requestEntity);

                if (damagePool.Has(request.WeaponHitbox))
                {
                    ref Damage damage = ref damagePool.Get(request.WeaponHitbox);
                    damage.Value = request.Damage;
                }

                world.DelEntity(requestEntity);
            }
        }
    }
}