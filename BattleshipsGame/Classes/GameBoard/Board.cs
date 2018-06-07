using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameUtilityLib;

namespace BattleshipsGame.Classes.GameBoard
{
    public class Board : GameObject
    {
        public List<BoardTiles.BoardTile> Tiles { get; set; } = new List<BoardTiles.BoardTile>();

        private Texture2D _gridOverlay;

        public Board()
        {

        }

        public override void Initialize()
        {
            Vector2 tileSize = new Vector2(Size.X / 10, Size.Y / 10);

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    BoardTiles.WaterTile wt = new BoardTiles.WaterTile(new Vector2(x, y), tileSize, Position);
                    Tiles.Add(wt);
                }
            }
        }

        public override void LoadContent(ContentManager content)
        {
            foreach (var tile in Tiles)
            {
                tile.LoadContent(content);
            }

            _gridOverlay = content.Load<Texture2D>("board_grid");
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            foreach(var tile in Tiles)
            {
                tile.Draw(spriteBatch, camera);
            }

            RectangleF drawArea = new RectangleF(Position.X, Position.Y, Size.X, Size.Y);
            spriteBatch.Draw(_gridOverlay, drawArea, Color.White);
        }
    }
}
