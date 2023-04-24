using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.MainMenu.Views
{
    using Components;

    public class MultiplayerView : BaseView
    {
        [field: SerializeField]
        public Button Return { get; private set; }
        [field: SerializeField]
        public Serverlist Serverlist { get; private set; }
    }
}