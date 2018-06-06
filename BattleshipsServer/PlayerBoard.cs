using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleshipsServer.Enumeration;
using BattleshipsServer.Extensions;
using BattleshipsServer.Exceptions;

namespace BattleshipsServer
{
    public class PlayerBoard
    {
        private BoardPiece[] _pieces;

        public PlayerBoard(string playerPieces)
        {
            // Parse the player pieces
            string[] pieces = playerPieces.Split(';');

            if (pieces.Count() != 20)
            {
                throw new PlayerBoardInvalidPieceCountException(pieces.Count());
            }

            List<BoardPiece> finalPieces = new List<BoardPiece>();

            foreach(string piece in pieces)
            {
                // Split and assemble a board piece
                string[] parts = piece.Split(',');
                BoardPiece boardPiece = new BoardPiece()
                {
                    X = Convert.ToInt32(parts[0]),
                    Y = Convert.ToInt32(parts[1]),
                    State = BoardPieceStateEnum.Fine
                };

                finalPieces.Add(boardPiece);
            }

            _pieces = finalPieces.ToArray();
        }

        public bool ApplyHit(string targetPos)
        {
            string[] parts = targetPos.Split(',');
            int x = Convert.ToInt32(parts[0]);
            int y = Convert.ToInt32(parts[1]);

            for(int i = 0; i < _pieces.Length; i++)
            {
                if(_pieces[i].X == x && _pieces[i].Y == y)
                {
                    _pieces[i].State = BoardPieceStateEnum.Hit;
                    return true;
                }
            }

            return false;
        }

        private struct BoardPiece
        {
            public int X { get; set; }
            public int Y { get; set; }
            public BoardPieceStateEnum State { get; set; }
        }
    }
}
