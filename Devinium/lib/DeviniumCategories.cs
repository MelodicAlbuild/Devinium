using System.IO;
using UnityEngine;
using System.Collections.Generic;
using static Devinium.lib.data.ExportHandler;
using Devinium.lib.data;

namespace Devinium.lib
{
    class DeviniumCategories : MonoBehaviour
    {

        public void InitCategories()
        {
            foreach (KeyValuePair<Category, GUID> dict in questingCategories)
            {
                if (dict.Key.category_type == "recipe")
                {
                    CreateRecipeCategory(dict.Key.name, dict.Key.guid);
                }
                else if (dict.Key.category_type == "factory")
                {
                    CreateFactoryCategory(dict.Key.name, dict.Key.guid);
                }
                else if (dict.Key.category_type == "module")
                {
                    CreateModuleCategory(dict.Key.name, dict.Key.guid);
                }
                else
                {
                    return;
                }
            }

            Debug.Log("[Devinium | Categories]: Categories Loaded...");
        }

        private void CreateFactoryCategory(string name, string categoryId)
        {
            var ok = ScriptableObject.CreateInstance<FactoryType>();
            ok.name = name;
            var guid = GUID.Parse(categoryId);
            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = ok, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
            Debug.Log("[Devinium | Categories]: Factory Category with name " + name + " has been loaded");
        }

        private void CreateModuleCategory(string name, string categoryId)
        {
            var ok = ScriptableObject.CreateInstance<ModuleCategory>();
            ok.name = name;
            var guid = GUID.Parse(categoryId);
            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = ok, Guid = guid, Labels = new string[4] } };
            RuntimeAssetStorage.Add(assets, default);
            Debug.Log("[Devinium | Categories]: Module Category with name " + name + " has been loaded");
        }

        private void CreateRecipeCategory(string name, string categoryId)
        {
            var Forge = ScriptableObject.CreateInstance<RecipeCategory>();
            Forge.name = name;
            var guid = GUID.Parse(categoryId);
            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = Forge, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
            Debug.Log("[Devinium | Categories]: Recipe Category with name " + name + " has been loaded");
        }
    }
}