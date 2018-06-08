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
    public class BoardTile : GameObject
    {
        // X and Y on the board
        public Vector2 TileIndex { get; set; }

        public bool Hit { get; set; }

        public Vector2 _tileSize, _boardPos;

        public bool Highlight { get; set; }

        public BoardTile(Vector2 tileIndex, Vector2 tileSize, Vector2 boardPos)
        {
            TileIndex = tileIndex;
            _tileSize = tileSize;
            _boardPos = boardPos;
        }

        public BoardTile(Vector2 tileIndex)
        {
            TileIndex = tileIndex;
        }

        public Color OverlayColor { get; set; } = Color.White;

        public sealed override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            RectangleF drawArea = new RectangleF(_boardPos.X + (TileIndex.X * _tileSize.X), _boardPos.Y + (TileIndex.Y * _tileSize.Y), _tileSize.X, _tileSize.Y);

            DrawTile(drawArea, spriteBatch);

            if (Hit)
                DrawHitMarker(drawArea, spriteBatch);
        }

        public virtual void DrawTile(RectangleF drawArea, SpriteBatch spriteBatch)
        {

        }

        public void DrawHitMarker(RectangleF drawArea, SpriteBatch spriteBatch)
        {

        }
    }
}
