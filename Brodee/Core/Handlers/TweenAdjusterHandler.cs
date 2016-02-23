using System.Collections.Generic;
using Brodee.Components;

namespace Brodee.Core.Handlers
{
    public class TweenAdjusterHandler : Handler
    {
        readonly HashSet<iTween> _iTweens = new HashSet<iTween>();
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            var iTweenIt = iTweenManager.GetIterator();
            iTween current = iTweenIt.GetNext();
            while (current != null)
            {
                if (!_iTweens.Contains(current))
                {
                    current.time /= 2.0f;
                    _iTweens.Add(current);
                }
                current = iTweenIt.GetNext();
            }

            _iTweens.RemoveWhere(x => x.destroyed);
        }
    }
}