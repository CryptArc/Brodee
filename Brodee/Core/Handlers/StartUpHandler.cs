using Brodee.Components;
using Brodee.Controls;

namespace Brodee.Core.Handlers
{
    public class StartUpHandler : Handler
    {
        private readonly IGeneralControls _generalControls;

        public StartUpHandler(IGeneralControls generalControls)
        {
            _generalControls = generalControls;
        }

        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            _generalControls.MakeConfirmPopUp("Start Up", "Just some text when starting up!");
        }
    }
}