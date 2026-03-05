using UnityEngine;
using Random = UnityEngine.Random;
using Shared;

namespace Enemies
{
    public class EnemySpawnerController : ITickable, IEnemySpawner
    {
        private readonly GeneratorData _data;
        private readonly EnemiesFactory _factory;
        private readonly EnemyDeathHandler _handler;
        private readonly ITarget _target;
        private readonly IWorldBounds _border;

        private GeneratorStage _currentStage;
        private bool _isSpawning;
        private int _currentStageIndex;
        private float _spawnTimer;

        public EnemySpawnerController(ITarget target, GeneratorData data, EnemyDeathHandler handler, EnemiesFactory factory, IWorldBounds border)
        {
            _target = target;
            _data = data;
            _factory = factory;
            _handler = handler;
            _border = border;
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
            _factory.SpawnEnemy(_currentStage.Enemies[Random.Range(0, _currentStage.Enemies.Count)], _handler.Handle, _border.PickPoint(_target.Position, _data.RadiusPlayer));
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