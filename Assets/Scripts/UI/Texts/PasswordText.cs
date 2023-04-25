using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.Texts
{
    [RequireComponent(typeof(Shadow))]
    public class PasswordText : Text
    {
        public override string text
        {
            get
            {
                return m_Text;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(m_Text))
                        return;
                    m_Text = "";
                    SetVerticesDirty();
                }
                else if (m_Text != value)
                {
                    m_Text = new string('*', value.Length);
                    SetVerticesDirty();
                    SetLayoutDirty();
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            var shadow = GetComponent<Shadow>();
            shadow.effectDistance = new Vector2(2, -2);
            shadow.effectColor = new Color32(96, 96, 96, 255);
        }
    }
}