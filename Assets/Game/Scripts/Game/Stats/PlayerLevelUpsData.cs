using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "PlayerLevelUpsData", menuName = "Micro Vampire/Level up/Player level ups data")]
    public class PlayerLevelUpsData : LevelUpsData<SpartanStats> { }
    
    [Serializable]
    public struct SpartanStats
    {
        public int Health;
    }
}
