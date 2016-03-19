using System;
using System.Collections.Generic;
using Brodee.Components;
using Newtonsoft.Json;
using UnityEngine;

namespace Brodee.Core.Handlers
{
    public class GameStateUpdateHandler : Handler
    {
        private static readonly List<global::Card> EmptyCardList = new List<global::Card>();

        private readonly Func<GameState> _newGameStateFunc;
        private readonly Func<GameState> _oldGameStateFunc;

        public GameStateUpdateHandler(Func<GameState> oldGameStateFunc, Func<GameState> newGameStateFunc)
        {
            _oldGameStateFunc = oldGameStateFunc;
            _newGameStateFunc = newGameStateFunc;
        }

        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            var newGameState = _newGameStateFunc();
            var oldGameState = _oldGameStateFunc();

            var newSceneMode = SceneMgr.Get().GetMode().MapToScene();
            if (oldGameState.Mode != newSceneMode)
                Logger.AppendLine($"Changing scene to {newSceneMode}");

            newGameState.Mode = newSceneMode;
            newGameState.GameMenuOpen = GameMenu.Get()?.IsShown() ?? false;
            newGameState.OptionsMenuOpen = OptionsMenu.Get()?.IsShown() ?? false;


            var gameState = global::GameState.Get();
            if (gameState != null)
            {
                var friendlyPlayer = gameState.GetFriendlySidePlayer();
                var opposingPlayer = gameState.GetOpposingSidePlayer();

                var friendlyPlayedCards = friendlyPlayer.GetBattlefieldZone()?.GetCards() ?? EmptyCardList;
                var opposingPlayedCards = opposingPlayer.GetBattlefieldZone()?.GetCards() ?? EmptyCardList;
                if (Time.frameCount % 30 != 0)
                    return;
                Logger.AppendLine("GameStateUpdateHandler");
                Logger.AppendLine($"friendlyPlayedCards is Empty:{friendlyPlayedCards.Count == 0}");

                foreach (var friendlyPlayedCard in friendlyPlayedCards)
                {
                    newGameState.FriendlyPlayedCards.Add(new Card(friendlyPlayedCard.GetEntity()));
                }

                foreach (var opposingPlayedCard in opposingPlayedCards)
                {
                    newGameState.OpposingPlayedCards.Add(new Card(opposingPlayedCard.GetEntity()));
                }
            }
        }
    }
}