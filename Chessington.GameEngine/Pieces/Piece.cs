using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public abstract class Piece
    {
        protected Square currentSquare;
        protected Board board;

        public Player Player { get; private set; }

        protected Piece(Player player)
        {
            Player = player;
        }

        protected bool OpponentSquare(Square square)
        {
            var piece = board?.GetPiece(square);
            if (piece == null) return false;

            return board.GetPiece(square).Player != Player;
        }

        protected bool FriendlySquare(Square square)
        {
            var piece = board?.GetPiece(square);
            if (piece == null) return false;

            return board.GetPiece(square).Player == Player;
        }

        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

        public void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
        }
    }
}