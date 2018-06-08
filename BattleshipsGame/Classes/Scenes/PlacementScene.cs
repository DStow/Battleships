using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGameUtilityLib;
using Microsoft.Xna.Framework.Content;
using MonoGameUtilityLib.MenuSupport;

namespace BattleshipsGame.Classes.Scenes
{
    /// <summary>
    /// This scene is used by the player to place their pieces on the world
    /// </summary>
    public class PlacementScene : Scene
    {
        // Has all of the ship placements been done?
        public bool PlacementDone { get; set; }

        private GameBoard.PlacementBoard _board;

        // Menu buttons
        private Button _doneButton;
        private Button _resetButton;


        public override void Initialize()
        {
            _board = new GameBoard.PlacementBoard();
            _board.Position = new Vector2(25, 60);
            _board.Size = new Vector2(520, 520);
            _board.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            _board.LoadContent(content);

            // Setup the menu controls
            _doneButton = new Button("Complete", new Vector2(611, 92), FontHandler.Instance.LargeFont, DoneButton_Click, BattleshipsGame.Graphics.GraphicsDevice);
            _doneButton.BorderHighlightColour = Color.Red;
            _doneButton.Visible = false;

            _resetButton = new Button("Reset", new Vector2(611, 160), FontHandler.Instance.LargeFont, ResetButton_Click, BattleshipsGame.Graphics.GraphicsDevice);
            _resetButton.BorderHighlightColour = Color.Red;
        }

        public override void Update(GameTime gameTime)
        {
            _board.Update(gameTime);

            _doneButton.Update(gameTime);
            _resetButton.Update(gameTime);

            if(_board.PlacementShip != null)
            {
                _doneButton.Visible = false;
            }
            else
            {
                _doneButton.Visible = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);

            spriteBatch.DrawString(FontHandler.Instance.LargeFont, "Placement Scene", new Vector2(25, 25), Color.White);

            _board.Draw(spriteBatch, camera);

            _doneButton.Draw(spriteBatch);
            _resetButton.Draw(spriteBatch);
        }

        #region Button Clicks
        private void DoneButton_Click(Button button)
        {
            PlacementDone = true;
        }

        private void ResetButton_Click(Button button)
        {
            _board.ResetBoard();
        }
        #endregion
    }
}
