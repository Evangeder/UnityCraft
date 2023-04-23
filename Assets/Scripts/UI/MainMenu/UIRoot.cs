using UnityEngine;

namespace UnityCraft.UI.MainMenu
{
    using Views;
    using Shared.Views;

    public class UIRoot : MonoBehaviour
    {
        [field: SerializeField]
        public MenuView MenuView { get; private set; }

        [field: SerializeField]
        public LoadingView LoadingView { get; private set; }

        [field: SerializeField]
        public SettingsView SettingsView { get; private set; }

        [field: SerializeField]
        public ControlsView ControlsView { get; private set; }

        [field: SerializeField]
        public SingleplayerView SingleplayerView { get; private set; }

        [field: SerializeField]
        public GenerateLevelView GenerateLevelView { get; private set; }

        [field: SerializeField]
        public MultiplayerView MultiplayerView { get; private set; }
    }
}