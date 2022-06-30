using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player)
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

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);

            var possibleMoves = PrimaryDiagonalMoves(currentSquare);
            possibleMoves.AddRange(SecondaryDiagonalMoves(currentSquare));

            return possibleMoves.Where(square => square != currentSquare);
        }
    }
}