﻿using Framework.Inventory;
using Framework.Managers;
using HarmonyLib;

namespace BlasClient.ProgressSync.Helpers
{
    public class BeadHelper : IProgressHelper
    {
        public void ApplyProgress(ProgressUpdate progress)
        {
            if (!Main.Multiplayer.config.syncSettings.inventoryItems) return;

            if (progress.Value == 1)
                Core.InventoryManager.RemoveRosaryBead(progress.Id);
            else
                Core.InventoryManager.AddRosaryBead(progress.Id);
        }

        public string GetProgressNotification(ProgressUpdate progress)
        {
            RosaryBead bead = Core.InventoryManager.GetRosaryBead(progress.Id);
            return progress.Value == 0 && bead != null ? $"{Main.Multiplayer.Localize("itmnot")} {bead.caption}" : null;
        }

        public void SendAllProgress()
        {
            foreach (RosaryBead bead in Core.InventoryManager.GetRosaryBeadOwned())
            {
                ProgressUpdate progress = new ProgressUpdate(bead.id, ProgressType.Bead, 0);
                Main.Multiplayer.NetworkManager.SendProgress(progress);
            }
        }
    }

    [HarmonyPatch(typeof(InventoryManager), "AddRosaryBead", typeof(RosaryBead))]
    public class InventoryBead_Patch
    {
        public static void Postfix(RosaryBead rosaryBead)
        {
            if (Main.Multiplayer.ProgressManager.CurrentlyUpdatingProgress || !Main.Multiplayer.config.syncSettings.inventoryItems)
                return;

            ProgressUpdate progress = new ProgressUpdate(rosaryBead.id, ProgressType.Bead, 0);
            Main.Multiplayer.NetworkManager.SendProgress(progress);
        }
    }
    [HarmonyPatch(typeof(InventoryManager), "RemoveRosaryBead", typeof(RosaryBead))]
    public class InventoryBeadRemove_Patch
    {
        public static void Postfix(RosaryBead bead)
        {
            if (Main.Multiplayer.ProgressManager.CurrentlyUpdatingProgress || !Main.Multiplayer.config.syncSettings.inventoryItems)
                return;

            ProgressUpdate progress = new ProgressUpdate(bead.id, ProgressType.Bead, 1);
            Main.Multiplayer.NetworkManager.SendProgress(progress);
        }
    }
}
