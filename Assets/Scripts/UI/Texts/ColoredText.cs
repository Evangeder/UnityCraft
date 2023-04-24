using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.Texts
{
    [RequireComponent(typeof(Shadow))]
    public class ColoredText : Text
    {
        private static string COLOR_END_TAG = "</color>";
        private Dictionary<char, string> colors = new()
        {
            { '0', "<color=#000000FF>" },
            { '1', "<color=#0000BFFF>" },
            { '2', "<color=#00BF00FF>" },
            { '3', "<color=#00BFBFFF>" },
            { '4', "<color=#BF0000FF>" },
            { '5', "<color=#BF00BFFF>" },
            { '6', "<color=#BFBF00FF>" },
            { '7', "<color=#BFBFBFFF>" },
            { '8', "<color=#404040FF>" },
            { '9', "<color=#4040FFFF>" },
            { 'a', "<color=#40FF40FF>" },
            { 'b', "<color=#40FFFFFF>" },
            { 'c', "<color=#FF4040FF>" },
            { 'd', "<color=#FF40FFFF>" },
            { 'e', "<color=#FFFF40FF>" },
            { 'f', "<color=#FFFFFFFF>" }
        };

        public override string text
        {
            get
            {
                return m_Text;
            }
            set
            {
                bool wasColorFlagTriggered = false;

                if (string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(m_Text))
                        return;
                    m_Text = "";
                    SetVerticesDirty();
                }
                else if (m_Text != value)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (value[i] == '&')
                        {
                            if (wasColorFlagTriggered)
                            {
                                sb.Append(COLOR_END_TAG);
                            }
                            sb.Append(colors[value[++i]]);
                            wasColorFlagTriggered = true;
                            continue;
                        }
                        sb.Append(value[i]);
                    }

                    if (wasColorFlagTriggered)
                    {
                        sb.Append(COLOR_END_TAG);
                    }

                    m_Text = sb.ToString();
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