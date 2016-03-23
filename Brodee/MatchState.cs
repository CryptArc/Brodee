using System.Collections.Generic;

namespace Brodee
{

    public interface IMatchState
    {
        IEnumerable<Core.BrodeeCard> FriendlyPlayedCards { get; }
        IEnumerable<Core.BrodeeCard> OpposingPlayedCards { get; }
    }

    public class MatchState : IMatchState
    {
        IEnumerable<Core.BrodeeCard> IMatchState.FriendlyPlayedCards => FriendlyPlayedCards;
        IEnumerable<Core.BrodeeCard> IMatchState.OpposingPlayedCards => OpposingPlayedCards;
        public List<Core.BrodeeCard> FriendlyPlayedCards { get; set; }
        public List<Core.BrodeeCard> OpposingPlayedCards { get; set; }

        public MatchState()
        {
            FriendlyPlayedCards = new List<Core.BrodeeCard>();
            OpposingPlayedCards = new List<Core.BrodeeCard>();
        }
    }
}