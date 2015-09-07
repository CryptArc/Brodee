using UnityEngine;

namespace BrodeStone
{
    public interface IHandler
    {
        HandlerType GetHandlerType { get; }

        void Handle(GameObject parent, GameState previous, GameState next);
    }
    
}