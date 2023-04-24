using System.Linq;
using UnityEngine;

namespace UnityCraft.UI.MainMenu
{
    using System.Collections.Generic;
    using UnityCraft.Patterns;

    public class Flags : Singleton<Flags>
    {
        [SerializeField]
        private Flag[] flags;
        private Dictionary<string, Sprite> codeFlagPairs;
        private Dictionary<string, Sprite> altCodeFlagPairs;

        private void PrepareDictionaries()
        {
            codeFlagPairs = new Dictionary<string, Sprite>();
            altCodeFlagPairs = new Dictionary<string, Sprite>();

            foreach (var flag in flags)
            {
                codeFlagPairs.Add(flag.code1, flag.sprite);
                altCodeFlagPairs.Add(flag.code2, flag.sprite);
            }
        }

        public Sprite GetFlag(string code)
        {
            Sprite sprite;

            if (codeFlagPairs == null && altCodeFlagPairs == null)
            {
                PrepareDictionaries();
            }

            if (codeFlagPairs.TryGetValue(code, out sprite))
            {
                return sprite;
            }

            if (altCodeFlagPairs.TryGetValue(code, out sprite))
            {
                return sprite;
            }

            Debug.LogError($"<color=red>{nameof(Flags)}</color>: Requested invalid flag code: {code}");
            return null;
        }
    }
}