using System;

namespace Brodee.Core
{
    public class BrodeeCard : IEquatable<BrodeeCard>
    {
        public string Name { get; private set; }
        public string CardId { get; private set; }
        public int EntityId { get; private set; }

        public BrodeeCard(Entity entity)
        {
            Name = entity.GetName();
            CardId = entity.GetCardId();
            EntityId = entity.GetEntityId();
        }

        public BrodeeCard(string name, string cardId, int entityId)
        {
            Name = name;
            CardId = cardId;
            EntityId = entityId;
        }

        public bool Equals(BrodeeCard other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EntityId == other.EntityId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BrodeeCard) obj);
        }

        public override int GetHashCode()
        {
            return EntityId;
        }
    }
}