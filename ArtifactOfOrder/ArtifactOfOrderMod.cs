using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using System.Reflection;
using UnityEngine;

namespace ArtifactOfOrderMod
{

    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [R2APISubmoduleDependency(nameof(ItemAPI), nameof(LanguageAPI), nameof(ArtifactAPI))]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
	
    public class ArtifactOfOrderMod : BaseUnityPlugin
	{
        public const string PluginGUID = "com.Brod8362.ArtifactOfOrder";
        public const string PluginAuthor = "Brod8362";
        public const string PluginName = "Artifact of Order";
        public const string PluginVersion = "1.0.0";

        public static AssetBundle MainAssets;

        public void Awake()
        {
            //Init our logging class so that we can properly log for debugging
            Log.Init(Logger);
            System.IO.Stream Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ArtifactOfOrder.ArtifactResources.artifactoforder");
            MainAssets = AssetBundle.LoadFromStream(Stream);
            if (MainAssets == null)
            {
                Log.LogError("assets are null!");
            }

            OrderArtifact.InitializeArtifact(MainAssets);

            // This line of log will appear in the bepinex console when the Awake method is done.
            Log.LogInfo("Initialized Artifact of Order");
        }
    }
}
