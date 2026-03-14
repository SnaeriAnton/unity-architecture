using UnityEngine;

namespace Game
{
    public class GameTime : IGamePauseService
    {
        public void Pause() => Time.timeScale = 0;

        public void Resume() => Time.timeScale = 1;
    }
}