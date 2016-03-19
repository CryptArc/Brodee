using System;
using System.Collections.Generic;
using System.Linq;
using Brodee.Components;

namespace Brodee.Core.Handlers
{
    public class TweenAdjusterHandler : Handler
    {
        public override void SpecificHandle(IGameState previous, IGameState next)
        {

            var cards = global::GameState.Get()?.GetFriendlySidePlayer()?.GetBattlefieldZone()?.GetCards();
            if (cards != null)
            {
                foreach (var card in cards)
                {
                    CardStuff.SetAllAnimationSpeed(card, 10f);
                }
            }
        }
    }
}