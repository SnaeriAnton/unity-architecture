using UnityEngine;

namespace Game
{
    public class AxeEcsLink : MonoBehaviour
    {
        public Entity Entity { get; private set; }
        public bool IsRegistered { get; private set; }

        public void Bind(Entity entity)
        {
            Entity = entity;
            IsRegistered = true;
        }

        public void Clear()
        {
            Entity = default;
            IsRegistered = false;
        }
    }
}