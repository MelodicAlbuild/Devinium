using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static Devinium.lib.data.ExportHandler;
using Devinium.lib.data;
using System.Collections.Generic;

namespace Devinium.lib
{
    class DeviniumItems
    {
        public void InitItems()
        {
            foreach (KeyValuePair<Item, GUID> dict in questingItems)
            {
                CreateItem(dict.Key.item_name, dict.Key.stack_size, dict.Key.name, dict.Key.description, dict.Key.guid, dict.Key.base_item, Sprite2(dict.Key.icon_path));
            }

            Debug.Log("[Questing Update | Items]: Items Loaded...");
        }

        private static void Initialize<T>(ref T str)
        where T : struct, ISerializationCallbackReceiver
        {
            str.OnAfterDeserialize();
        }
        private Sprite Sprite2(string iconpath)
        {
            var path = System.IO.Path.Combine(Application.persistentDataPath, "Mods", iconpath);
            if (!File.Exists(path))
            {
                Debug.Log("ERROR: [Questing Update | Items]: Specified Icon path not found: " + path);

                Debug.LogError("[Questing Update | Items]: Specified Icon path not found: " + path);
                return null;
            }
            var bytes = File.ReadAllBytes(path);


            var texture = new Texture2D(512, 512, TextureFormat.ARGB32, true);
            texture.LoadImage(bytes);

            var sprite = Sprite.Create(texture, new Rect(Vector2.zero, Vector2.one * texture.width), new Vector2(0.5f, 0.5f), texture.width, 0, SpriteMeshType.FullRect, Vector4.zero, false);
            return sprite;
        }

        private void CreateItem(string codename, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<ItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.MaxStack = maxstack;
            item.Icon = icon;
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            Debug.Log("[Questing Update | Items]: Item " + codename + " has been loaded");
        }
    }
}