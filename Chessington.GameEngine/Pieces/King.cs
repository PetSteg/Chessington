using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player)
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

        private Square GetMove(Square currentSquare, int rowOffset, int colOffset)
        {
            return new Square(currentSquare.Row + rowOffset, currentSquare.Col + colOffset);
        }

        private List<Square> GetMovesFromOffsets(Square currentSquare, int[] rowOffsets, int[] colOffsets)
        {
            return rowOffsets.Select((_, i) => GetMove(currentSquare, rowOffsets[i], colOffsets[i])).ToList();
        }

        private List<Square> DiagonalMoves(Square currentSquare)
        {
            var rowOffsets = new[] { 1, 1, -1, -1 };
            var colOffsets = new[] { 1, -1, 1, -1 };

            return GetMovesFromOffsets(currentSquare, rowOffsets, colOffsets);
        }

        private List<Square> StraightMoves(Square currentSquare)
        {
            var rowOffsets = new[] { 0, 0, 1, -1 };
            var colOffsets = new[] { 1, -1, 0, 0 };

            return GetMovesFromOffsets(currentSquare, rowOffsets, colOffsets);
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);

            return StraightMoves(currentSquare).Concat(DiagonalMoves(currentSquare)).Where(ValidSquare);
        }
    }
}