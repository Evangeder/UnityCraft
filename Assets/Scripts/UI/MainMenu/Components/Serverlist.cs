using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityCraft.UI.MainMenu.Components
{
    using Networking.API;

    public class Serverlist : MonoBehaviour
    {
        [SerializeField]
        private Server serverComponentPrefab;
        [SerializeField]
        private Transform instantiationTarget;
        [SerializeField]
        private GameObject hamburgerLoading;
        private List<Server> instantiatedObjects;

        private async void OnEnable()
        {
            await ShowServerlist();
        }

        private void OnDisable()
        {
            ClearServerlist();
        }

        async Task ShowServerlist()
        {
            instantiatedObjects = new();

            hamburgerLoading.SetActive(true);
            var serverApiResponse = await new ServerApi().Fetch();
            serverApiResponse.SortByPlayersOnline();

            foreach (var serverData in serverApiResponse.servers)
            {
                var server = Instantiate(serverComponentPrefab, instantiationTarget);
                server.ShowInfo(serverData);
                instantiatedObjects.Add(server);
            }
            hamburgerLoading.SetActive(false);
        }

        public void ClearServerlist()
        {
            foreach (var obj in instantiatedObjects)
            {
                Destroy(obj.gameObject);
            }

            instantiatedObjects.Clear();
        }
    }
}