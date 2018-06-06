using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsServer.Exceptions
{
    public class PlayerBoardInvalidPieceCountException : ApplicationException
    {
        public PlayerBoardInvalidPieceCountException(int actualCount)
            : base("Expected 20 pieces, received: " + actualCount)
        {

        }
    }
}
