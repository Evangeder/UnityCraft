using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.MainMenu.Views
{
    public class GenerateLevelView : BaseView
    {
        [field: SerializeField]
        public Button LevelType { get; private set; }

        [field: SerializeField]
        public Button LevelShape { get; private set; }

        [field: SerializeField]
        public Button LevelSize { get; private set; }

        [field: SerializeField]
        public Button LevelTheme { get; private set; }

        [field: SerializeField]
        public Button Create { get; private set; }

        [field: SerializeField]
        public Button Cancel { get; private set; }
    }
}