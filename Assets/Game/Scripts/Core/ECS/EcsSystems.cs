using System.Collections.Generic;

namespace Game
{
    public class EcsSystems
    {
        private readonly List<IEcsRunSystem> _systems = new();

        public EcsSystems(EcsWorld world) => CommandBuffer = new CommandBuffer(world);
        
        public CommandBuffer CommandBuffer { get; }
        
        public void Add(IEcsRunSystem system) => _systems.Add(system);

        public void Run(float deltaTime)
        {
            CommandBuffer.Playback();
            
            for (int i = 0; i < _systems.Count; i++)
            {
                _systems[i].Run(deltaTime);
                CommandBuffer.Playback();
            }
        } 
    }
}