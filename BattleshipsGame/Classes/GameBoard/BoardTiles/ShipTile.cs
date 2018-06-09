using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameUtilityLib;


namespace BattleshipsGame.Classes.GameBoard.BoardTiles
{
    // Inherit from WaterTile as a ship tile will have water beneath it...
    public class ShipTile : WaterTile
    {

        private Texture2D _shipTexture;

        public ShipTile(Vector2 tileIndex, Vector2 tileSize, Vector2 boardPos)
            : base(tileIndex, tileSize, boardPos)
        {

        }

        public ShipTile(Vector2 tileIndex)
            : base(tileIndex)
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            _shipTexture = content.Load<Texture2D>("tile_ship");
        }

        public override void DrawTile(RectangleF drawArea, SpriteBatch spriteBatch)
        {
            base.DrawTile(drawArea, spriteBatch);

            if (Hit)
                OverlayColor = Color.OrangeRed;

            spriteBatch.Draw(_shipTexture, drawArea, OverlayColor);
        }
    }
}
