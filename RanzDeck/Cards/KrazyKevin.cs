using RanzDeck.MonoBehaviours;
using UnboundLib;
using UnityEngine;

namespace RanzDeck.Cards
{
    class KrazyKevin : RanzCard
    {
        public override string GetModName() => RanzDeck.ModInitials;
        protected override string GetTitle() => "Krazy Kevin";
        protected override string GetDescription() => "Touching enemies deals damage based on your size. Dealing damage this way will knock you back from your target.";
        protected override GameObject? GetCardArt() => null;
        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Rare;
        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.EvilPurple;

        /// <summary>
        /// When modifying the supplied parameters / objects, those modifications are copied over to the respective stats in "ApplyCardStats()"
        /// </summary>
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.movementSpeed = 1.5f;
        }

        /// <summary>
        /// Modifications on the parameters are applied directly.
        /// </summary>
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // only add the effect once
            ExtensionMethods.GetOrAddComponent<Rampage>(player.gameObject);
        }

        /// <summary>
        /// Modifications on the parameters are applied directly.
        /// </summary>
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // TODO check if card is still part of list at this point
            if (this.GetPlayerUsages(player) == 1) 
            {
                Destroy(player.gameObject.GetComponent<Rampage>());
            }
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Movement Speed",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Blocking",
                    amount = "no more",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Shooting",
                    amount = "no more",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
    }
    
}
