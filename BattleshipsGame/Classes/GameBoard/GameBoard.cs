using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameUtilityLib;


namespace BattleshipsGame.Classes.GameBoard
{
    public class GameBoard : Board
    {
        public Ships.Ship[] Ships { get; set; }

        public Vector2? SelectedTileIndex { get; set; }


        private bool _isLeftDown = false;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            MouseState ms = Mouse.GetState();

            if (this.AreaF.Contains(new Vector2(ms.Position.X, ms.Position.Y)))
            {
                if (ms.LeftButton == ButtonState.Pressed && _previousMouseTile != null && _isLeftDown == false)
                {
                    // Check if it's a valid spot
                    SelectedTileIndex = _previousMouseTile.TileIndex;
                    _isLeftDown = true;
                }
                else if (ms.LeftButton == ButtonState.Released)
                {
                    _isLeftDown = false;
                }
            }
        }

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

        public bool ApplyHit(Vector2 tileIndex, bool shipHit = false)
        {
            int index = (int)((tileIndex.Y * 10) + tileIndex.X);
            Tiles[index].Hit = true;

            if (Ships != null)
            {
                foreach(var ship in Ships)
                {
                    foreach(var tile in ship.ShipTiles)
                    {
                        if(tile.TileIndex == tileIndex)
                        {
                            tile.Hit = true;
                            return true;
                        }
                    }
                }
            }

            if (shipHit)
            {
                Tiles[index] = new BoardTiles.ShipTile(tileIndex, Tiles[index]._tileSize, Tiles[index]._boardPos);
                Tiles[index].Initialize();
                Tiles[index].LoadContent(BattleshipsGame.GameContent);
                Tiles[index].Hit = true;
            }


            return false;
        }
    }
}
