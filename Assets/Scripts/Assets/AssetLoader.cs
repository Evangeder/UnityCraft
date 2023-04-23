using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace UnityCraft.Assets
{
    using Patterns;

    public class AssetLoader : PersistentSingleton<AssetLoader>
    {
        [SerializeField]
        private Material[] ChunkMaterials;
        [SerializeField]
        private Material Clouds;
        [SerializeField]
        private Material Dirt;
        [SerializeField]
        private Material Particles;
        [SerializeField]
        private Material Rain;
        [SerializeField]
        private Material Sun;
        [SerializeField]
        private Material Moon;

        [Header("Armor and items")]
        [SerializeField]
        private Material ArmorChainmail;
        [SerializeField]
        private Material ArmorCloth;
        [SerializeField]
        private Material ArmorDiamond;
        [SerializeField]
        private Material ArmorGold;
        [SerializeField]
        private Material ArmorIron;
        [SerializeField]
        private Material Items;
        [SerializeField]
        private Material Arrows;

        [Header("Paintings")]
        [SerializeField]
        private Material Art;

        [Header("UI")]
        [SerializeField]
        private Material Container;
        [SerializeField]
        private Material Crafting;
        [SerializeField]
        private Material Furnace;
        [SerializeField]
        private Material GUI;
        [SerializeField]
        private Material Icons;
        [SerializeField]
        private Material Inventory;
        [SerializeField]
        private Material Logo;

        [Header("Mobs")]
        [SerializeField]
        private Material MobCreeper;
        [SerializeField]
        private Material MobPig;
        [SerializeField]
        private Material MobSheep;
        [SerializeField]
        private Material MobSkeleton;
        [SerializeField]
        private Material MobSpider;
        [SerializeField]
        private Material MobZombie;
        [SerializeField]
        private Material MobPlayer;

        private void Start()
        {
            LoadAssets();
        }

        private bool LoadAssets()
        {
            if (Directory.Exists(Path.Combine(Application.dataPath, "Minecraft")))
            {
                LoadAsset("terrain.png", ChunkMaterials);
                LoadAsset("clouds.png", Clouds);
                LoadAsset("dirt.png", Dirt);
                LoadAsset("particles.png", Particles);
                LoadAsset("rain.png", Rain);
                LoadAsset(Path.Combine("terrain", "moon.png"), Moon);
                LoadAsset(Path.Combine("terrain", "sun.png"), Sun);

                return true;
            }

            return false;
        }

        private bool LoadAsset(string name, Material[] Materials)
        {
            bool success = true;
            foreach (var material in Materials)
            {
                if (!LoadAsset(name, material))
                {
                    success = false;
                }
            }

            return success;
        }

        private bool LoadAsset(string name, Material material)
        {
            Texture2D texture;
            byte[] fileData;

            var path = Path.Combine(Application.dataPath, "Minecraft", name);

            if (File.Exists(path))
            {
                fileData = File.ReadAllBytes(path);
                texture = new Texture2D(2, 2);
                texture.LoadImage(fileData);
                material.SetTexture("_MainTex", texture);

                return true;
            }

            return false;
        }
    }
}

