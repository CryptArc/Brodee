using Brodee.Core;

namespace Brodee.Triggers
{
    public class CardPlayedTrigger
    {
        public bool FriendlyCard { get; private set; }
        public BrodeeCard Card { get; private set; }

        public CardPlayedTrigger(bool friendlyCard, BrodeeCard card)
        {
            FriendlyCard = friendlyCard;
            Card = card;
        }
    }
}