using UnityEngine;

namespace Runtime
{
    public class PlayerMovement
    {
        private readonly Transform _player;
        private readonly Border _border;
        private readonly float _speed;

        private Vector3 _newPosition;
        
        public PlayerMovement(Transform player, Border border, float speed)
        {
            _player = player;
            _border = border;
            _speed = speed;
        }
        
        public void OnAxis(float x, float y)
        {
            Vector2 axis = new(x, y);
            float speed = _speed * Time.deltaTime;
            axis = (axis * speed) + (Vector2)_player.position;
            float clampX = Mathf.Clamp(axis.x, (_border.Size.x / 2 * -1) + _player.localScale.x / 2, _border.Size.x / 2 - _player.localScale.x / 2);
            float clampY = Mathf.Clamp(axis.y, (_border.Size.y / 2 * -1) + _player.localScale.y / 2, _border.Size.y / 2 - _player.localScale.y / 2);
            _newPosition.x = clampX;
            _newPosition.y = clampY;
            _player.position = _newPosition;
        }
    }
}
