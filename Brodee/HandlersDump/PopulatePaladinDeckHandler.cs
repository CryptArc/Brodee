using System.IO;
using System.Linq;
using Brodee.Components;

namespace Brodee.HandlersDump
{
    public class PopulatePaladinDeckHandler : Handler
    {
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            var cdt = CollectionDeckTray.Get();
            var collectionMgr = CollectionManager.Get();
            var rdmDeck = collectionMgr.GetDecks().Values.First().GetDeckFillFromString(File.ReadAllText("H:\\deck.txt"));
            cdt.PopulateDeck(rdmDeck);
        }
    }
}