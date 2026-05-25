using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class EcsWorld
    {
        private int _nextEntityId = 1;

        private readonly Dictionary<Type, IComponentStorage> _storages = new();
        private readonly HashSet<int> _aliveEntities = new();

        public int AliveEntitiesCount => _aliveEntities.Count;
        
        public EcsFilter Filter() => new(this);
        public bool Has(int entityId, Type componentType) => _storages.TryGetValue(componentType, out IComponentStorage storage) && storage.Has(entityId);
        public bool IsAlive(Entity entity) => _aliveEntities.Contains(entity.Id);
        
        public int CountEntitiesWith(Type componentType)
        {
            if (!_storages.TryGetValue(componentType, out IComponentStorage storage))
                return 0;

            return storage.Count;
        }
        
        public T Get<T>(Entity entity) where T : struct
        {
            EnsureAlive(entity);

            ComponentStorage<T> storage = GetStorage<T>();

            if (!storage.Has(entity.Id))
                throw new InvalidOperationException(
                    $"Entity {entity.Id} does not have component {typeof(T).Name}.");

            return storage.Get(entity.Id);
        }
        
        public void Set<T>(Entity entity, T component) where T : struct
        {
            EnsureAlive(entity);

            ComponentStorage<T> storage = GetStorage<T>();

            if (!storage.Has(entity.Id))
                throw new InvalidOperationException(
                    $"Entity {entity.Id} does not have component {typeof(T).Name}.");

            storage.Set(entity.Id, component);
        }
        
        public void Add<T>(Entity entity, T component) where T : struct
        {
            EnsureAlive(entity);

            ComponentStorage<T> storage = GetStorage<T>();

            if (storage.Has(entity.Id))
                throw new InvalidOperationException(
                    $"Entity {entity.Id} already has component {typeof(T).Name}.");

            storage.Add(entity.Id, component);
        }
        
        private void EnsureAlive(Entity entity)
        {
            if (!_aliveEntities.Contains(entity.Id))
                throw new InvalidOperationException($"Entity {entity.Id} is not alive.");
        }
        
        public Entity CreateEntity()
        {
            Entity entity = new Entity(_nextEntityId++);
            _aliveEntities.Add(entity.Id);

            return entity;
        }
        
        public void DestroyEntity(Entity entity)
        {
            if (!_aliveEntities.Contains(entity.Id))
                return;

            foreach (IComponentStorage storage in _storages.Values)
                storage.Remove(entity.Id);

            _aliveEntities.Remove(entity.Id);
        }
        
        public void Remove<T>(Entity entity) where T : struct
        {
            if (!_aliveEntities.Contains(entity.Id))
                return;

            GetStorage<T>().Remove(entity.Id);
        }
        
        public int[] GetEntityIds(Type componentType)
        {
            if (!_storages.TryGetValue(componentType, out IComponentStorage storage))
                return Array.Empty<int>();

            return storage.EntityIds.ToArray();
        }

        private ComponentStorage<T> GetStorage<T>() where T : struct
        {
            Type type = typeof(T);

            if (_storages.TryGetValue(type, out IComponentStorage storage))
                return (ComponentStorage<T>)storage;

            ComponentStorage<T> newStorage = new();
            _storages.Add(type, newStorage);

            return newStorage;
        }
        
        private bool TryGetStorage<T>(out ComponentStorage<T> storage) where T : struct
        {
            if (_storages.TryGetValue(typeof(T), out IComponentStorage rawStorage))
            {
                storage = (ComponentStorage<T>)rawStorage;
                return true;
            }

            storage = null;
            return false;
        }
        
        public bool Has<T>(Entity entity) where T : struct
        {
            if (!IsAlive(entity))
                return false;

            if (!TryGetStorage<T>(out ComponentStorage<T> storage))
                return false;

            return storage.Has(entity.Id);
        }
        
        public bool TryGet<T>(Entity entity, out T component) where T : struct
        {
            component = default;

            if (!IsAlive(entity))
                return false;

            if (!TryGetStorage<T>(out ComponentStorage<T> storage))
                return false;

            if (!storage.Has(entity.Id))
                return false;

            component = storage.Get(entity.Id);
            return true;
        }
        
        public int CountEntitiesWith<T>() where T : struct
        {
            if (!TryGetStorage<T>(out ComponentStorage<T> storage))
                return 0;

            return storage.Count;
        }
    }
}