using UnityEngine;

namespace Infrastructure
{
    public class PlayerMovement
    {
        private readonly Transform _player;
        private readonly Border _board;
        private readonly float _speed;

        private Vector3 _newPosition;
        
        public PlayerMovement(Transform player, Border board, float speed)
        {
            _player = player;
            _board = board;
            _speed = speed;
        }
        
        public void OnAxis(Vector2 axis)
        {
            float speed = _speed * Time.deltaTime;
            axis = (axis * speed) + (Vector2)_player.position;
            float clampX = Mathf.Clamp(axis.x, (_board.Size.x / 2 * -1) + _player.localScale.x / 2, _board.Size.x / 2 - _player.localScale.x / 2);
            float clampY = Mathf.Clamp(axis.y, (_board.Size.y / 2 * -1) + _player.localScale.y / 2, _board.Size.y / 2 - _player.localScale.y / 2);
            _newPosition.x = clampX;
            _newPosition.y = clampY;
            _player.position = _newPosition;
        }
    }
}
