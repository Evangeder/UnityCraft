using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.MainMenu.Views
{
    public class SingleplayerView : BaseView
    {
        [field: SerializeField]
        public Button World1 { get; private set; }
        [field: SerializeField]
        public Button World2 { get; private set; }
        [field: SerializeField]
        public Button World3 { get; private set; }
        [field: SerializeField]
        public Button World4 { get; private set; }
        [field: SerializeField]
        public Button World5 { get; private set; }
        [field: SerializeField]
        public Button Return { get; private set; }
    }
}