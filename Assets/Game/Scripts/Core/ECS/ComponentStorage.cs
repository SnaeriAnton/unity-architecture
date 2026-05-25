using System.Collections.Generic;

namespace Game
{
    public interface IComponentStorage
    {
        public IEnumerable<int> EntityIds { get; }
        public int Count { get; }
        
        public bool Has(int entityId);
        public void Remove(int entityId);
    }

    public class ComponentStorage<T> : IComponentStorage where T : struct
    {
        private readonly Dictionary<int, T> _components = new();

        public IEnumerable<int> EntityIds => _components.Keys;
        public int Count => _components.Count;

        public void Add(int entityId, T component) => _components.Add(entityId, component);
        public void Set(int entityId, T component) => _components[entityId] = component;
        public T Get(int entityId) => _components[entityId];
        public bool Has(int entityId) => _components.ContainsKey(entityId);
        public void Remove(int entityId)=> _components.Remove(entityId);
    }
}