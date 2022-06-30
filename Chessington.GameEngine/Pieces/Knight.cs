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

        private Square GetMove(Square currentSquare, int rowOffset, int colOffset)
        {
            return new Square(currentSquare.Row + rowOffset, currentSquare.Col + colOffset);
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);

            var rowOffsets = new[] { 1, 1, -1, -1, 2, 2, -2, -2 };
            var colOffsets = new[] { 2, -2, 2, -2, 1, -1, 1, -1 };

            return rowOffsets.Select((_, i) => GetMove(currentSquare, rowOffsets[i], colOffsets[i])).ToList();
        }
    }
}