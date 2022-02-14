﻿using UnboundLib.Cards;
using UnityEngine;

namespace RanzDeck.Cards
{
    class DrFatBot : CustomCard
    {
        public override string GetModName() => RanzDeck.ModInitials;
        protected override string GetTitle() => "Dr. Fat Bot";
        protected override string GetDescription() => "He likes turtles :)";
        protected override GameObject? GetCardArt() => RanzDeck.DrFatBotCardArt;
        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Common;
        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.EvilPurple;

        /// <summary>
        /// Called when a card is instantiated (on game and gamemode init / start).
        /// When modifying the supplied parameters / objects, those modifications are copied over to the respective stats in "ApplyCardStats()"
        /// </summary>
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            UnityEngine.Debug.Log($"RanzDeck: Card '{GetTitle()}' has been setup.");
            block.cdMultiplier = 1.3f;
            statModifiers.health = 3f;
        }

        /// <summary>
        /// Called when a card has been added to a player (card selection / sandbox).
        /// Modifications on the parameters are applied directly.
        /// </summary>
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            UnityEngine.Debug.Log($"[{RanzDeck.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
        }

        /// <summary>
        /// Called when a card is removed from a player.
        /// Modifications on the parameters are applied directly.
        /// </summary>
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            UnityEngine.Debug.Log($"[{RanzDeck.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
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
