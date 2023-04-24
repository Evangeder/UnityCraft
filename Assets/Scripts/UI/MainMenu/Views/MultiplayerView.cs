using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.MainMenu.Views
{
    public class MultiplayerView : BaseView
    {
        [field: SerializeField]
        public Button CreateNewWorld { get; private set; }
        [field: SerializeField]
        public Button LoadWorld { get; private set; }
        [field: SerializeField]
        public Button Return { get; private set; }
    }
}