using UnityEngine;

namespace Game
{
    public class AxeViewSyncSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;

        public AxeViewSyncSystem(EcsWorld world) => _world = world;

        public void Run(float deltaTime)
        {
            foreach (Entity axe in _world.Filter().With<AxeTag>().With<Position>().With<Rotation>().With<AxeViewRef>())
            {
                Position position = _world.Get<Position>(axe);
                AxeViewRef viewRef = _world.Get<AxeViewRef>(axe);
                Rotation rotationRef = _world.Get<Rotation>(axe);

                viewRef.Axe.transform.position = new Vector3(
                    position.Value.x,
                    position.Value.y,
                    viewRef.Axe.transform.position.z);

                viewRef.Axe.transform.Rotate(0f, 0f, rotationRef.Value.z, Space.Self);
            }
        }
    }
}