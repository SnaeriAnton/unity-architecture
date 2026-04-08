using UnityEngine;

namespace Game
{
    public class PlayerMovement
    {
        private readonly Border _board;
        private readonly float _speed;

        private Vector3 _newPosition;
        private Vector2 _currentDirection;

        public PlayerMovement(Border board, float speed)
        {
            _board = board;
            _speed = speed;
        }

        public void OnAxis(Vector2 axis) => _currentDirection = axis;

        public Vector3 GetPosition(Vector3 position, Vector2 scale, float dt)
        {
            float speed = _speed * dt;
            Vector2 next = (Vector2)position + _currentDirection * speed;
            float x = Mathf.Clamp(next.x, (_board.Size.x / 2 * -1) + scale.x, _board.Size.x / 2 - scale.x);
            float y = Mathf.Clamp(next.y, (_board.Size.y / 2 * -1) + scale.y, _board.Size.y / 2 - scale.y);
            _newPosition.x = x;
            _newPosition.y = y;
            return _newPosition;
        }
    }
}