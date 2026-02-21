using Core.GSystem;
using UnityEngine;

namespace Game
{
    public class PlayerMovement
    {
        private readonly Transform _player;
        private readonly float _speed;
        
        private Border _board;
        private Vector3 _newPosition;
        
        public PlayerMovement(Transform player, float speed)
        {
            _player = player;
            _speed = speed;
        }
        
        private Border Board => _board ??= G.Main.Resolve<Border>();
        
        public void OnAxis(Vector2 axis)
        {
            float speed = _speed * Time.deltaTime;
            axis = (axis * speed) + (Vector2)_player.position;
            float x = Mathf.Clamp(axis.x, (Board.Size.x / 2 * -1) + _player.localScale.x / 2, Board.Size.x / 2 - _player.localScale.x / 2);
            float y = Mathf.Clamp(axis.y, (Board.Size.y / 2 * -1) + _player.localScale.y / 2, Board.Size.y / 2 - _player.localScale.y / 2);
            _newPosition.x = x;
            _newPosition.y = y;
            _player.position = _newPosition;
        }
    }
}
