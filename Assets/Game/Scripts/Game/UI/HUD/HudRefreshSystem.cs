using Leopotam.EcsLite;

namespace Game
{
    public class HudRefreshSystem : IEcsRunSystem
    {
        private readonly GameUiBridge _gameUiBridge;

        public HudRefreshSystem(GameUiBridge gameUiBridge) => _gameUiBridge = gameUiBridge;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<HudRefreshRequest>().End();

            foreach (int request in filter)
            {
                _gameUiBridge.RefreshHud();
                world.DelEntity(request);
            }
        }
    }
}