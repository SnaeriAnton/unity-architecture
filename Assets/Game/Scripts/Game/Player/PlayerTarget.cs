using Contracts;
using UnityEngine;

namespace Game
{
    public class PlayerTarget : MonoBehaviour, ITarget
    {
        private PlayerModel _model;
        private PlayerDamageHandler _handler;
        private PlayerView _view;

        public void Construct(PlayerModel model, PlayerView view, PlayerDamageHandler handler)
        {
            _model = model;
            _view = view;
            _handler = handler;
        }

        public Vector3 Position => _view.Position;
        public bool IsDead => _model.IsDead;
        
        public void TakeDamage(int damage) => _handler.TakeDamage(damage);
    }
}