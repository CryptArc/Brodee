using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Components
{
    public interface IHandler
    {
        void Setup(GameObject parent);
        void SpecificHandle(IGameState previous, IGameState next);
    }

    public abstract class Handler : IHandler
    {
        protected readonly Trigger[] EmptyTriggers = new Trigger[0];
        protected GameObject Parent;

        public virtual void Setup(GameObject parent)
        {
            Parent = parent;
        }

        public void Handle(IGameState previous, IGameState next)
        {
            SpecificHandle(previous, next);
        }

        public abstract void SpecificHandle(IGameState previous, IGameState next);
    }

}