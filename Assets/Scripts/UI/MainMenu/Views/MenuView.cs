using UnityCraft.Networking.API;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.MainMenu.Views
{
    public class MenuView : BaseView
    {
        [field: SerializeField]
        public Button Singleplayer { get; private set; }

        [field: SerializeField]
        public Button Multiplayer { get; private set; }

        [field: SerializeField]
        public Button Settings { get; private set; }

        [field: SerializeField]
        public Button QuitButton { get; private set; }
    }
}