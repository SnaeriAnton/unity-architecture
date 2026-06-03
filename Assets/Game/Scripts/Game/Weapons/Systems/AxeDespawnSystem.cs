using Leopotam.EcsLite;

namespace Game
{
    public class AxeDespawnSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<AxeTag>().Inc<AxeViewRef>().Inc<AxeDespawnRequestedTag>().End();

            EcsPool<AxeViewRef> viewRefPool = world.GetPool<AxeViewRef>();

            foreach (int axe in filter)
            {
                ref AxeViewRef viewRef = ref viewRefPool.Get(axe);

                if (viewRef.Axe != null)
                {
                    AxeEcsLink link = viewRef.Axe.GetComponent<AxeEcsLink>();

                    if (link != null)
                        link.Clear();

                    viewRef.Axe.DespawnByEcs();
                }

                world.DelEntity(axe);
            }
        }
    }
}