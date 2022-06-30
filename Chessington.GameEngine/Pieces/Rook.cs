using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player)
            : base(player)
        {
        }

        private List<Square> HorizontalMoves(Square currentSquare)
        {
            return Enumerable.Range(0, 8).Where(x => x != currentSquare.Col)
                .Select(col => new Square(currentSquare.Row, col)).ToList();
        }

        private List<Square> VerticalMoves(Square currentSquare)
        {
            return Enumerable.Range(0, 8).Where(x => x != currentSquare.Row)
                .Select(row => new Square(row, currentSquare.Col)).ToList();
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);

            var possibleMoves = HorizontalMoves(currentSquare);
            possibleMoves.AddRange(VerticalMoves(currentSquare));

            return possibleMoves;
        }
    }
}