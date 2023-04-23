using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.Shared.Views
{
    public class ControlsView : BaseView
    {
        [field: SerializeField]
        public Button Forward { get; private set; }
        [field: SerializeField]
        public Button Left { get; private set; }
        [field: SerializeField]
        public Button Back { get; private set; }
        [field: SerializeField]
        public Button Right { get; private set; }
        [field: SerializeField]
        public Button Jump { get; private set; }
        [field: SerializeField]
        public Button Drop { get; private set; }
        [field: SerializeField]
        public Button Inventory { get; private set; }
        [field: SerializeField]
        public Button Chat { get; private set; }
        [field: SerializeField]
        public Button ToggleFog { get; private set; }
        [field: SerializeField]
        public Button SaveLocation { get; private set; }
        [field: SerializeField]
        public Button LoadLocation { get; private set; }
        [field: SerializeField]
        public Button Save { get; private set; }
    }
}