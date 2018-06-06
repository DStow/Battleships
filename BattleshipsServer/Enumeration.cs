using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsServer.Enumeration
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

    public enum BoardPieceStateEnum
    {
        Fine,
        Hit
    }
}
