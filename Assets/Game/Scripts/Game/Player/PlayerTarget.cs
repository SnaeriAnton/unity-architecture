using Contracts;
using UnityEngine;

namespace Game
{
    public class PlayerTarget : MonoBehaviour, ITarget
    {
        private PlayerModel _model;
        private PlayerDamageHandler _handler;

        public void Construct(PlayerModel model, PlayerDamageHandler handler)
        {
            _model = model;
            _handler = handler;
        }

        public Vector3 Position => _model.Position;
        public bool IsDead => _model.IsDead;
        
        public void TakeDamage(int damage) => _handler.TakeDamage(damage);
    }
}