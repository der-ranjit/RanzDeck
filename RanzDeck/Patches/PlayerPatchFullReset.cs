using System;
using HarmonyLib;
using RanzDeck.MonoBehaviours;

namespace RanzDeck.Patches
{
    [HarmonyPatch(typeof(Player), "FullReset")]
    [Serializable]
    internal class PlayerPatchFullReset
    {
        private static void Prefix(Player __instance)
        {
            RanzBehaviorsManager.DestroyAllRanzDeckMonoBehaviours(__instance.gameObject);
        }
    }
}