namespace Game
{
    public class HudEcsApi
    {
        private readonly CommandBuffer _commandBuffer;

        public HudEcsApi(CommandBuffer commandBuffer) => _commandBuffer = commandBuffer;

        public void RequestRefresh()
        {
            Entity request = _commandBuffer.CreateEntity();
            _commandBuffer.Add(request, new HudRefreshRequest());
        }
    }
}