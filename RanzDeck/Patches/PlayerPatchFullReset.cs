using System;
using HarmonyLib;

namespace RanzDeck.Patches
{
    [HarmonyPatch(typeof(Player), "FullReset")]
    [Serializable]
    internal class PlayerPatchFullReset
    {
        private static void Prefix(Player __instance)
        {
            RanzDeckLoader.DestroyRanzBehaviours(__instance.gameObject);
        }
    }
}