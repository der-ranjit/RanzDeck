﻿using RanzDeck.MonoBehaviours;
using UnboundLib.Cards;
using UnityEngine;

namespace RanzDeck.Cards
{
    class CockyBlocky : CustomCard
    {
        public override string GetModName() => RanzDeck.ModInitials;
        protected override string GetTitle() => "Cocky Blocky";
        protected override string GetDescription() => "Blocking a projectile teleports you behind the attacker's aim direction.";
        protected override GameObject? GetCardArt() => null;
        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Uncommon;
        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.EvilPurple;

        private bool isPrimaryEffect = false;

        /// <summary>
        /// Called when a card is instantiated (on game and gamemode init / start).
        /// When modifying the supplied parameters / objects, those modifications are copied over to the respective stats in "ApplyCardStats()"
        /// </summary>
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            UnityEngine.Debug.Log($"RanzDeck: Card '{GetTitle()}' has been setup.");
            statModifiers.health = 0.65f;
            block.cdMultiplier = 0.9f;
        }

        /// <summary>
        /// Called when a card has been added to a player (card selection / sandbox).
        /// Modifications on the parameters are applied directly.
        /// </summary>
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            UnityEngine.Debug.Log($"[{RanzDeck.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            // only add the teleport effect once
            TeleportBehindAttackerBlockEffect existingEffect = player.gameObject.GetComponent<TeleportBehindAttackerBlockEffect>();
            if (existingEffect == null) {
                this.isPrimaryEffect = true;
                player.gameObject.AddComponent<TeleportBehindAttackerBlockEffect>();
            }
        }

        /// <summary>
        /// Called when a card is removed from a player.
        /// Modifications on the parameters are applied directly.
        /// </summary>
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            UnityEngine.Debug.Log($"[{RanzDeck.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            if (this.isPrimaryEffect) {
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