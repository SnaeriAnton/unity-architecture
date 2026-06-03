
using Leopotam.EcsLite;

namespace Game
{
    public class HudEcsApi
    {
        private readonly EcsWorld _world;

        public HudEcsApi(EcsWorld world) => _world = world;

        public void RequestRefresh()
        {
            int request = _world.NewEntity();
            _world.GetPool<HudRefreshRequest>().Add(request);
        }
    }
}