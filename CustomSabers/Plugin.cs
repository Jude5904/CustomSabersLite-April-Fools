﻿using IPA;
using IPALogger = IPA.Logging.Logger;
using IPA.Utilities;
using IPA.Config.Stores;
using Config = IPA.Config.Config;
using CustomSaber.Configuration;
using CustomSaber.Utilities;
using BS_Utils.Utilities;
using System.IO;
using CustomSaber.UI;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomSaber
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }

        public static string PluginName => "Custom Sabers Lite";

        public static string PluginGUID => "qqrz.CustomSabersLite";

        public static string CustomSaberAssetsPath => Path.Combine(UnityGame.InstallPath, "CustomSabers");

        public static bool AssetLoaderInitialized { get; set; }

        public static IPALogger Log { get; private set; }

        [Init]
        public void Init(IPALogger logger, Config config)
        {
            AssetLoaderInitialized = false;
            Log = logger;
            CustomSaberConfig.Instance = config.Generated<CustomSaberConfig>();
            Log.Debug("Config Loaded");
        }

        [OnStart]
        public async Task OnApplicationStart()
        {
            SettingsUI.CreateMenu();
            AddEvents();
            try
            {
                //await Task.WhenAll(CustomSaberAssetLoader.LoadAsync());
                CustomSaberAssetLoader.Load();
                AssetLoaderInitialized = true;
                SettingsUI.UpdateMenuOnSabersLoaded();
            } 
            catch { }
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            CustomSaberAssetLoader.Clear();
            RemoveEvents();
        }

        private void OnGameSceneLoaded()
        {
            if (AssetLoaderInitialized) SaberScript.Load();
        }

        private void OnMenuSceneLoaded()
        {
            //this doesn't actually refresh the button
            if (!SettingsUI.MenuButtonActive && AssetLoaderInitialized) SettingsUI.UpdateMenu();
        }

        private void AddEvents()
        {
            RemoveEvents();
            BSEvents.gameSceneLoaded += OnGameSceneLoaded;
            BSEvents.menuSceneActive += OnMenuSceneLoaded;
        }

        private void RemoveEvents()
        {
            BSEvents.gameSceneLoaded -= OnGameSceneLoaded;
            BSEvents.menuSceneActive -= OnMenuSceneLoaded;
        }
    }
}
