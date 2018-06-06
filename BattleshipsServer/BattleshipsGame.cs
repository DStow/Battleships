using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleshipsServer.Extensions;

namespace BattleshipsServer
{
    public class BattleshipsGame
    {
        public enum ServerStateEnum
        {
            Player1Connect,
            Player2Connect,
            PlayerPieces,
            Player1Turn,
            Player2Turn,
            GameOver,
            Unknown
        }

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

        private string player1Pieces = null, player2Pieces = null;

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

                    if (player1Pieces == null && playerNum == "1")
                    {
                        player1Pieces = pieces;
                        result = "OK";
                    }
                    else if (player1Pieces != null && playerNum == "1")
                    {
                        result = "Pieces already recieved";
                    }
                    else if (player2Pieces == null && playerNum == "2")
                    {
                        player2Pieces = pieces;
                        result = "OK";

                        // Go to the first players turn
                        GameState = ServerStateEnum.Player1Turn;
                    }
                    else if (player2Pieces != null && playerNum == "2")
                    {
                        result = "Pieces already recieved";
                    }
                }
            }

            return result;
        }
    }
}
