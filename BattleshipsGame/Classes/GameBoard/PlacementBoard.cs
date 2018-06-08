using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameUtilityLib;
using Microsoft.Xna.Framework.Content;

using BattleshipsGame.Classes.Ships;

namespace BattleshipsGame.Classes.GameBoard
{
    public class PlacementBoard : Board
    {
        private Ships.Ship _placementShip;

        private List<Ships.Ship> _placedShips = new List<Ships.Ship>();

        private PlacementDirectionEnum _currentDirection = PlacementDirectionEnum.Right;

        private Type _currentShipType;

        private Type[] _shipTypes;
        private int _shipTypeIndex;

        public PlacementBoard()
        {
            MouseHoverEnabled = true;
            _currentShipType = typeof(Ships.Cruiser);

            _shipTypeIndex = 0;
            _shipTypes = new Type[] {typeof(Battleship), typeof(Cruiser), typeof(Cruiser), typeof(Destroyer), typeof(Destroyer), typeof(Destroyer)
                , typeof(Submarine) , typeof(Submarine) , typeof(Submarine) , typeof(Submarine) };
        }

        private ContentManager _content;
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            _content = content;
        }

        private bool _isSpaceDown = false;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GeneratePlacementShip();

            HandleRotation();

            HandlePlacement();
        }

        private void GeneratePlacementShip()
        {
            if (_previousMouseTile != null)
            {
                _placementShip = (Ships.Ship)Activator.CreateInstance(_shipTypes[_shipTypeIndex]);

                _placementShip.TileIndex = _previousMouseTile.TileIndex;
                _placementShip.Direction = _currentDirection;


                _placementShip.Initialize();
                _placementShip.LoadContent(_content);
            }
        }

        private void HandleRotation()
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Space) && _isSpaceDown == false)
            {
                int dir = (int)_currentDirection;
                dir++;
                if (dir > 3)
                    dir = 0;

                _currentDirection = (PlacementDirectionEnum)dir;
                _isSpaceDown = true;
            }
            else if (ks.IsKeyUp(Keys.Space))
            {
                _isSpaceDown = false;
            }
        }

        private bool _isLeftDown = false;
        private void HandlePlacement()
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed && _previousMouseTile != null && _isLeftDown == false)
            {
                _placedShips.Add(_placementShip);
                _shipTypeIndex++;
                _isLeftDown = true;
            }
            else if (ms.LeftButton == ButtonState.Released)
            {
                _isLeftDown = false;
            }
        }

        protected override void DrawTiles(SpriteBatch spriteBatch, Camera camera)
        {
            base.DrawTiles(spriteBatch, camera);

            foreach (var ship in _placedShips)
            {
                foreach (var tile in ship.ShipTiles)
                {
                    tile.OverlayColor = Color.White;
                    tile._boardPos = this.Position;
                    tile._tileSize = new Vector2(this.Size.X / 10, this.Size.Y / 10);
                    tile.Draw(spriteBatch, camera);
                }
            }

            if (_placementShip != null)
            {
                Color tileOverlay = Color.White;

                // Loop through tiles and see if all fo them are inside the board bounds
                foreach (var tile in _placementShip.ShipTiles)
                {
                    if (tile.TileIndex.X < 0 || tile.TileIndex.X > 9 || tile.TileIndex.Y < 0 || tile.TileIndex.Y > 9)
                    {
                        tileOverlay = Color.Red;
                    }
                }

                foreach (var tile in _placementShip.ShipTiles)
                {
                    tile.OverlayColor = tileOverlay;

                    if (tile.TileIndex.X < 0 || tile.TileIndex.X > 9 || tile.TileIndex.Y < 0 || tile.TileIndex.Y > 9)
                    {

                    }
                    else
                    {
                        tile._boardPos = this.Position;
                        tile._tileSize = new Vector2(this.Size.X / 10, this.Size.Y / 10);
                        tile.Draw(spriteBatch, camera);
                    }
                }
            }
        }
    }
}
