using Devinium.lib.data;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Devinium.lib;

namespace Devinium
{
    public class Devinium : GameMod
    {
        public override void Load()
        {
            Debug.Log("Devinium Loaded");

            new ImportHandler();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            switch (scene.name)
            {
                case "MainMenu":
                    break;
                case "Island":
                    new DeviniumItems().InitItems();
                    new DeviniumCategories().InitCategories();
                    new DeviniumDeposits().InitDeposits();
                    new DeviniumRecipies().InitRecipes();
                    break;
            }
        }

        public override void Unload()
        {
            throw new NotImplementedException();
        }
    }
}
