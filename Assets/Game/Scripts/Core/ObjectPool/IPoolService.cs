using UnityEngine;

namespace Core.Pool
{
    public interface IPoolService
    {
        public T Spawn<T>(T prefab, Vector3 pos, Quaternion qua) where T : Component, IPoolable;

        public void Despawn<T>(T obj) where T : Component, IPoolable;
    }
}