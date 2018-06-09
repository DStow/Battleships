using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TCPClientLibNetFramework;

namespace BattleshipsGame.Classes
{
    /// <summary>
    /// This class handles all of the communications with the server
    /// </summary>
    public static class ServerCommunications
    {
        // Connect and get back the player number
        public static bool Connect()
        {
            if (Settings.DebugCommuncationsMode)
            {
                BattleshipsGame.PlayerNumber = 1;
                return true;
            }

            Client c = new Client(BattleshipsGame.ServerIP, BattleshipsGame.ServerPort);
            string result = "";
            try
            {
                result = c.SendMessage("Connect");
            }
            catch
            {
                return false;
            }

            string[] parts = result.Split('|');

            if (parts.Length == 2)
            {
                if (parts[0] == "OK")
                {
                    BattleshipsGame.PlayerNumber = Convert.ToInt32(parts[1]);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Send the players ship pieces off to the server
        public static bool SendPieces(string placementData)
        {
            if (Settings.DebugCommuncationsMode)
            {
                BattleshipsGame.PlayerNumber = 1;
                return true;
            }

            Client c = new Client(BattleshipsGame.ServerIP, BattleshipsGame.ServerPort);
            string result = "";
            try
            {
                result = c.SendMessage("Pieces|" + BattleshipsGame.PlayerNumber + "|" + placementData);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static RecieveTurnResult RecieveOpponentMove()
        {
            if (Settings.DebugCommuncationsMode)
            {
                BattleshipsGame.PlayerNumber = 1;
                return new RecieveTurnResult() { Pending = true };
            }

            Client c = new Client(BattleshipsGame.ServerIP, BattleshipsGame.ServerPort);

            string result = "";
            result = c.SendMessage("Recieve|" + BattleshipsGame.PlayerNumber);

            RecieveTurnResult recieveResult = new RecieveTurnResult();
            string[] parts = result.Split('|');
            if (result == "Pending")
            {
                recieveResult.Pending = true;
            }
            else if (parts[0] == "OK")
            {
                recieveResult.Pending = false;
                string[] indexParts = parts[1].Split(',');
                recieveResult.TileIndex = new Vector2(Convert.ToInt32(indexParts[0]), Convert.ToInt32(indexParts[1]));
            }

            return recieveResult;
        }
    }

    public class RecieveTurnResult
    {
        public bool Pending { get; set; }
        public Vector2? TileIndex { get; set; }

    }
}
