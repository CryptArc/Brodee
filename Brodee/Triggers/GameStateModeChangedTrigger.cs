namespace Brodee.Triggers
{
    public class GameStateModeChangedTrigger : Trigger
    {
        public Scene Scene { get; private set; }

        public GameStateModeChangedTrigger(Scene scene)
        {
            Scene = scene;
        }
    }
}