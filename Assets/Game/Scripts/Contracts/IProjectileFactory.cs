using Game;
using UnityEngine;

namespace Contracts
{
    public interface IProjectileFactory
    {
        public Axe SpawnAxe(Vector3 position, Quaternion rotate);
    }
}