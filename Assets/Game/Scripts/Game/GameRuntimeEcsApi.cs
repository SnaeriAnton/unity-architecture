namespace Game
{
    public class GameRuntimeEcsApi
    {
        private readonly CommandBuffer _commandBuffer;

        public GameRuntimeEcsApi(CommandBuffer commandBuffer) => _commandBuffer = commandBuffer;

        public void RequestCleanup()
        {
            Entity request = _commandBuffer.CreateEntity();
            _commandBuffer.Add(request, new GameRuntimeCleanupRequest());
        }
    }
}