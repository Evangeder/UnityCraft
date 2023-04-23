using UnityEngine;

namespace UnityCraft.UI
{
    /// <summary>
    /// A base class for all the interface views.
    /// </summary>
    public abstract class BaseView : MonoBehaviour
    {
        /// <summary>
        /// Shows the view by setting the GameObject active and initializes proper data.
        /// </summary>
        public virtual void ShowView()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the view by setting the GameObject inactive.
        /// </summary>
        public virtual void HideView()
        {
            gameObject.SetActive(false);
        }
    }
}