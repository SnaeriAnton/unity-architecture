using Application;
using UnityEngine;

namespace Infrastructure
{
    public class GameTime : IGameTime
    {
        public void Pause() => Time.timeScale = 0;

        public void Resume() => Time.timeScale = 1;
    }
}
