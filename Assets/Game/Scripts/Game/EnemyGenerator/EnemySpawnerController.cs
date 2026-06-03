using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class EnemySpawnerController
    {
        private readonly GeneratorData _data;
        private readonly Transform _playerTransform;
        private readonly Border _border;
        private readonly Factory _factory;

        private GeneratorStage _currentStage;
        private bool _isSpawning;
        private int _currentStageIndex;

        public EnemySpawnerController(Transform playerTransform, GeneratorData data, Factory factory, Border border)
        {
            _playerTransform = playerTransform;
            _data = data;
            _factory = factory;
            _border = border;
        }

        public bool CanSpawn => _isSpawning && !_currentStage.Equals(default);
        public float CurrentSpawnInterval => _currentStage.SpawnInterval;

        public void Start()
        {
            _currentStage = _data.Stages[_currentStageIndex];
            _isSpawning = true;
        }

        public void Stop() => _isSpawning = false;
        
        public void Spawn()
        {
            if (_currentStage.Equals(default)) return;

            _factory.SpawnEnemy(_currentStage.Enemies[Random.Range(0, _currentStage.Enemies.Count)], _border.PickPoint(_playerTransform.position, _data.RadiusPlayer));
        }

        public void Reset()
        {
            _currentStageIndex = 0;
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