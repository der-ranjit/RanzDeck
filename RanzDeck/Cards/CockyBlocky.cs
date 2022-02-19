using RanzDeck.MonoBehaviours;
using UnboundLib;
using UnityEngine;

namespace RanzDeck.Cards
{
    class CockyBlocky : RanzCard
    {
        public static string CardName = "Cocky Blocky";

        public override string GetModName() => RanzDeck.ModInitials;
        protected override string GetTitle() => CockyBlocky.CardName;
        protected override string GetDescription() => "Blocking a projectile teleports you behind the attacker's aim direction.";
        protected override GameObject? GetCardArt() => RanzDeck.LoadCardArtAsset("C_COCKYBLOCKY");
        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Uncommon;
        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.EvilPurple;

        /// <summary>
        /// When modifying the supplied parameters / objects, those modifications are copied over to the respective stats in "ApplyCardStats()"
        /// </summary>
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.health = 0.65f;
            block.cdMultiplier = 0.9f;
        }

        /// <summary>
        /// Modifications on the parameters are applied directly.
        /// </summary>
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // only add the teleport effect once
            ExtensionMethods.GetOrAddComponent<TeleportBehindAttackerBlockEffect>(player.gameObject);
        }

        /// <summary>
        /// Modifications on the parameters are applied directly.
        /// </summary>
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (this.GetCurrentUsages(player) == 1)
            {
                Destroy(player.gameObject.GetComponent<TeleportBehindAttackerBlockEffect>());
            }
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Health",
                    amount = "-35%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Block Cooldown",
                    amount = "-10%",
                    simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
                }
            };
        }
    }
}
