using System.Linq;
using RanzDeck.MonoBehaviours;
using UnboundLib;
using UnboundLib.Cards;

namespace RanzDeck.Cards
{
    public abstract class RanzCard : CustomCard
    {
        public abstract void OnSetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block);
        protected float GetCurrentUsages(Player player)
        {
            return player.data.currentCards.Where(card => card.name == this.GetTitle()).Count();
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            this.OnSetupCard(cardInfo, gun, cardStats, statModifiers, block);
            ExtensionMethods.GetOrAddComponent<CardAuthorText>(cardInfo.gameObject);
        }
    }
}