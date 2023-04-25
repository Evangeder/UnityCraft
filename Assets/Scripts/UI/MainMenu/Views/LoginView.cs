using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.MainMenu.Views
{
    using Networking.API;

    public class LoginView : BaseView
    {
        [field: SerializeField]
        public Button Login { get; private set; }
        [field: SerializeField]
        public Button Return { get; private set; }
        [field: SerializeField]
        public InputField LoginField { get; private set; }
        [field: SerializeField]
        public InputField PasswordField { get; private set; }
        [field: SerializeField]
        public Text TextLoginFailed { get; private set; }
    }
}