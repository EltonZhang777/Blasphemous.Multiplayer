﻿using BlasClient.Data;
using BlasClient.ProgressSync.Helpers;
using Framework.FrameworkCore;
using Framework.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace BlasClient.ProgressSync
{
    public class ProgressManager
    {
        // Only enabled when processing & applying the queued progress updates
        public bool CurrentlyUpdatingProgress { get; private set; }

        // Locked queue of received progress updates
        private readonly List<ProgressUpdate> queuedProgressUpdates = new ();
        private static readonly object progressLock = new ();

        // Helper interfaces for applying / obtaining progress updates
        private readonly Dictionary<ProgressType, IProgressHelper> progressHelpers = new ()
        {
            { ProgressType.Bead, new BeadHelper() },
            { ProgressType.Prayer, new PrayerHelper() },
            { ProgressType.Relic, new RelicHelper() },
            { ProgressType.Heart, new HeartHelper() },
            { ProgressType.Collectible, new CollectibleHelper() },
            { ProgressType.QuestItem, new QuestItemHelper() },
            { ProgressType.PlayerStat, new StatHelper() },
            { ProgressType.SwordSkill, new SwordSkillHelper() },
            { ProgressType.MapCell, new MapCellHelper() },
            { ProgressType.Flag, new FlagHelper() },
            { ProgressType.PersistentObject, new PersistentObjectHelper() },
            { ProgressType.Teleport, new TeleportHelper() },
            { ProgressType.ChurchDonation, new ChurchDonationHelper() },
            { ProgressType.MiriamStatus, new MiriamHelper() },
        };

        public void ReceiveProgress(ProgressUpdate progress)
        {
            lock (progressLock)
            {
                Main.Multiplayer.Log("Received new game progress: " + progress.Id);
                queuedProgressUpdates.Add(progress);
            }
        }

        public void Update()
        {
            lock (progressLock)
            {
                CurrentlyUpdatingProgress = true;

                for (int i = 0; i < queuedProgressUpdates.Count; i++)
                {
                    ApplyProgress(queuedProgressUpdates[i]);
                }
                queuedProgressUpdates.Clear();

                CurrentlyUpdatingProgress = false;
            }
        }

        private void ApplyProgress(ProgressUpdate progress)
        {
            if (!progressHelpers.TryGetValue(progress.Type, out IProgressHelper helper))
            {
                Main.Multiplayer.Log("Error: Progress type doesn't exist: " + progress.Type);
                return;
            }

            helper.ApplyProgress(progress);
        }

        public string GetProgressNotification(ProgressUpdate progress)
        {
            if (!progressHelpers.TryGetValue(progress.Type, out IProgressHelper helper))
            {
                Main.Multiplayer.Log("Error: Progress type doesn't exist: " + progress.Type);
                return null;
            }

            return helper.GetProgressNotification(progress);
        }

        // Called when interacting with pers. object - determine whether to send it or not
        public void UsePersistentObject(string persistentId)
        {
            string scene = Core.LevelManager.currentLevel.LevelName;
            int objectSceneIdx = PersistentStates.getObjectSceneIndex(scene, persistentId);
            string objectSceneId = scene + "~" + objectSceneIdx;

            // Make sure this pers. object should sync & isn't already activated
            if (objectSceneIdx < 0 || Main.Multiplayer.checkPersistentObject(objectSceneId)) return;

            // Update save game data & send this object
            Main.Multiplayer.addPersistentObject(objectSceneId);
            if (Main.Multiplayer.config.syncSettings.worldState)
            {
                ProgressUpdate progress = new ProgressUpdate(objectSceneId, ProgressType.PersistentObject, 0);
                Main.Multiplayer.NetworkManager.SendProgress(progress);
            }
        }

        // Called when sending all data upon connecting to server and loading game
        public void SendAllProgress()
        {
            Main.Multiplayer.DisableFileLogging = true;

            foreach (IProgressHelper helper in progressHelpers.Values)
            {
                helper.SendAllProgress();
            }

            Main.Multiplayer.DisableFileLogging = false;
        }

        public void LevelLoaded(string scene)
        {
            foreach (PersistentObject persistence in Object.FindObjectsOfType<PersistentObject>())
            {
                int objectSceneIdx = PersistentStates.getObjectSceneIndex(scene, persistence.GetPersistenID());
                string objectSceneId = scene + "~" + objectSceneIdx;

                // This object does not even sync or hasn't been interacted with yet
                if (objectSceneIdx < 0 || !Main.Multiplayer.checkPersistentObject(objectSceneId)) continue;

                // Calling setPersistence() with null data means to play instant animation
                persistence.SetCurrentPersistentState(null, false, null);
            }
        }
    }
}
