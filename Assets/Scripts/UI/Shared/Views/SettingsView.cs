using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.Shared.Views
{
    public class SettingsView : BaseView
    {
        [field: SerializeField]
        public Button Music { get; private set; }

        [field: SerializeField]
        public Button Sfx { get; private set; }

        [field: SerializeField]
        public Button InvertMouse { get; private set; }

        [field: SerializeField]
        public Button ShowFps { get; private set; }

        [field: SerializeField]
        public Button RenderDistance { get; private set; }

        [field: SerializeField]
        public Button ViewBobbing { get; private set; }

        [field: SerializeField]
        public Button Anaglyph { get; private set; }

        [field: SerializeField]
        public Button LimitFramerate { get; private set; }

        [field: SerializeField]
        public Button Difficulty { get; private set; }

        [field: SerializeField]
        public Button Controls { get; private set; }

        [field: SerializeField]
        public Button Save { get; private set; }
    }
}