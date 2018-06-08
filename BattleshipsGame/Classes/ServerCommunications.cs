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
                result = c.SendMessage("Pieces|"  +BattleshipsGame.PlayerNumber + "|" + placementData);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
