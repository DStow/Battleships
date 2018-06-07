using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameUtilityLib;


namespace BattleshipsGame.Classes.GameBoard.BoardTiles
{
    // Inherit from WaterTile as a ship tile will have water beneath it...
    public class ShipTile : WaterTile
    {
        public ShipTile(Vector2 tileIndex, Vector2 tileSize, Vector2 boardPos)
            : base(tileIndex, tileSize, boardPos)
        {

        }
    }
}
