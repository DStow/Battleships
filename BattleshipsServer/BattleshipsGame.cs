using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleshipsServer.Extensions;
using BattleshipsServer.Enumeration;

namespace BattleshipsServer
{
    public class BattleshipsGame
    {

        private ServerStateEnum _state = ServerStateEnum.Unknown;
        public ServerStateEnum GameState
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;

                // Fire off the game state changed event
                if (GameStateChanged != null)
                    GameStateChanged(this, null);
            }
        }

        public event EventHandler GameStateChanged;

        private PlayerBoard _player1Board, _player2Board;


        public BattleshipsGame()
        {
            GameState = ServerStateEnum.Player1Connect;
        }

        public string ProcessServerMessage(string message)
        {
            string result = "Error";

            // Split with |
            string[] messageParts = message.Split('|');

            if (GameState == ServerStateEnum.Player1Connect || GameState == ServerStateEnum.Player2Connect)
            {
                result = HandlePlayerConnects(messageParts);
            }
            else if (GameState == ServerStateEnum.PlayerPieces)
            {
                result = HandlePlayerPieces(messageParts);
            }

            return result;
        }

        private string HandlePlayerConnects(string[] messageParts)
        {
            string result = "";

            if (messageParts[0] == "Connect")
            {
                if (GameState == ServerStateEnum.Player1Connect)
                {
                    // Signal that they have connected as player 1
                    result = "OK|1";
                    GameState = ServerStateEnum.Player2Connect;
                }
                else if (GameState == ServerStateEnum.Player2Connect)
                {
                    // Signal that they have connected as player 2
                    result = "OK|2";
                    GameState = ServerStateEnum.PlayerPieces;
                }
            }

            return result;
        }

        private string HandlePlayerPieces(string[] messageParts)
        {
            string result = "";

            string playerNum = "";

            if (messageParts[0] == "Pieces")
            {
                if (messageParts.GetPiecesOrNull(1) == null)
                {
                    result = "No player number provided";
                }
                else if (messageParts.GetPiecesOrNull(2) == null)
                {
                    result = "No player pieces sent";
                }
                else
                {
                    string num = messageParts[1];

                    if (num != "1" && num != "2")
                    {
                        result = "Player number not valid";
                        return result;
                    }

                    string pieces = messageParts[2];

                    playerNum = num;

                    // ToDo: Validate player has sent the correct amount of pieces

                    if (_player1Board == null && playerNum == "1")
                    {
                        _player1Board = new PlayerBoard(pieces);
                        result = "OK";
                    }
                    else if (_player1Board != null && playerNum == "1")
                    {
                        result = "Pieces already recieved";
                    }
                    else if (_player2Board == null && playerNum == "2")
                    {
                        _player2Board = new PlayerBoard(pieces);
                        result = "OK";

                        // Go to the first players turn
                        GameState = ServerStateEnum.Player1Turn;
                    }
                    else if (_player2Board != null && playerNum == "2")
                    {
                        result = "Pieces already recieved";
                    }
                }
            }

            return result;
        }

        private string _lastPlay;
        private string HandlePlayerTurn(string[] messageParts)
        {
            string result = "";

            // Two commands - Recieve and Turn
            if(messageParts[0] == "Turn" && _lastPlay == null)
            {
                if(_state == ServerStateEnum.Player1Turn && messageParts[1] == "1")
                {
                    // Apply hit to board
                    _lastPlay = messageParts[2];
                    bool hit = _player2Board.ApplyHit(messageParts[2]);
                    if (hit)
                        return "HIT";
                    else
                        return "MISS";
                }
                else if(_state == ServerStateEnum.Player2Turn && messageParts[1] == "2")
                {
                    // Apply hit to board
                    _lastPlay = messageParts[2];
                    bool hit = _player1Board.ApplyHit(messageParts[2]);
                    if (hit)
                        return "HIT";
                    else
                        return "MISS";
                }
                else
                {
                    result = "Not Your Turn";
                }
            }
            else if(messageParts[0] == "Turn" && _lastPlay != null)
            {
                result = "Turn Already Taken";
            }
            else if(messageParts[1] =="Recieve" && _lastPlay != null)
            {
                result = "OK|" + _lastPlay;
                _lastPlay = null;

                // This triggers the move to the next player
                // ToDO: Check if the game is over maybe?
                if (_state == ServerStateEnum.Player1Turn)
                    _state = ServerStateEnum.Player2Turn;
                else if (_state == ServerStateEnum.Player2Turn)
                    _state = ServerStateEnum.Player1Turn;
            }
            else if(messageParts[1] == "Recieve" && _lastPlay == null)
            {
                result = "Pending";
            }

            return result;
        }
    }
}
