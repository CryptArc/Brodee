using Brodee.Components;
using Brodee.Triggers;

namespace Brodee.Handlers
{
    public class CardTileAttemptHandler : Handler
    {
        DeckTileHolder _tileHolder = new DeckTileHolder();
        
        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            _tileHolder.Update(Parent);
            _tileHolder.AddCard("GVG_058");
            _tileHolder.AddCard("GVG_059");
            _tileHolder.AddCard("GVG_060");

            return EmptyTriggers;
        }
    }
}