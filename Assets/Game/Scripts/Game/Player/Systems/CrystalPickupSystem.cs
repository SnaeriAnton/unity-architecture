using System;
using System.Collections.Generic;

namespace Game
{
    public class CrystalPickupSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;
        private readonly Action _onCrystalPicked;

        public CrystalPickupSystem(EcsWorld world, CommandBuffer commandBuffer, Action onCrystalPicked)
        {
            _world = world;
            _commandBuffer = commandBuffer;
            _onCrystalPicked = onCrystalPicked;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity entity in _world.Filter().With<CrystalPickupRequest>())
            {
                CrystalPickupRequest request = _world.Get<CrystalPickupRequest>(entity);

                request.Crystal.PickUp();
                _onCrystalPicked.Invoke();
                _commandBuffer.DestroyEntity(entity);

            }
        }
    }
}