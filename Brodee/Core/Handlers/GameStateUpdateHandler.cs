using System;
using System.Collections.Generic;
using Brodee.Components;

namespace Brodee.Core.Handlers
{
    public class GameStateUpdateHandler : Handler
    {
        private static readonly List<global::Card> EmptyCardList = new List<global::Card>();

        private readonly Func<GameState> _newGameStateFunc;

        public GameStateUpdateHandler(Func<GameState> newGameStateFunc)
        {
            _newGameStateFunc = newGameStateFunc;
        }

        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            var newGameState = _newGameStateFunc();

            UpdateCommon(newGameState);
            if (newGameState.Mode == Scene.GamePlay)
                UpdateInGame(newGameState);
        }

        private void UpdateCommon(GameState newState)
        {
            var newSceneMode = SceneMgr.Get().GetMode().MapToScene();

            newState.Mode = newSceneMode;
            newState.GameMenuOpen = GameMenu.Get()?.IsShown() ?? false;
            newState.OptionsMenuOpen = OptionsMenu.Get()?.IsShown() ?? false;
        }

        private void UpdateInGame(GameState newState)
        {
            var gameState = global::GameState.Get();
            if (gameState != null)
            {
                var friendlyPlayer = gameState.GetFriendlySidePlayer();
                var opposingPlayer = gameState.GetOpposingSidePlayer();

                var friendlyPlayedCards = friendlyPlayer.GetBattlefieldZone()?.GetCards() ?? EmptyCardList;
                var opposingPlayedCards = opposingPlayer.GetBattlefieldZone()?.GetCards() ?? EmptyCardList;

                foreach (var friendlyPlayedCard in friendlyPlayedCards)
                {
                    newState.MatchState.FriendlyPlayedCards.Add(new BrodeeCard(friendlyPlayedCard.GetEntity()));
                }

                foreach (var opposingPlayedCard in opposingPlayedCards)
                {
                    newState.MatchState.OpposingPlayedCards.Add(new BrodeeCard(opposingPlayedCard.GetEntity()));
                }
            }
        }
    }
}