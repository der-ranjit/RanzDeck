using BepInEx;
using UnboundLib.Cards;
using HarmonyLib;
using RanzDeck.Cards;
using UnityEngine;

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
        public const string Version = "1.0.0";
        private const string ModName = "RanzDeck";
        private const string ModId = "ranz.rounds.ranzdeck";

        private static readonly AssetBundle CardArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("ranzdeck", typeof(RanzDeck).Assembly);
 
        public static GameObject DrFatBotCardArt = CardArtBundle.LoadAsset<GameObject>("C_DRFATBOT");

        void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        void Start()
        {
            instance = this;
            CustomCard.BuildCard<DrSmollBot>();
            CustomCard.BuildCard<DrFatBot>();
            CustomCard.BuildCard<CockyBlocky>();
        }
    }
}
