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

        private IEnumerable<Square> HorizontalMoves()
        {
            var horizontalMovesLeft = MovesOnDirection(0, -1);
            var horizontalMovesRight = MovesOnDirection(0, 1);
            return horizontalMovesLeft.Concat(horizontalMovesRight);
        }

        private IEnumerable<Square> VerticalMoves()
        {
            var verticalMovesLeft = MovesOnDirection(-1, 0);
            var verticalMovesRight = MovesOnDirection(1, 0);
            return verticalMovesLeft.Concat(verticalMovesRight);
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            this.board = board;
            currentSquare = board.FindPiece(this);

            return HorizontalMoves().Concat(VerticalMoves());
        }
    }
}