using System.IO;
using System.Linq;
using BrodeStone.Triggers;

namespace BrodeStone.Handlers
{
    public class PopulatePaladinDeckHandler : Handler
    {
        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            var cdt = CollectionDeckTray.Get();
            var collectionMgr = CollectionManager.Get();
            var rdmDeck = collectionMgr.GetDecks().Values.First().CreateRDMFromDeckString(File.ReadAllText("H:\\deck.txt"));
            cdt.PopulateDeck(rdmDeck);
            return EmptyTriggers;
        }
    }
}