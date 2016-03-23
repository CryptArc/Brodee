using Brodee.Components;
using Brodee.Triggers;

namespace Brodee.Core.Handlers
{
    public class CardTileAttemptHandler : TriggerHandler<SliderAttemptTrigger>
    {
        DeckTileHolder _tileHolder = new DeckTileHolder();

        public override void SpecificHandle(SliderAttemptTrigger trigger, IGameState next)
        {
            //_tileHolder.Update(Parent);
            _tileHolder.AddCard("GVG_058");
            _tileHolder.AddCard("GVG_059");
            _tileHolder.AddCard("GVG_060");
        }
    }
}