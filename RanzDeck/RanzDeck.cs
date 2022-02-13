using BepInEx;
using UnboundLib.Cards;
using HarmonyLib;
using RanzCards.Cards;

namespace RanzCards
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class RanzDeck : BaseUnityPlugin
    {
        // needed for BepinEx stuff (exposes the GameObject)
        public static RanzDeck instance { get; private set; }

        public const string ModInitials = "OOF";
        public const string Version = "1.0.0";
        private const string ModName = "RanzDeck";
        private const string ModId = "ranz.rounds.ranzdeck";

        void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        void Start()
        {
            instance = this;
            CustomCard.BuildCard<DrFatBot>();
        }
    }
}
