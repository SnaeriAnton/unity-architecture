using System.Collections.Generic;
using Domain;

namespace Application
{
    public readonly struct GameSettings
    {
        private readonly List<Weapons> _startWeapons;
        
        public readonly int StartCrystalValues;
        public readonly int StartCoinValues;
        
        public IReadOnlyList<Weapons> StartWeapons => _startWeapons;

        public GameSettings(IReadOnlyList<Weapons> startWeapons, int startCrystalValues, int startCoinValues)
        {
            _startWeapons = new(startWeapons);
            StartCrystalValues = startCrystalValues;
            StartCoinValues = startCoinValues;
        }
    }
}
