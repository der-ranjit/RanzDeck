using BepInEx;
using HarmonyLib;
using RanzDeck.MonoBehaviours;
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
        public static RanzDeck? instance { get; private set; }

        public const string ModInitials = "OOF";
        public const string Version = "1.2.2";
        private const string ModName = "RanzDeck";
        private const string ModId = "ranz.rounds.ranzdeck";

        public static readonly AssetBundle CardArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("ranzdeck", typeof(RanzDeck).Assembly);

        public static GameObject LoadCardArtAsset(string assetName) => RanzDeck.CardArtBundle.LoadAsset<GameObject>(assetName);

        private static Harmony? harmony;
        public static bool devMode = true;

        public void Awake()
        {
            instance = this;
            RanzDeck.harmony = new Harmony(ModId);
            RanzDeck.harmony.PatchAll();
            if (RanzDeck.devMode && this.gameObject.GetComponent<DevMode>() == null)
            {
                this.gameObject.AddComponent<DevMode>();
            }
            RanzDeckLoader.Load();
        }

        public void OnDestroy()
        {
            RanzDeckLoader.DestroyRanzBehaviours(this.gameObject);
            RanzDeckLoader.Unload();
            if (RanzDeck.harmony != null)
            {
                RanzDeck.harmony.UnpatchSelf();
            }
        }
    }
}
