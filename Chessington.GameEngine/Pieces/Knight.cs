using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player)
        {
        }

        private bool ValidSquare(Square square)
        {
            var row = square.Row;
            if (row < 0 || row > 7) return false;

            var col = square.Col;
            if (col < 0 || col > 7) return false;

            return true;
        }

        private bool FriendlySquare(Square square, Board board)
        {
            var piece = board.GetPiece(square);
            if (piece == null) return false;

            return board.GetPiece(square).Player == Player;
        }

        private Square GetMove(Square currentSquare, int rowOffset, int colOffset)
        {
            return new Square(currentSquare.Row + rowOffset, currentSquare.Col + colOffset);
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);

            var rowOffsets = new[] { 1, 1, -1, -1, 2, 2, -2, -2 };
            var colOffsets = new[] { 2, -2, 2, -2, 1, -1, 1, -1 };

            return rowOffsets.Select((_, i) => GetMove(currentSquare, rowOffsets[i], colOffsets[i])).Where(ValidSquare)
                .Where(move => !FriendlySquare(move, board));
        }
    }
}