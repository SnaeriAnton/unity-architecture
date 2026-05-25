namespace Game
{
    public sealed class GameRuntimeCleanupValidationSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;
        private readonly EcsDiagnostics _diagnostics;

        public GameRuntimeCleanupValidationSystem(EcsWorld world, CommandBuffer commandBuffer, EcsDiagnostics diagnostics)
        {
            _world = world;
            _commandBuffer = commandBuffer;
            _diagnostics = diagnostics;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity request in _world.Filter().With<GameRuntimeCleanupValidationRequest>())
            {
                _diagnostics.ValidateRuntimeClean();
                _commandBuffer.DestroyEntity(request);
            }
        }
    }
}