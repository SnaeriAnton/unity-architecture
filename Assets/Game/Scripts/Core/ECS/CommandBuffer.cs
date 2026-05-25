using System;
using System.Collections.Generic;

namespace Game
{
    public class CommandBuffer
    {
        private readonly List<Action> _commands = new();
        private readonly EcsWorld _world;

        public CommandBuffer(EcsWorld world) => _world = world;

        public Entity CreateEntity()
        {
            Entity entity = _world.CreateEntity();
            return entity;
        }

        public void Add<T>(Entity entity, T component) where T : struct => _commands.Add(() => _world.Add(entity, component));
        public void Set<T>(Entity entity, T component) where T : struct => _commands.Add(() => _world.Set(entity, component));

        public void Remove<T>(Entity entity) where T : struct
        {
            _commands.Add(() =>
            {
                if (!_world.IsAlive(entity)) return;

                if (_world.Has<T>(entity))
                    _world.Remove<T>(entity);
            });
        }
        
        public void AddIfMissing<T>(Entity entity, T component) where T : struct
        {
            _commands.Add(() =>
            {
                if (!_world.IsAlive(entity)) return;

                if (!_world.Has<T>(entity))
                    _world.Add(entity, component);
            });
        }
        
        public void DestroyEntity(Entity entity)
        {
            _commands.Add(() =>
            {
                if (_world.IsAlive(entity))
                    _world.DestroyEntity(entity);
            });
        }
        
        public void Playback()
        {
            for (int i = 0; i < _commands.Count; i++)
                _commands[i].Invoke();

            _commands.Clear();
        }
    }
}