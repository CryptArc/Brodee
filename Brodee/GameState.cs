using System.Collections.Generic;

namespace Brodee
{
    public interface IGameState
    {
        Scene Mode { get; }
        bool GameMenuOpen { get; }
        bool OptionsMenuOpen { get; }
        bool QuestLogOpen { get; }
        bool BrodeeMenuOpen { get; set; }

        IEnumerable<Core.Card> FriendlyPlayedCards { get; }
        IEnumerable<Core.Card> OpposingPlayedCards { get; }
    }

    public class GameState : IGameState
    {
        public Scene Mode { get; set; } = Scene.None;

        public bool GameMenuOpen { get; set; }
        public bool OptionsMenuOpen { get; set; }
        public bool BrodeeMenuOpen { get; set; }
        public bool QuestLogOpen { get; set; }

        IEnumerable<Core.Card> IGameState.FriendlyPlayedCards => FriendlyPlayedCards;
        IEnumerable<Core.Card> IGameState.OpposingPlayedCards => OpposingPlayedCards;
        public List<Core.Card> FriendlyPlayedCards { get; set; }
        public List<Core.Card> OpposingPlayedCards { get; set; }

        public GameState()
        {
            FriendlyPlayedCards = new List<Core.Card>();
            OpposingPlayedCards = new List<Core.Card>();
        }
    }
}