using Leopotam.EcsLite;

namespace Game
{
    public class GameRuntimeEcsApi
    {
        private readonly EcsWorld _world;

        public GameRuntimeEcsApi(EcsWorld world) => _world = world;

        public void RequestCleanup()
        {
            int request = _world.NewEntity();
            _world.GetPool<LeoGameRuntimeCleanupRequest>().Add(request);
        }
    }
}