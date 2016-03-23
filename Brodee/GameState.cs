namespace Brodee
{
    public interface IGameState
    {
        Scene Mode { get; }
        bool GameMenuOpen { get; }
        bool OptionsMenuOpen { get; }
        bool QuestLogOpen { get; }
        bool BrodeeMenuOpen { get; set; }

        IMatchState MatchState { get; }
    }

    public class GameState : IGameState
    {
        public Scene Mode { get; set; } = Scene.None;

        public bool GameMenuOpen { get; set; }
        public bool OptionsMenuOpen { get; set; }
        public bool BrodeeMenuOpen { get; set; }
        public bool QuestLogOpen { get; set; }

        public MatchState MatchState { get; set; }
        IMatchState IGameState.MatchState => MatchState;
    }
}