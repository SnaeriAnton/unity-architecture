using System.Collections.Generic;
using Domain;

namespace Application
{
    public readonly struct GameSettings
    {
        private readonly List<Weapons> _startWeapons;

        public GameSettings(IReadOnlyList<Weapons> startWeapons) => _startWeapons = new(startWeapons);
        
        public IReadOnlyList<Weapons> StartWeapons => _startWeapons;
    }
}
