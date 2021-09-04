using System;
using System.Collections.Generic;
using System.Text;

using RoR2;
using R2API;
using UnityEngine;

namespace ArtifactOfOrderMod
{
    public class OrderArtifact
    {
        public static ArtifactDef order = ScriptableObject.CreateInstance<ArtifactDef>();

        public static void InitializeArtifact(AssetBundle assets)
        {
            Log.LogInfo("Initializing artifact of order...");
            order.nameToken = "Artifact of Order";
            order.descriptionToken = "Lunar align yourself at the beginning of each stage.";
            foreach (string s in assets.GetAllAssetNames()) {
                Log.LogInfo(s);
            }
            order.smallIconSelectedSprite = assets.LoadAsset<Sprite>("assets/textures/icons/artifacts/shrineoforder-2.png");
            order.smallIconDeselectedSprite = assets.LoadAsset<Sprite>("assets/textures/icons/artifacts/shrineoforder-3.png");

            ArtifactAPI.Add(order);
            Hook();
        }

        private static void Hook()
        {
            On.RoR2.Stage.BeginAdvanceStage += LunarAlign;
        }

        private static void LunarAlign(On.RoR2.Stage.orig_BeginAdvanceStage orig, Stage stage, SceneDef scene)
        {
            if (RunArtifactManager.instance.IsArtifactEnabled(order))
            { 
                foreach (var playerController in PlayerCharacterMasterController.instances) { 
                    Log.LogDebug($"lunar aligning {playerController.GetDisplayName()}'s inventory");
                    playerController.master.inventory.ShrineRestackInventory(new Xoroshiro128Plus(Run.instance.stageRng.nextUlong));
                }
            }

            //always orig
            orig(stage, scene);
        }
    }
}
