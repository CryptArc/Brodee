namespace Brodee.Core
{
    public class Card
    {
        public string Name { get; private set; }
        public string CardId { get; private set; }

        public Card(Entity entity)
        {
            Name = entity.GetName();
            CardId = entity.GetCardId();
        }

        public Card(string name, string cardId)
        {
            Name = name;
            CardId = cardId;
        }
    }
}