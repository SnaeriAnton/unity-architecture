using Application;
using Domain;
using Runtime;
using UnityEngine;

namespace Presentation
{
    public class PlayerSessionAdapter : IPlayerSession
    {
        private readonly Player _player;

        public PlayerSessionAdapter(Player player) => _player = player;
        
        public bool HasWeapon(Weapons weapon) => _player.HasWeapon(weapon);
        public void StartPlay() => _player.StartPlay();
        public void Restart() => _player.Restart();
        public void SetPlayerStats(SpartanStats stats) => _player.SetPlayerStats(stats);
        public void SetWeaponStats(Weapons name, WeaponStats stats) => _player.SetWeaponStats(name, stats);
    }
}
