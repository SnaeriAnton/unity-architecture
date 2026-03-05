using UnityEngine;
using Shared;

namespace Player
{
    public class PlayerMovement
    {
        private readonly Transform _player;
        private readonly IWorldBounds _border;
        private readonly float _speed;

        private Vector3 _newPosition;
        
        public PlayerMovement(Transform player, IWorldBounds border, float speed)
        {
            _player = player;
            _border = border;
            _speed = speed;
        }
        
        public void OnAxis(Vector2 axis)
        {
            float speed = _speed * Time.deltaTime;
            axis = (axis * speed) + (Vector2)_player.position;
            float x = Mathf.Clamp(axis.x, (_border.Size.x / 2 * -1) + _player.localScale.x / 2, _border.Size.x / 2 - _player.localScale.x / 2);
            float y = Mathf.Clamp(axis.y, (_border.Size.y / 2 * -1) + _player.localScale.y / 2, _border.Size.y / 2 - _player.localScale.y / 2);
            _newPosition.x = x;
            _newPosition.y = y;
            _player.position = _newPosition;
        }
    }
}
