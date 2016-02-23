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

            var gameState = global::GameState.Get();
            if (gameState != null)
            {
                var friendlyPlayer = gameState.GetFriendlySidePlayer();
                var opposingPlayer = gameState.GetOpposingSidePlayer();

            }
        }
    }
}