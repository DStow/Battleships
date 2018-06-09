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
    public class WaterTile : BoardTile
    {
        private Texture2D _waterTexture;

        public WaterTile(Vector2 tileIndex, Vector2 tileSize, Vector2 boardPos)
            : base(tileIndex, tileSize, boardPos)
        {

        }

        public WaterTile(Vector2 tileIndex)
            : base(tileIndex)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            _waterTexture = content.Load<Texture2D>("tile_water");
        }

        public override void DrawTile(RectangleF drawArea, SpriteBatch spriteBatch)
        {
            base.DrawTile(drawArea, spriteBatch);

            Color col = Color.White;
            if (Highlight)
                col = Color.IndianRed;

            if (Hit)
                col = Color.Orange;

            spriteBatch.Draw(_waterTexture, drawArea, col);
        }
    }
}
