﻿using Framework.Managers;
using HarmonyLib;
using Tools.Level.Interactables;

namespace BlasClient.ProgressSync.Helpers.PersistentObjects
{
    [HarmonyPatch(typeof(PrieDieu), "OnUse")]
    public class PrieDieuUse_Patch
    {
        public static void Postfix(PrieDieu __instance)
        {
            if (Main.Multiplayer.ProgressManager.CurrentlyUpdatingProgress) return;

            string persistentId = __instance.GetPersistenID();
            Main.Multiplayer.Log($"Used PrieDieu: {persistentId}");
            Main.Multiplayer.ProgressManager.UsePersistentObject(persistentId);
        }
    }

    [HarmonyPatch(typeof(PrieDieu), "GetCurrentPersistentState")]
    public class PrieDieuReceive_Patch
    {
        public static bool Prefix(string dataPath, PrieDieu __instance)
        {
            if (dataPath != "use") return true;

            // Maybe play activation animation
            __instance.Ligthed = true;
            return false;
        }
    }

    [HarmonyPatch(typeof(PrieDieu), "SetCurrentPersistentState")]
    public class PrieDieuLoad_Patch
    {
        public static bool Prefix(PersistentManager.PersistentData data, PrieDieu __instance)
        {
            if (data != null)
            {
                // This method is being called normally - only execute if object hasn't been interacted with
                return !Main.Multiplayer.checkPersistentObject(__instance.GetPersistenID());
            }

            __instance.Ligthed = true;
            return false;
        }
    }
}
