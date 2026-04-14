using UnityEngine;

namespace Game
{
    public class UIRoot : MonoBehaviour
    {
        [field: SerializeField] public HUDView HudView { get; private set; }
        [field: SerializeField] public LoseScreenView LoseScreenView{ get; private set; }
        [field: SerializeField] public UpgradeWindowView UpgradeWindowView{ get; private set; }
        [field: SerializeField] public MenuScreenView MenuScreenView{ get; private set; }
    }
}