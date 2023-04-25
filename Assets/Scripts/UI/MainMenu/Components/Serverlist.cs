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

            var servers = await ClassicubeApi.Instance.ShowServerList();

            foreach (var serverData in servers)
            {
                var server = Instantiate(serverComponentPrefab, instantiationTarget);
                server.ShowInfo(serverData);
                instantiatedObjects.Add(server);
            }
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