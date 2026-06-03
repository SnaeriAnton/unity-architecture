using System;
using Leopotam.EcsLite;

namespace Game
{
    public class CrystalPickupSystem : IEcsRunSystem
    {
        private readonly Action _onCrystalPicked;

        public CrystalPickupSystem(Action onCrystalPicked) => _onCrystalPicked = onCrystalPicked;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<CrystalPickupRequest>().End();

            EcsPool<CrystalPickupRequest> requestPool = world.GetPool<CrystalPickupRequest>();

            foreach (int entity in filter)
            {
                ref CrystalPickupRequest request = ref requestPool.Get(entity);

                request.Crystal.PickUp();
                _onCrystalPicked.Invoke();

                world.DelEntity(entity);
            }
        }
    }
}