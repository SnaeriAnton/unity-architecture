using System;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
    public class EcsFilter : IEnumerable<Entity>
    {
        private readonly EcsWorld _world;
        private readonly List<Type> _with = new();
        private readonly List<Type> _without = new();

        public EcsFilter(EcsWorld world) => _world = world;

        public EcsFilter With<T>() where T : struct
        {
            _with.Add(typeof(T));
            return this;
        }

        public EcsFilter Without<T>() where T : struct
        {
            _without.Add(typeof(T));
            return this;
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            if (_with.Count == 0)
                throw new InvalidOperationException("EcsFilter must contain at least one With<T>().");

            Type baseType = GetSmallestStorageType();
            int[] entityIds = _world.GetEntityIds(baseType);

            for (int i = 0; i < entityIds.Length; i++)
            {
                int entityId = entityIds[i];

                if (!HasAllRequiredComponents(entityId)) continue;
                if (HasAnyExcludedComponent(entityId)) continue;

                yield return new Entity(entityId);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private bool HasAllRequiredComponents(int entityId)
        {
            for (int i = 1; i < _with.Count; i++)
                if (!_world.Has(entityId, _with[i])) return false;

            return true;
        }

        private bool HasAnyExcludedComponent(int entityId)
        {
            for (int i = 0; i < _without.Count; i++)
                if (_world.Has(entityId, _without[i])) return true;

            return false;
        }
        
        private Type GetSmallestStorageType()
        {
            Type smallestType = _with[0];
            int smallestCount = _world.CountEntitiesWith(smallestType);

            for (int i = 1; i < _with.Count; i++)
            {
                Type type = _with[i];
                int count = _world.CountEntitiesWith(type);

                if (count < smallestCount)
                {
                    smallestType = type;
                    smallestCount = count;
                }
            }

            return smallestType;
        }
    }
}