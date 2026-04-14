using UnityEngine;
using Contracts;
using Zenject;

namespace Game
{
    public class EnemyDamageReceiver : MonoBehaviour, IEnemyTarget
    {
        private IEnemyTarget _target;
        public void Construct(IEnemyTarget target) => _target = target;
        public void TakeDamage(float damage) => _target.TakeDamage(damage);
    }
}