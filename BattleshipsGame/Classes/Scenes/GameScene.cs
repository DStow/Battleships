using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameUtilityLib;
using MonoGameUtilityLib.Timers;

namespace BattleshipsGame.Classes.Scenes
{
    /// <summary>
    /// This scene is used by the player to play the game!
    /// </summary>
    public class GameScene : Scene
    {
        public FixedTimer ConnectionTimer { get; set; }

        private bool _myTurn = false;

        private GameBoard.Board _gameBoard;

        public override void Initialize()
        {
            base.Initialize();

            // Setup the game board
            _gameBoard = new GameBoard.Board();
            _gameBoard.Position = new Vector2(25, 60);
            _gameBoard.Size = new Vector2(520, 520);
            _gameBoard.Initialize();

            // 20 times a second
            ConnectionTimer = new FixedTimer(1000 / 20);

            if (BattleshipsGame.PlayerNumber == 1)
                _myTurn = true;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            _gameBoard.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            ConnectionTimer.Update(gameTime);

            if (ConnectionTimer.Tick && _myTurn == false)
            {
                // Send off the check request or something
                RecieveTurnResult res = ServerCommunications.RecieveOpponentMove();
                if (res.Pending == false && res.TileIndex != null)
                {
                    // Apply the players turn


                    _myTurn = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);

            _gameBoard.Draw(spriteBatch, camera);
        }
    }
}
