using UnityEngine;

namespace Game
{
    public class GameTimeService
    {
        public void Pause() => Time.timeScale = 0f;
        public void Resume() => Time.timeScale = 1f;
    }
}