using BepInEx;
using UnboundLib.Cards;
using HarmonyLib;
using RanzDeck.Cards;
using UnityEngine;
using Photon.Pun;
using UnboundLib.Utils;
using System.Collections.Generic;

namespace RanzDeck
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.BlockForcePatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class RanzDeck : BaseUnityPlugin
    {
        // needed for BepinEx stuff (exposes the GameObject)
        public static RanzDeck? instance { get; private set; }

        public const string ModInitials = "OOF";
        public const string Version = "1.2.2";
        private const string ModName = "RanzDeck";
        private const string ModId = "ranz.rounds.ranzdeck";

        private static readonly AssetBundle CardArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("ranzdeck", typeof(RanzDeck).Assembly);
        public static GameObject DrFatBotCardArt = CardArtBundle.LoadAsset<GameObject>("C_DRFATBOT");
        public static GameObject? DrSmolBotCardArt = CardArtBundle.LoadAsset<GameObject>("C_DRSMALLBOT");
        public static GameObject? CockyBlockyCardArt = CardArtBundle.LoadAsset<GameObject>("C_COCKYBLOCKY");

        private static Harmony? harmony;
        private static bool devMode = true;

        private static List<CardInfo> buildCards = new List<CardInfo>();

        public static void Log(string message)
        {
            if (RanzDeck.devMode)
            {
                UnityEngine.Debug.Log(message);
            }
        }

        public void Awake()
        {
            RanzDeck.harmony = new Harmony(ModId);
            RanzDeck.harmony.PatchAll();
            instance = this;
            RanzDeck.LoadRanzDeckStuff();

            // TODO for devMode load into sandbox mode with 2 players and some given cards (cards need to be awaited)
            // also return to main menu if possible.
            // also also try to reset CardManager or default cards may be used (seems to be some issue with sandbox and not leaving the room correctly?)
        }

        public void OnDestroy()
        {
            if (RanzDeck.harmony != null)
            {
                RanzDeck.harmony.UnpatchSelf();
            }
            RanzDeck.UnloadRanzDeckStuff();
        }

        private static void LoadRanzDeckStuff()
        {
            RanzDeck.Log("[RanzDeck] Loading start");
            CustomCard.BuildCard<DrSmollBot>(RanzDeck.HandleCardBuild);
            CustomCard.BuildCard<DrFatBot>(RanzDeck.HandleCardBuild);
            CustomCard.BuildCard<CockyBlocky>(RanzDeck.HandleCardBuild);
            CustomCard.BuildCard<KrazyKevin>(RanzDeck.HandleCardBuild);
        }

        private static void HandleCardBuild(CardInfo cardInfo)
        {
            RanzDeck.Log($"[RanzDeck] Loaded card '{cardInfo.name}'");
            RanzDeck.buildCards.Add(cardInfo);
        }

        private static void UnloadRanzDeckStuff()
        {
            RanzDeck.Log("[RanzDeck] Unloading start");
            RanzDeck.CardArtBundle.Unload(true);
            RanzDeck.UnloadBuildCards();
        }

        private static void UnloadBuildCards()
        {
            foreach (CardInfo cardInfo in RanzDeck.buildCards)
            {
                RanzDeck.Log($"[RanzDeck] Unloading Card '{cardInfo.name}'"); ;
                CardManager.DisableCard(cardInfo);
                CardManager.cards.Remove(cardInfo.name);
                CustomCard.cards.Remove(cardInfo);
                ((DefaultPool)PhotonNetwork.PrefabPool).ResourceCache.Remove(cardInfo.name);
                PhotonNetwork.Destroy(cardInfo.gameObject);
            }
            RanzDeck.buildCards.Clear();
        }
    }
}
