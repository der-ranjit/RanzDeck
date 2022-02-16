using UnboundLib.Cards;

namespace RanzDeck.Cards
{
    public abstract class RanzCard : CustomCard
    {
        protected float GetPlayerUsages(Player player)
        {
            return player.data.currentCards.FindAll(card => card.name == this.GetTitle()).Count;
        }
    }
}