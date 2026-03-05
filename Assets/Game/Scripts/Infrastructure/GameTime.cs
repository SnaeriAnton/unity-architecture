using UnityEngine;

namespace Infrastructure
{
    public class GameTime
    {
        public void Pause() => Time.timeScale = 0;

        public void Resume() => Time.timeScale = 1;
    }
}
