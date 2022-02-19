using UnboundLib.Cards;
using UnityEngine;

namespace RanzDeck.Cards
{
    class DrFatBot : CustomCard
    {
        public static string CardName = "Dr. Fat Bot";

        public override string GetModName() => RanzDeck.ModInitials;
        protected override string GetTitle() => DrFatBot.CardName;
        protected override string GetDescription() => "I like turtles";
        protected override GameObject? GetCardArt() => RanzDeck.LoadCardArtAsset("C_DRFATBOT");
        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Common;
        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.EvilPurple;

        /// <summary>
        /// When modifying the supplied parameters / objects, those modifications are copied over to the respective stats in "ApplyCardStats()"
        /// </summary>
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            block.cdMultiplier = 1.3f;
            statModifiers.health = 3f;
        }

        /// <summary>
        /// Modifications on the parameters are applied directly.
        /// </summary>
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        /// <summary>
        /// Modifications on the parameters are applied directly.
        /// </summary>
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Health",
                    amount = "+300%",
                    simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Block Cooldown",
                    amount = "+30%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                }
            };
        }
    }
}
