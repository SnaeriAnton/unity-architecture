using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure
{
    public class EnemySpawnerController : ITickable
    {
        private readonly GeneratorData _data;
        private readonly Player _player;
        private readonly Border _board;
        private readonly Factory _factory;
        private readonly EnemyDeathHandler _handler;

        private GeneratorStage _currentStage;
        private bool _isSpawning;
        private int _currentStageIndex;
        private float _spawnTimer;

        public EnemySpawnerController(Player player, GeneratorData data, EnemyDeathHandler handler, Factory factory, Border board)
        {
            _player = player;
            _data = data;
            _factory = factory;
            _handler = handler;
            _board = board;
            _spawnTimer = 0f;
        }

        public void Start()
        {
            _currentStage = _data.Stages[_currentStageIndex];
            _isSpawning = true;
        }

        public void Stop() => _isSpawning = false;

        public void Tick()
        {
            if (!_isSpawning) return;
            _spawnTimer += Time.deltaTime;

            if (_spawnTimer < _currentStage.SpawnInterval) return;

            _spawnTimer = 0f;
            _factory.SpawnEnemy(_currentStage.Enemies[Random.Range(0, _currentStage.Enemies.Count)], _handler.Handle, _board.PickPoint(_player.transform.position, _data.RadiusPlayer));
        }

        public void Reset()
        {
            _currentStageIndex = 0;
            _spawnTimer = 0f;
            _currentStage = _data.Stages[_currentStageIndex];
        }

        public void LevelUp()
        {
            if (_currentStageIndex < _data.Stages.Count - 1)
            {
                _currentStageIndex++;
                _currentStage = _data.Stages[_currentStageIndex];
            }
        }
    }
}