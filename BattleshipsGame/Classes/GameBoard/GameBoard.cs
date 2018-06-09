using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameUtilityLib;


namespace BattleshipsGame.Classes.GameBoard
{
    public class GameBoard : Board
    {
        public Ships.Ship[] Ships { get; set; }

        protected override void DrawTiles(SpriteBatch spriteBatch, Camera camera)
        {
            base.DrawTiles(spriteBatch, camera);

            if (Ships != null)
            {
                foreach (var ship in Ships)
                {
                    foreach (var tile in ship.ShipTiles)
                    {
                        tile.OverlayColor = Color.White;
                        tile._boardPos = this.Position;
                        tile._tileSize = new Vector2(this.Size.X / 10, this.Size.Y / 10);
                        tile.Draw(spriteBatch, camera);
                    }
                }
            }
        }
    }
}
