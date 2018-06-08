using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameUtilityLib;

namespace BattleshipsGame.Classes.GameBoard
{
    public class Board : GameObject
    {
        public BoardTiles.BoardTile[] Tiles { get; set; }

        public bool MouseHoverEnabled { get; set; }

        private Texture2D _gridOverlay;

        public Board()
        {
            MouseHoverEnabled = true;
        }

        public override void Initialize()
        {
            Vector2 tileSize = new Vector2(Size.X / 10, Size.Y / 10);

            Tiles = new BoardTiles.BoardTile[10 * 10];

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    BoardTiles.WaterTile wt = new BoardTiles.WaterTile(new Vector2(x, y), tileSize, Position);
                    Tiles[(y * 10) + x] = wt;
                }
            }
        }

        protected BoardTiles.BoardTile _previousMouseTile;
        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            // BattleshipsGame.GameWindow.Title = ms.Position.ToString();

            if (_previousMouseTile != null)
                _previousMouseTile.Highlight = false;

            // Tile index 
            if (AreaF.Contains(new Vector2(ms.X, ms.Y)))
            {
                Vector2 relativePos = new Vector2(ms.X - AreaF.X, ms.Y - AreaF.Y);

                Vector2 tileSize = new Vector2(Size.X / 10, Size.Y / 10);
                int yCount = Convert.ToInt32(relativePos.Y) / Convert.ToInt32(tileSize.Y);
                yCount = yCount * 10;

                int xCount = Convert.ToInt32(relativePos.X) / Convert.ToInt32(tileSize.X);

                int mouseTileIndex = xCount + yCount;

                if (mouseTileIndex <= 10 * 10 - 1)
                {
                    _previousMouseTile = Tiles[mouseTileIndex];
                    _previousMouseTile.Highlight = true;
                }

            }
            //int mouseTileIndex = 
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

            DrawTiles(spriteBatch, camera);

            DrawGrid(spriteBatch, camera);

        }

        protected virtual void DrawTiles(SpriteBatch spriteBatch, Camera camera)
        {
            foreach (var tile in Tiles)
            {
                tile.Draw(spriteBatch, camera);
            }
        }

        protected virtual void DrawGrid(SpriteBatch spriteBatch, Camera camera)
        {
            RectangleF drawArea = new RectangleF(Position.X, Position.Y, Size.X, Size.Y);
            spriteBatch.Draw(_gridOverlay, drawArea, Color.White);
        }
    }
}
