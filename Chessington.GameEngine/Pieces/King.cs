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

        private Square GetMove((int, int) offset)
        {
            var (rowOffset, colOffset) = offset;
            return new Square(currentSquare.Row + rowOffset, currentSquare.Col + colOffset);
        }

        private List<Square> GetMovesFromOffsets(IEnumerable<(int, int)> offsets)
        {
            return offsets.Select(GetMove).ToList();
        }

        private IEnumerable<Square> DiagonalMoves()
        {
            var offsets = new List<(int, int)> { (1, 1), (1, -1), (-1, 1), (-1, -1) };

            return GetMovesFromOffsets(offsets);
        }

        private IEnumerable<Square> StraightMoves()
        {
            var offsets = new List<(int, int)> { (0, 1), (0, -1), (1, 0), (-1, 0) };

            return GetMovesFromOffsets(offsets);
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            this.board = board;
            currentSquare = board.FindPiece(this);

            return StraightMoves().Concat(DiagonalMoves())
                .Where(move => move.IsInBounds() && !FriendlySquare(move));
        }
    }
}