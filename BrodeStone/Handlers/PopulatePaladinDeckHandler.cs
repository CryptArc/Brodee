using System.IO;
using System.Linq;
using UnityEngine;

namespace BrodeStone.Handlers
{
    public class PopulatePaladinDeckHandler : IHandler
    {
        public HandlerType GetHandlerType => HandlerType.PopulatePaladinDeck;

        public void Handle(GameObject component, GameState previous, GameState next)
        {
            var cdt = CollectionDeckTray.Get();
            var collectionMgr = CollectionManager.Get();
            var rdmDeck = collectionMgr.GetDecks().Values.First().CreateRDMFromDeckString(File.ReadAllText("H:\\deck.txt"));
            cdt.PopulateDeck(rdmDeck);
        }
    }
}