using System;
using System.Collections.Generic;

namespace BrodeStone
{
    public class TileSorter : IComparer<TileWrapper>
    {
        public int Compare(TileWrapper x, TileWrapper y)
        {
            if (x == null || y == null)
            {
                Logger.AppendLine("TileWrapper was null");
                return 0;
            }
            else
                Logger.AppendLine("TileWrapper was not null");

            string xcardId = x.Tile.GetCardID();
            string ycardId = y.Tile.GetCardID();

            if (xcardId == null || ycardId == null)
            {
                Logger.AppendLine("GetCardID was null");
                return 0;
            }
            else
                Logger.AppendLine("GetCardID was not null");

            var xEntityDef = DefLoader.Get().GetEntityDef(xcardId);
            var yEntityDef = DefLoader.Get().GetEntityDef(ycardId);

            if (xEntityDef == null || yEntityDef == null)
            {
                Logger.AppendLine("Tile Sorter getting def was null");
                return 0;
            }
            else
                Logger.AppendLine("Tile Sorter getting def was not null");

            var xCost = xEntityDef.GetCost();
            var yCost = yEntityDef.GetCost();
            
            if (xCost != yCost)
                return xCost.CompareTo(yCost);
            return string.Compare(xEntityDef.GetName(), yEntityDef.GetName(), StringComparison.Ordinal);
        }
    }
}