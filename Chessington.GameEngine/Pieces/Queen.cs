using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
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

        private List<Square> PrimaryDiagonalMoves(Square currentSquare)
        {
            var diagonalOffset = currentSquare.Row - currentSquare.Col;

            return Enumerable.Range(0, 8)
                .Select(x => new Square(x + diagonalOffset, x))
                .Where(ValidSquare).ToList();
        }

        private List<Square> SecondaryDiagonalMoves(Square currentSquare)
        {
            var diagonalSum = currentSquare.Row + currentSquare.Col;

            return Enumerable.Range(0, 8)
                .Select(x => new Square(x, diagonalSum - x))
                .Where(ValidSquare).ToList();
        }

        private List<Square> DiagonalMoves(Square currentSquare)
        {
            var diagonalMoves = PrimaryDiagonalMoves(currentSquare);
            diagonalMoves.AddRange(SecondaryDiagonalMoves(currentSquare));

            return diagonalMoves.Where(square => square != currentSquare).ToList();
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

        private List<Square> LateralMoves(Square currentSquare)
        {
            var lateralMoves = HorizontalMoves(currentSquare);
            lateralMoves.AddRange(VerticalMoves(currentSquare));

            return lateralMoves;
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);

            return LateralMoves(currentSquare).Concat(DiagonalMoves(currentSquare));
        }
    }
}