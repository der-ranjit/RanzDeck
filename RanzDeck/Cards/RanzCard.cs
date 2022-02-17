using System.Linq;
using UnboundLib.Cards;

namespace RanzDeck.Cards
{
    public abstract class RanzCard : CustomCard
    {
        protected float GetCurrentUsages(Player player)
        {
            return player.data.currentCards.Where(card => card.name == this.GetTitle()).Count();
        }
    }
}