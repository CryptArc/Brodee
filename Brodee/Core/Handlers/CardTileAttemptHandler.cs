using Brodee.Components;
using Brodee.HandlersDump;

namespace Brodee.Core.Handlers
{
    public class CardTileAttemptHandler : Handler
    {
        DeckTileHolder _tileHolder = new DeckTileHolder();
        
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            _tileHolder.Update(Parent);
            _tileHolder.AddCard("GVG_058");
            _tileHolder.AddCard("GVG_059");
            _tileHolder.AddCard("GVG_060");
        }
    }
}