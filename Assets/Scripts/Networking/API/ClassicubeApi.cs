using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCraft.Networking.API
{
    using Patterns;

    public class ClassicubeApi : PersistentSingleton<ClassicubeApi>
    {
        private const string LOGIN_URL = "https://www.classicube.net/api/login/";
        private const string SERVERS_URL = "https://www.classicube.net/api/servers/";

        [SerializeField]
        private Canvas loadingCanvas;
        [SerializeField]
        private InputField loginField;
        [SerializeField]
        private InputField passwordField;

        private CookieContainer cookieContainer;
        private HttpClientHandler clientHandler;
        private HttpClient client;

        [HideInInspector]
        public bool PlayerLoggedIn = false;

        private void Start()
        {
            cookieContainer = new CookieContainer();
            clientHandler = new HttpClientHandler()
            {
                UseCookies = true,
                CookieContainer = cookieContainer
            };
            client = new HttpClient(clientHandler);
        }

        public async Task<bool> Login(string username, string password)
        {
            ShowLoadingOverlay();
            LoginApiResult loginApiResult;

            var response = await client.GetAsync(LOGIN_URL);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var lastToken = JsonConvert.DeserializeObject<LoginApiResult>(json).Token;

                var values = new Dictionary<string, string>
                {
                    { "username", username },
                    { "password", password },
                    { "token", lastToken },
                };

                var content = new FormUrlEncodedContent(values);
                response = await client.PostAsync(LOGIN_URL, content);

                var responseString = await response.Content.ReadAsStringAsync();
                loginApiResult = JsonConvert.DeserializeObject<LoginApiResult>(responseString);

                loadingCanvas.gameObject.SetActive(false);
                PlayerLoggedIn = loginApiResult.Authenticated;

                return PlayerLoggedIn;
            }

            HideLoadingOverlay();
            PlayerLoggedIn = false;
            return PlayerLoggedIn;
        }

        public async Task<Servers[]> ShowServerList()
        {
            ShowLoadingOverlay();

            var cookie = cookieContainer.GetCookies(new Uri(LOGIN_URL))[0];
            client.DefaultRequestHeaders.Add("Cookie", $"{cookie.Name}={cookie.Value}");

            var response = await client.GetAsync(SERVERS_URL);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerApiResult serverList = JsonConvert.DeserializeObject<ServerApiResult>(responseString);

            serverList.SortByPlayersOnline();

            HideLoadingOverlay();
            return serverList.servers;
        }

        private void ShowLoadingOverlay()
        {
            loadingCanvas.gameObject.SetActive(true);
        }

        private void HideLoadingOverlay()
        {
            loadingCanvas.gameObject.SetActive(false);
        }
    }
}
