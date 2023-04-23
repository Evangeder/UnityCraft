using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.Shared.Views
{
    public class ErrorView : BaseView
    {
        [field: SerializeField]
        public Text Message { get; private set; }
    }
}