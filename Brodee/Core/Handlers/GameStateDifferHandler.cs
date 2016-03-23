using System.Linq;
using Brodee.Components;
using Brodee.Triggers;

namespace Brodee.Core.Handlers
{
    public class GameStateDifferHandler : Handler
    {
        private readonly HandlerHub _handlerHub;

        public GameStateDifferHandler(HandlerHub handlerHub)
        {
            _handlerHub = handlerHub;
        }

        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            if (!previous.GameMenuOpen && next.GameMenuOpen)
                _handlerHub.AddTrigger(new GameMenuOpenedTrigger());
            if (!previous.OptionsMenuOpen && next.OptionsMenuOpen)
                _handlerHub.AddTrigger(new OptionsMenuOpenedTrigger());
            if (!previous.BrodeeMenuOpen && next.BrodeeMenuOpen)
                _handlerHub.AddTrigger(new BrodeeMenuOpenedTrigger());

            if (previous.Mode != next.Mode)
                _handlerHub.AddTrigger(new GameStateModeChangedTrigger(next.Mode));

            if (next.MatchState.FriendlyPlayedCards.Count() != previous.MatchState.FriendlyPlayedCards.Count())
            {
                Logger.AppendLine($"next.FriendlyPlayedCards.Count:{next.MatchState.FriendlyPlayedCards.Count()} previous.FriendlyPlayedCards.Count:{previous.MatchState.FriendlyPlayedCards.Count()}");
            }
            foreach (var playedCard in next.MatchState.FriendlyPlayedCards)
            {
                if (previous.MatchState.FriendlyPlayedCards.All(x => x.EntityId != playedCard.EntityId))
                {
                    Logger.AppendLine($"FriendPlayedCard:{playedCard.Name}");
                }
            }

            foreach (var playedCard in next.MatchState.OpposingPlayedCards)
            {
                if (previous.MatchState.OpposingPlayedCards.All(x => x.EntityId != playedCard.EntityId))
                {
                    Logger.AppendLine($"OpposingPlayedCard:{playedCard.Name}");
                }
            }

        }
    }
}