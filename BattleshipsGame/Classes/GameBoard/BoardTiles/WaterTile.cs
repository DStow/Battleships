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

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            _waterTexture = content.Load<Texture2D>("tile_water");
        }

        public override void DrawTile(RectangleF drawArea, SpriteBatch spriteBatch)
        {
            base.DrawTile(drawArea, spriteBatch);

            spriteBatch.Draw(_waterTexture, drawArea, Color.White);
        }
    }
}
