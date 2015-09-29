using BrodeStone.Triggers;
using UnityEngine;

namespace BrodeStone.Handlers
{
    public interface IHandler
    {
        void Setup(GameObject parent);
        Trigger[] SpecificHandle(GameState previous, GameState next);
    }

    public abstract class Handler : IHandler
    {
        protected readonly Trigger[] EmptyTriggers = new Trigger[0];
        protected GameObject Parent;

        public virtual void Setup(GameObject parent)
        {
            Parent = parent;
        }

        public Trigger[] Handle(GameState previous, GameState next)
        {
            return SpecificHandle(previous, next);
        }

        public abstract Trigger[] SpecificHandle(GameState previous, GameState next);
    }

}