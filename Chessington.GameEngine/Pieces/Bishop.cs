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

        private Square GetNextSquare(int distance, int rowDirection, int colDirection)
        {
            var nextRow = currentSquare.Row + distance * rowDirection;
            var nextCol = currentSquare.Col + distance * colDirection;
            return Square.At(nextRow, nextCol);
        }

        private IEnumerable<Square> MovesOnDirection(int rowDirection, int colDirection)
        {
            var moves = new List<Square>();

            for (var distance = 1; distance < 7; distance++)
            {
                var nextSquare = GetNextSquare(distance, rowDirection, colDirection);
                if (!nextSquare.IsInBounds() || FriendlySquare(nextSquare))
                {
                    break;
                }

                if (OpponentSquare(nextSquare))
                {
                    moves.Add(nextSquare);
                    break;
                }

                moves.Add(nextSquare);
            }

            return moves;
        }

        private IEnumerable<Square> PrimaryDiagonalMoves()
        {
            var movesDownLeft = MovesOnDirection(1, -1);
            var movesUpRight = MovesOnDirection(-1, 1);

            return movesDownLeft.Concat(movesUpRight);
        }

        private IEnumerable<Square> SecondaryDiagonalMoves()
        {
            var movesDownRight = MovesOnDirection(1, 1);
            var movesUpLeft = MovesOnDirection(-1, -1);

            return movesDownRight.Concat(movesUpLeft);
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            this.board = board;
            currentSquare = board.FindPiece(this);

            return PrimaryDiagonalMoves().Concat(SecondaryDiagonalMoves());
        }
    }
}