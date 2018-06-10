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
        private bool _gameReady = false;

        private GameBoard.GameBoard _playerBoard, _opponentBoard;

        private string _statusText = "";

        public Ships.Ship[] PlayerShips { get; set; }

        public bool GameOver { get; set; }

        public int _successfulHits = 0;
        public int _successfulEnemyHits = 0;

        public override void Initialize()
        {
            base.Initialize();

            // Setup the game board
            _playerBoard = new GameBoard.GameBoard();
            _playerBoard.Position = new Vector2(15, 120);
            _playerBoard.Size = new Vector2(380, 380);
            _playerBoard.Ships = this.PlayerShips;
            _playerBoard.MouseHoverEnabled = false;
            _playerBoard.Initialize();

            _opponentBoard = new GameBoard.GameBoard();
            _opponentBoard.Position = new Vector2(30 + 380, 120);
            _opponentBoard.Size = new Vector2(380, 380);
            _opponentBoard.Initialize();
            _opponentBoard.MouseHoverEnabled = false;

            // 20 times a second
            ConnectionTimer = new FixedTimer(1000);

            if (BattleshipsGame.PlayerNumber == 1)
                _myTurn = true;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            _playerBoard.LoadContent(content);
            _opponentBoard.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(_successfulEnemyHits==20 || _successfulHits == 20)
            {
                GameOver = true;
                return;
            }

            ConnectionTimer.Update(gameTime);

            HandleGameReady();

            if (_gameReady)
            {
                if (_myTurn)
                {
                    _opponentBoard.MouseHoverEnabled = true;
                }
                else
                {
                    _opponentBoard.MouseHoverEnabled = false;
                }
            }

            _playerBoard.Update(gameTime);
            _opponentBoard.Update(gameTime);

        }

        private void HandleGameReady()
        {
            if (_gameReady == false && ConnectionTimer.Tick)
            {
                _statusText = "Waiting for the other player to connect and place their pieces";
                // Check if both players have connected and placed their pieces before continuting
                _gameReady = ServerCommunications.CheckGameReady();
            }
            else if (ConnectionTimer.Tick && _myTurn == false)
            {
                // Send off the check request or something
                RecieveTurnResult res = ServerCommunications.RecieveOpponentMove();
                if (res.Pending == false && res.TileIndex != null)
                {
                    // Apply the players turn
                    bool hitLocal = _playerBoard.ApplyHit(res.TileIndex.Value);


                    if (!hitLocal)
                        _myTurn = true;
                    else
                    {
                        _myTurn = false;
                        _successfulEnemyHits++;
                    }
                }
            }
            else if (_myTurn && _gameReady == true)
            {
                if (_opponentBoard.SelectedTileIndex != null)
                {
                    bool? sendTurnResult = ServerCommunications.SendTurn(_opponentBoard.SelectedTileIndex.Value);
                    // send your turn
                    if (sendTurnResult.HasValue == false)
                    {
                        // They clicked too early
                    }
                    else if(sendTurnResult.Value)
                    {
                        // Hit enemy so stay on
                        _opponentBoard.ApplyHit(_opponentBoard.SelectedTileIndex.Value, true);
                        _opponentBoard.SelectedTileIndex = null;
                        _myTurn = true;
                        _successfulHits++;
                    }
                    else
                    {
                        _opponentBoard.ApplyHit(_opponentBoard.SelectedTileIndex.Value);
                        _opponentBoard.SelectedTileIndex = null;
                        _myTurn = false;
                    }
                }
                _statusText = "Your Turn!";
            }
            else if (!_myTurn && _gameReady == true)
            {
                _statusText = "Other players turn!";
            }
        }

        private void HandleMyTurn()
        {

        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);

            _playerBoard.Draw(spriteBatch, camera);
            _opponentBoard.Draw(spriteBatch, camera);

            spriteBatch.DrawString(FontHandler.Instance.MediumFont, "Your Board:", new Vector2(18, 91), Color.White);
            spriteBatch.DrawString(FontHandler.Instance.MediumFont, "Opponents Board:", new Vector2(413, 91), Color.White);

            spriteBatch.DrawString(FontHandler.Instance.LargeFont, _statusText, new Vector2(25, 25), Color.White);

        }
    }
}
