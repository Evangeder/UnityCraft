using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.UI.MainMenu.Components
{
    using Networking.API;
    using UI.Texts;

    public class Server : MonoBehaviour
    {
        [SerializeField]
        private ColoredText serverName;
        [SerializeField]
        private ColoredText serverSoftware;
        [SerializeField]
        private Text playerCount;
        [SerializeField]
        private Image serverFlag;

        private Servers server;

        public void ShowInfo(Servers server)
        {
            this.server = server;

            if (server == null)
            {
                Destroy(gameObject);
                return;
            }

            serverName.text = server.Name;
            serverSoftware.text = server.Software;
            playerCount.text = $"{server.CurrentPlayers}/{server.MaxPlayers}";
            serverFlag.sprite = Flags.Instance.GetFlag(server.FlagCode.ToLower());
        }

        //public void Connect()
        //{

        //}
    }
}