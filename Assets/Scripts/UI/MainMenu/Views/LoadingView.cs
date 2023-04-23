using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.MainMenu.Views
{
    public class LoadingView : BaseView
    {
        [field: SerializeField]
        public Image ProgressBar { get; private set; }

        [field: SerializeField]
        public Text ActionText { get; private set; }

        [field: SerializeField]
        public Text CurrentState { get; private set; }
    }
}