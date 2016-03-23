using Brodee.Triggers;

namespace Brodee.Components
{
    public interface ITriggerHandler
    {
        void Handle(Trigger trigger, IGameState next);
    }

    public abstract class TriggerHandler<T> : ITriggerHandler where T : Trigger
    {
        public abstract void SpecificHandle(T trigger, IGameState next);

        public void Handle(Trigger trigger, IGameState next)
        {
            SpecificHandle(trigger as T, next);
        }
    }
}