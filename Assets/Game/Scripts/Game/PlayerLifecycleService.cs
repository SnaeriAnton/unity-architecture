using UnityEngine;

namespace Game
{
    public class PlayerLifecycleService
    {
        private readonly PlayerApi _playerApi;
        private readonly PlayerDeathView _deathView;
        private readonly WeaponApi  _weaponApi;

        public PlayerLifecycleService(
            PlayerApi playerApi,
            PlayerDeathView deathView,
            WeaponApi  weaponApi)
        {
            _playerApi = playerApi;
            _deathView = deathView;
            _weaponApi = weaponApi;
        }

        public void Start() => _playerApi.Start();
        public void Stop() => _playerApi.Stop();

        public void Restart()
        {
            _playerApi.SetPosition(Vector2.zero);
            _playerApi.ResetInvulnerability();
            _playerApi.Revive();
            _deathView.ResetDeathView();
            _weaponApi.RequestResetWeapons();
        }
    }
}