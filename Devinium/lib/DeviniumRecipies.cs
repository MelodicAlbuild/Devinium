using System.Linq;
using System.Reflection;
using UnityEngine;
using System.IO;
using static Devinium.lib.data.ExportHandler;
using Devinium.lib.data;
using System.Collections.Generic;
using System;

namespace Devinium.lib
{
    class DeviniumRecipies
    {
        public void InitRecipes()
        {
            foreach (KeyValuePair<data.Recipe, GUID> dict in questingRecipes)
            {
                CreateRecipe(dict.Key.recipe_name, dict.Key.inputs, dict.Key.output, dict.Key.base_recipe, dict.Key.itemID, dict.Key.required_items, dict.Key.recipe_category);
            }

            Debug.Log("[Devinium | Recipes]: Recipes Loaded...");
        }
        private RecipeCategory tempcategory;
        public RecipeCategory FindCategories(string categoryname)
        {
            tempcategory = null;
            foreach (Recipe recipe in GameResources.Instance.Recipes)
            {
                foreach (RecipeCategory category in recipe.Categories)
                {
                    if (category != null && categoryname != null)
                    {
                        if (category.name == categoryname)
                        {
                            tempcategory = category;
                        }
                    }
                }
            }
            return tempcategory;
        }

        private void CreateRecipe(string recipeName, data.Input[] inputs, Output[] outputs, string baseRecipe, string itemId, string[] requiredItems, string recipeCategory)
        {
            var outputItem = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputs[0].output_name);
            var finalInput = new InventoryItemData[inputs.Length];
            var i = 0;
            foreach (data.Input input in inputs)
            {
                var itemVar = GameResources.Instance.Items.FirstOrDefault(s => s.name == input.input_name);
                finalInput[i] = new InventoryItemData { Item = itemVar, Amount = input.input_amount };
                i++;
            }

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = finalInput;
            recipe.Output = new InventoryItemData { Item = outputItem, Amount = outputs[0].output_amount };
            if (requiredItems[0] != "")
            {
                var requiredFinal = new ItemDefinition[inputs.Length];
                var iReq = 0;
                foreach (string item in requiredItems)
                {
                    var instanceVar = GameResources.Instance.Items.FirstOrDefault(s => s.name == item);
                    requiredFinal[iReq] = instanceVar;
                    iReq++;
                }
                recipe.RequiredUpgrades = requiredFinal;
            }
            else
            {
                var baseRecipeTag = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipe);
                recipe.RequiredUpgrades = baseRecipeTag.RequiredUpgrades;
            }
            if (recipeCategory != "")
            {
                recipe.Categories = new RecipeCategory[] { FindCategories(recipeCategory) };
            }
            else
            {
                var baseRecipeTag = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipe);
                recipe.Categories = baseRecipeTag.Categories.ToArray();
            }

            var guid = GUID.Parse(itemId);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName, string outputName, string baseRecipeName, int inputAmount, int outputAmount, string itemId)
        {
            var input = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input, Amount = inputAmount } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = baseRecipe.RequiredUpgrades;

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName, string outputName, string baseRecipeName, int inputAmount, int outputAmount, string itemId, string requiredItem)
        {
            var input = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);
            var requiredItemWrite = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input, Amount = inputAmount } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = new ItemDefinition[] { requiredItemWrite };

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName1, string inputName2, string outputName, string baseRecipeName, int inputAmount1, int inputAmount2, int outputAmount, string itemId, string requiredItem)
        {
            var input1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName1);
            var input2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName2);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);
            var requiredItemWrite = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input1, Amount = inputAmount1 }, new InventoryItemData { Item = input2, Amount = inputAmount2 } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = new ItemDefinition[] { requiredItemWrite };

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName1, string inputName2, string outputName, string baseRecipeName, int inputAmount1, int inputAmount2, int outputAmount, string itemId, GUID requiredItem)
        {
            var input1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName1);
            var input2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName2);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);
            var requiredItemWrite = GameResources.Instance.Items.FirstOrDefault(s => s.AssetId == requiredItem);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input1, Amount = inputAmount1 }, new InventoryItemData { Item = input2, Amount = inputAmount2 } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = new ItemDefinition[] { requiredItemWrite };

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName1, string inputName2, string outputName, string baseRecipeName, int inputAmount1, int inputAmount2, int outputAmount, string itemId, string requiredItem1, string requiredItem2)
        {
            var input1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName1);
            var input2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName2);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);
            var requiredItemWrite1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem1);
            var requiredItemWrite2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem2);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input1, Amount = inputAmount1 }, new InventoryItemData { Item = input2, Amount = inputAmount2 } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = new ItemDefinition[] { requiredItemWrite1, requiredItemWrite2 };

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName1, string inputName2, string inputName3, string outputName, string baseRecipeName, int inputAmount1, int inputAmount2, int inputAmount3, int outputAmount, string itemId, string requiredItem)
        {
            var input1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName1);
            var input2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName2);
            var input3 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName3);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);
            var requiredItemWrite = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input1, Amount = inputAmount1 }, new InventoryItemData { Item = input2, Amount = inputAmount2 }, new InventoryItemData { Item = input3, Amount = inputAmount3 } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = new ItemDefinition[] { requiredItemWrite };

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName1, string inputName2, string inputName3, string outputName, string baseRecipeName, int inputAmount1, int inputAmount2, int inputAmount3, int outputAmount, string itemId, GUID requiredItem)
        {
            var input1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName1);
            var input2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName2);
            var input3 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName3);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);
            var requiredItemWrite = GameResources.Instance.Items.FirstOrDefault(s => s.AssetId == requiredItem);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input1, Amount = inputAmount1 }, new InventoryItemData { Item = input2, Amount = inputAmount2 }, new InventoryItemData { Item = input3, Amount = inputAmount3 } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = new ItemDefinition[] { requiredItemWrite };

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName1, string inputName2, string inputName3, string outputName, string baseRecipeName, int inputAmount1, int inputAmount2, int inputAmount3, int outputAmount, string itemId, string requiredItem1, string requiredItem2)
        {
            var input1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName1);
            var input2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName2);
            var input3 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName3);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);
            var requiredItemWrite1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem1);
            var requiredItemWrite2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem2);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input1, Amount = inputAmount1 }, new InventoryItemData { Item = input2, Amount = inputAmount2 }, new InventoryItemData { Item = input3, Amount = inputAmount3 } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = new ItemDefinition[] { requiredItemWrite1, requiredItemWrite2 };

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName1, string inputName2, string inputName3, string outputName, string baseRecipeName, int inputAmount1, int inputAmount2, int inputAmount3, int outputAmount, string itemId, string requiredItem1, string requiredItem2, string requiredItem3)
        {
            var input1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName1);
            var input2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName2);
            var input3 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName3);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);
            var requiredItemWrite1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem1);
            var requiredItemWrite2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem2);
            var requiredItemWrite3 = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem3);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input1, Amount = inputAmount1 }, new InventoryItemData { Item = input2, Amount = inputAmount2 }, new InventoryItemData { Item = input3, Amount = inputAmount3 } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = new ItemDefinition[] { requiredItemWrite1, requiredItemWrite2, requiredItemWrite3 };

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName, string outputName, string baseRecipeName, int inputAmount, int outputAmount, string itemId, string requiredItem1, string requiredItem2)
        {
            var input = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);
            var requiredItemWrite1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem1);
            var requiredItemWrite2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem2);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input, Amount = inputAmount } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = new ItemDefinition[] { requiredItemWrite1, requiredItemWrite2 };

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName, string outputName, string baseRecipeName, int inputAmount, int outputAmount, string itemId, string requiredItem1, string requiredItem2, string requiredItem3)
        {
            var input = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);
            var requiredItemWrite1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem1);
            var requiredItemWrite2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem2);
            var requiredItemWrite3 = GameResources.Instance.Items.FirstOrDefault(s => s.name == requiredItem3);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input, Amount = inputAmount } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = new ItemDefinition[] { requiredItemWrite1, requiredItemWrite2, requiredItemWrite3 };

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName, string outputName, string baseRecipeName, int inputAmount, int outputAmount, string itemId, RecipeCategory recipeCategory)
        {
            var input = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input, Amount = inputAmount } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = baseRecipe.RequiredUpgrades;

            recipe.Categories = new RecipeCategory[] { recipeCategory };

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName1, string inputName2, string outputName, string baseRecipeName, int inputAmount1, int inputAmount2, int outputAmount, string itemId)
        {
            var input1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName1);
            var input2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName2);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input1, Amount = inputAmount1 }, new InventoryItemData { Item = input2, Amount = inputAmount2 } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = baseRecipe.RequiredUpgrades;

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName1, string inputName2, string inputName3, string inputName4, string outputName, string baseRecipeName, int inputAmount1, int inputAmount2, int inputAmount3, int inputAmount4, int outputAmount, string itemId)
        {
            var input1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName1);
            var input2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName2);
            var input3 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName3);
            var input4 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName4);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input1, Amount = inputAmount1 }, new InventoryItemData { Item = input2, Amount = inputAmount2 }, new InventoryItemData { Item = input3, Amount = inputAmount3 }, new InventoryItemData { Item = input4, Amount = inputAmount4 } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = baseRecipe.RequiredUpgrades;

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(string recipeName, string inputName1, string inputName2, string inputName3, string inputName4, string inputName5, string outputName, string baseRecipeName, int inputAmount1, int inputAmount2, int inputAmount3, int inputAmount4, int inputAmount5, int outputAmount, string itemId)
        {
            var input1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName1);
            var input2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName2);
            var input3 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName3);
            var input4 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName4);
            var input5 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName5);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input1, Amount = inputAmount1 }, new InventoryItemData { Item = input2, Amount = inputAmount2 }, new InventoryItemData { Item = input3, Amount = inputAmount3 }, new InventoryItemData { Item = input4, Amount = inputAmount4 }, new InventoryItemData { Item = input5, Amount = inputAmount5 } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = baseRecipe.RequiredUpgrades;

            recipe.Categories = baseRecipe.Categories.ToArray();

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }

        private void CreateRecipe(RecipeCategory recipeCategory, string recipeName, string inputName1, string inputName2, string outputName, string baseRecipeName, int inputAmount1, int inputAmount2, int outputAmount, string itemId)
        {
            var input1 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName1);
            var input2 = GameResources.Instance.Items.FirstOrDefault(s => s.name == inputName2);
            var output = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputName);
            var baseRecipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == baseRecipeName);

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = recipeName;
            recipe.Inputs = new InventoryItemData[] { new InventoryItemData { Item = input1, Amount = inputAmount1 }, new InventoryItemData { Item = input2, Amount = inputAmount2 } };
            recipe.Output = new InventoryItemData { Item = output, Amount = outputAmount };
            recipe.RequiredUpgrades = baseRecipe.RequiredUpgrades;

            recipe.Categories = new RecipeCategory[] { recipeCategory };

            var guid = GUID.Parse(itemId);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(recipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = recipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);

            
            Debug.Log("[Devinium | Recipes]: Recipe " + recipeName + " has been Loaded");
        }
    }
}
