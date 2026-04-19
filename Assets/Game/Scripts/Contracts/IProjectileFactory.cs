using Game;
using UnityEngine;

namespace Contracts
{
    public interface IProjectileFactory
    {
        public Axe SpawnAxe(Axe template, Vector3 position, Quaternion rotate);
    }
}