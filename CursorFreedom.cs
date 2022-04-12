using BepInEx;
using RoR2;
using UnityEngine;

// Allow scanning for ConCommand, and other stuff for Risk of Rain 2
[assembly: HG.Reflection.SearchableAttribute.OptIn]
namespace CursorFreedom
{
    [BepInPlugin(GUID, ModName, Version)]
    public class CursorFreedom : BaseUnityPlugin
    {
        public const string GUID = "com.justinderby.cursorfreedom";
        public const string ModName = "CursorFreedom";
        public const string Version = "1.0.0";

        public static CursorFreedom Instance;

        public void Awake()
        {
            On.RoR2.MPEventSystemManager.Update += MPEventSystemManager_Update;
            On.RoR2.RoR2Application.Update += RoR2Application_Update;
        }

        public void Destroy()
        {
            On.RoR2.MPEventSystemManager.Update -= MPEventSystemManager_Update;
            On.RoR2.RoR2Application.Update -= RoR2Application_Update;
        }

        private void RoR2Application_Update(On.RoR2.RoR2Application.orig_Update orig, RoR2Application self)
        {
            orig(self);
            PossiblyFreeCursor();
        }

        private void MPEventSystemManager_Update(On.RoR2.MPEventSystemManager.orig_Update orig, MPEventSystemManager self)
        {
            orig(self);
            PossiblyFreeCursor();
        }

        private void PossiblyFreeCursor()
        {
            if (!Application.isBatchMode)
            {
                Cursor.lockState = ((MPEventSystemManager.kbmEventSystem.isCursorVisible || MPEventSystemManager.combinedEventSystem.isCursorVisible) ? CursorLockMode.None : CursorLockMode.Locked);
                Cursor.visible = false;
            }
        }
    }
}
