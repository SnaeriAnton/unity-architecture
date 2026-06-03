using Leopotam.EcsLite;

namespace Game
{
    public class PlayerDeathSystem : IEcsRunSystem
    {
        private readonly PlayerDeathService _playerDeathService;

        public PlayerDeathSystem(PlayerDeathService playerDeathService) => _playerDeathService = playerDeathService;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<PlayerTag>().Inc<DeadTag>().Exc<DeathHandledTag>().End();

            EcsPool<DeathHandledTag> handledPool = world.GetPool<DeathHandledTag>();
            EcsPool<PlayingTag> playingPool = world.GetPool<PlayingTag>();

            foreach (int player in filter)
            {
                if (playingPool.Has(player))
                    playingPool.Del(player);

                if (!handledPool.Has(player))
                    handledPool.Add(player);

                _playerDeathService.HandleDeath();
            }
        }
    }
}