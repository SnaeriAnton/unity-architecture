using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "EnemyDropConfig", menuName = "Micro Vampire/Enemy drop config")]
    public class EnemyDropConfig : ScriptableObject
    {
        [field: SerializeField] public Coin CoinTemplate { get; private set; }
        [field: SerializeField] public Crystal CrystalTemplate { get; private set; }
        [field: SerializeField] public float CoinsChanceOnSpawn { get; private set; } = 0.1f;
        [field: SerializeField] public float CrystalsChanceOnSpawn { get; private set; } = 0.8f;
    }
}