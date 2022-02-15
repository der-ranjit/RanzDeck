using UnboundLib.Cards;
using UnityEngine;

namespace RanzDeck.Cards
{
    class DrSmollBot : CustomCard
    {
        public override string GetModName() => RanzDeck.ModInitials;
        protected override string GetTitle() => "Dr. Smol Bot";
        protected override string GetDescription() => "Does not like turtles :(";
        protected override GameObject? GetCardArt() => RanzDeck.DrSmolBotCardArt;
        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Uncommon;
        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.EvilPurple;

        /// <summary>
        /// When modifying the supplied parameters / objects, those modifications are copied over to the respective stats in "ApplyCardStats()"
        /// </summary>
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            block.cdMultiplier = 0.25f;
            statModifiers.health = 0.1f;
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
                    positive = false,
                    stat = "Health",
                    amount = "-90%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Block Cooldown",
                    amount = "-75%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                }
            };
        }
    }
}
