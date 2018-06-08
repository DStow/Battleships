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
using MonoGameUtilityLib.MenuSupport;

using BattleshipsGame.Classes.Ships;

namespace BattleshipsGame.Classes.GameBoard
{
    public class PlacementBoard : Board
    {
        // Has all of the ship placements been done?
        public bool PlacementDone { get; set; }

        // Current ship that is being 'placed'
        private Ship _placementShip;

        // List of the currently places ships
        private List<Ship> _placedShips = new List<Ship>();

        // Direction the current ship is being placed
        private PlacementDirectionEnum _currentDirection = PlacementDirectionEnum.Right;

        // Differant ship types that can be placed
        private Type[] _shipTypes;

        // Index of the ship type we are currently placing
        private int _shipTypeIndex;

        // Menu buttons
        private Button _doneButton;
        private Button _resetButton;

        // Stop the space from being repeatedly fired
        private bool _isSpaceDown = false;

        // Stop the left mouse button from ebing repeatedly fireds
        private bool _isLeftDown = false;

        public PlacementBoard()
        {
            // Enable the mouse over effect on the base board
            this.MouseHoverEnabled = true;

            // Set the ship type index to teh start
            _shipTypeIndex = 0;

            // Setup all of the ship types in order the player can place
            _shipTypes = new Type[] {typeof(Battleship), typeof(Cruiser), typeof(Cruiser), typeof(Destroyer), typeof(Destroyer), typeof(Destroyer)
                , typeof(Submarine) , typeof(Submarine) , typeof(Submarine) , typeof(Submarine) };
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            // Setup the menu controls
            _doneButton = new Button("Complete", new Vector2(611, 92), FontHandler.Instance.LargeFont, DoneButton_Click, BattleshipsGame.Graphics.GraphicsDevice);
            _doneButton.BorderHighlightColour = Color.Red;
            _doneButton.Visible = false;

            _resetButton = new Button("Reset", new Vector2(611, 160), FontHandler.Instance.LargeFont, ResetButton_Click, BattleshipsGame.Graphics.GraphicsDevice);
            _resetButton.BorderHighlightColour = Color.Red;
        }

        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _doneButton.Update(gameTime);
            _resetButton.Update(gameTime);

            if (_shipTypeIndex >= _shipTypes.Count())
            {
                _placementShip = null;
                _doneButton.Visible = true;
            }
            else
            {
                _doneButton.Visible = false;

                GeneratePlacementShip();

                HandleRotation();

                HandlePlacement();
            }
        }

        private bool CanPlacementShipBePlaced()
        {
            bool result = true;
            // Check bounds
            foreach (var tile in _placementShip.ShipTiles)
            {
                if (tile.TileIndex.X < 0 || tile.TileIndex.X > 9 || tile.TileIndex.Y < 0 || tile.TileIndex.Y > 9)
                {
                    result = false;
                    return result;
                }
            }

            // Check already placed ships
            foreach (var ship in _placedShips)
            {
                foreach (var tile in ship.ShipTiles)
                {
                    // Check if they are touching
                    foreach (var shipTile in _placementShip.ShipTiles)
                    {
                        if (tile.TileIndex == shipTile.TileIndex)
                        {
                            return false;
                        }
                    }
                }
            }
            return result;
        }

        private void GeneratePlacementShip()
        {
            if (_previousMouseTile != null)
            {
                _placementShip = (Ships.Ship)Activator.CreateInstance(_shipTypes[_shipTypeIndex]);

                _placementShip.TileIndex = _previousMouseTile.TileIndex;
                _placementShip.Direction = _currentDirection;


                _placementShip.Initialize();
                _placementShip.LoadContent(BattleshipsGame.GameContent);
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

        private void HandlePlacement()
        {
            MouseState ms = Mouse.GetState();

            if (this.AreaF.Contains(new Vector2(ms.Position.X, ms.Position.Y)))
            {
                if (ms.LeftButton == ButtonState.Pressed && _previousMouseTile != null && _isLeftDown == false && CanPlacementShipBePlaced())
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
        }
        #endregion  

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);

            _doneButton.Draw(spriteBatch);
            _resetButton.Draw(spriteBatch);
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
                if (CanPlacementShipBePlaced() == false)
                {
                    tileOverlay = Color.Red;

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

        #region Button Clicks
        private void DoneButton_Click(Button button)
        {
            PlacementDone = true;
        }

        private void ResetButton_Click(Button button)
        {
            _placedShips = new List<Ship>();
            _shipTypeIndex = 0;
        }
        #endregion
    }
}
