using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsServer
{
    public class BattleshipsGame
    {
        private enum ServerStateEnum
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



        public BattleshipsGame()
        {
            _state = ServerStateEnum.Player1Connect;
        }

        public string ProcessServerMessage(string message)
        {
            string result = "Error";

            // Split with |
            string[] messageParts = message.Split('|');

            if(_state == ServerStateEnum.Player1Connect || _state == ServerStateEnum.Player2Connect)
            {
                result = HandlePlayerConnects(messageParts);
            }

            return result;
        }

        private string HandlePlayerConnects(string[] messageParts)
        {
            string result = "";

            if(_state == ServerStateEnum.Player1Connect)
            {
                // Signal that they have connected as player 1
                result = "OK|1";
                _state = ServerStateEnum.Player2Connect;
            }
            else if(_state == ServerStateEnum.Player2Connect)
            {
                // Signal that they have connected as player 2
                result = "OK|2";
                _state = ServerStateEnum.PlayerPieces;
            }

            return result;
        }
    }
}
