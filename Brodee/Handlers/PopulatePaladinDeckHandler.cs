using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Brodee.Handlers
{
    public class PopulatePaladinDeckHandler : Handler
    {
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            var cdt = CollectionDeckTray.Get();
            var collectionMgr = CollectionManager.Get();
            var rdmDeck = collectionMgr.GetDecks().Values.First().CreateRDMFromDeckString(File.ReadAllText("H:\\deck.txt"));
            cdt.PopulateDeck(rdmDeck);
        }
    }
}