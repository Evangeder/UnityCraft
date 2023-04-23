using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UnityCraft.Settings
{
    using Blocks;
    using Patterns;

    public class GameSettings : PersistentSingleton<GameSettings>
    {
        #region Constants

        public const int CHUNK_SIZE = 16;
        public const int DEFAULT_CHUNK_HEIGHT = 128;

        #endregion

        #region Setting window name (for entering servers or displaying world name)

#if UNITY_STANDALONE_WIN
        [DllImport("user32.dll", EntryPoint = "SetWindowText")]
        public static extern bool SetWindowText(IntPtr hwnd, string lpString);
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string className, string windowName);
#endif

        private string windowName = "UnityCraft";

        /// <summary>
        /// Sets the window name to something else.
        /// <br>Works only on Windows builds.</br>
        /// </summary>
        public string WindowName
        {
            get => windowName;
            set
            {
#if UNITY_STANDALONE_WIN

                var windowPtr = FindWindow(null, windowName);
                SetWindowText(windowPtr, value);
#endif
                windowName = value;
            }
        }

        #endregion

        #region Private Fields

        private int quality;
        private int lightingType;
        private int protocolType;
        private float musicVolume;
        private float effectsVolume;
        private bool hideChat;

        #endregion

        #region Public Properties

        /// <summary>
        /// Sets the quality to either fast or fancy.
        /// </summary>
        public Quality Quality
        { 
            get => (Quality)quality;
            set => quality = (int)value;
        }

        /// <summary>
        /// Changes how the world shading should work.
        /// </summary>
        public LightingType LightingType
        {
            get => (LightingType)lightingType;
            set => lightingType = (int)value;
        }

        /// <summary>
        /// Sets the networking protocol type.
        /// <br></br>
        /// </summary>
        public ProtocolType ProtocolType
        {
            get => (ProtocolType)protocolType;
            set => protocolType = (int)value;
        }

        /// <summary>
        /// Sets the volume of music.
        /// <br>Range: 0f - 1f</br>
        /// </summary>
        public float MusicVolume
        {
            get => musicVolume;
            set => musicVolume = value;
        }

        /// <summary>
        /// Sets the volume of walking, mining, placing blocks, etc.
        /// <br>Range: 0f - 1f</br>
        /// </summary>
        public float EffectsVolume
        {
            get => effectsVolume;
            set => effectsVolume = value;
        }

        /// <summary>
        /// Hides the chat in multiplayer worlds.
        /// </summary>
        public bool HideChat
        {
            get => hideChat;
            set => hideChat = value;
        }

        /// <summary>
        /// Contains all informations required to render blocks.
        /// </summary>
        public BlockDefinition[] Blocks { get; set; }

        #endregion

        #region Loading and Saving

        public void Save()
        {
            PlayerPrefs.SetInt("Quality", (int)Quality);
            PlayerPrefs.SetInt("LightingType", (int)LightingType);
            PlayerPrefs.SetInt("ProtocolType", (int)ProtocolType);
            PlayerPrefs.SetFloat("MusicVolume", (float)MusicVolume);
            PlayerPrefs.SetFloat("EffectsVolume", (float)EffectsVolume);
            PlayerPrefs.SetInt("HideChat", HideChat ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            LoadKey("Quality", ref quality);
            LoadKey("LightingType", ref lightingType);
            LoadKey("ProtocolType", ref protocolType);
            LoadKey("MusicVolume", ref musicVolume);
            LoadKey("EffectsVolume", ref effectsVolume);
            LoadKey("HideChat", ref hideChat);
        }

        private void LoadKey(string name, ref object param)
        {
            if (PlayerPrefs.HasKey(name))
            {
                var value = PlayerPrefs.GetInt(name);
                if (Enum.IsDefined(param.GetType(), value))
                {
                    param = value;
                }
            }
        }

        private void LoadKey(string name, ref int param)
        {
            if (PlayerPrefs.HasKey(name))
            {
                param = PlayerPrefs.GetInt(name);
            }
        }

        private void LoadKey(string name, ref float param)
        {
            if (PlayerPrefs.HasKey(name))
            {
                param = PlayerPrefs.GetFloat(name);
            }
        }

        private void LoadKey(string name, ref string param)
        {
            if (PlayerPrefs.HasKey(name))
            {
                param = PlayerPrefs.GetString(name);
            }
        }

        private void LoadKey(string name, ref bool param)
        {
            if (PlayerPrefs.HasKey(name))
            {
                param = PlayerPrefs.GetInt(name) == 1;
            }
        }

        #endregion
    }
}