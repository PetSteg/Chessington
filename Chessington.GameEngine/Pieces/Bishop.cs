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

        private List<Square> PrimaryDiagonalMovesDirection(Square currentSquare, Board board, int direction)
        {
            var moves = new List<Square>();

            for (var distance = 1; distance < 7; distance++)
            {
                var offset = distance * direction;
                var nextSquare = new Square(currentSquare.Row + offset, currentSquare.Col + offset);
                if (!ValidSquare(nextSquare)) break;
                if (board.GetPiece(nextSquare) != null)
                {
                    if (board.GetPiece(nextSquare).Player != Player)
                        moves.Add(nextSquare);

                    break;
                }

                moves.Add(nextSquare);
            }

            return moves;
        }

        private List<Square> PrimaryDiagonalMoves(Square currentSquare, Board board)
        {
            var movesDownLeft = PrimaryDiagonalMovesDirection(currentSquare, board, -1);
            var movesUpRight = PrimaryDiagonalMovesDirection(currentSquare, board, 1);

            return movesDownLeft.Concat(movesUpRight).ToList();
        }

        private List<Square> SecondaryDiagonalMovesDirection(Square currentSquare, Board board, int direction)
        {
            var moves = new List<Square>();

            for (var distance = 1; distance < 7; distance++)
            {
                var offset = distance * direction;
                var nextSquare = new Square(currentSquare.Row + offset, currentSquare.Col - offset);
                if (!ValidSquare(nextSquare)) break;
                if (board.GetPiece(nextSquare) != null)
                {
                    if (board.GetPiece(nextSquare).Player != Player)
                        moves.Add(nextSquare);

                    break;
                }

                moves.Add(nextSquare);
            }

            return moves;
        }

        private List<Square> SecondaryDiagonalMoves(Square currentSquare, Board board)
        {
            var movesDownRight = SecondaryDiagonalMovesDirection(currentSquare, board, -1);
            var movesUpLeft = SecondaryDiagonalMovesDirection(currentSquare, board, 1);

            return movesDownRight.Concat(movesUpLeft).ToList();
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var primaryDiagonalMoves = PrimaryDiagonalMoves(currentSquare, board);
            var secondaryDiagonalMoves = SecondaryDiagonalMoves(currentSquare, board);

            return primaryDiagonalMoves.Concat(secondaryDiagonalMoves);
        }
    }
}