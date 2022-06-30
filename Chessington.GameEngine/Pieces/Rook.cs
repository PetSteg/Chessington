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

        private bool ValidSquare(Square square)
        {
            var row = square.Row;
            if (row < 0 || row > 7) return false;

            var col = square.Col;
            if (col < 0 || col > 7) return false;

            return true;
        }

        private List<Square> HorizontalMovesDirection(Square currentSquare, Board board, int direction)
        {
            var horizontalMoves = new List<Square>();

            for (var distance = 1; distance < 8; distance++)
            {
                var nextSquare = new Square(currentSquare.Row, currentSquare.Col + distance * direction);
                if (!ValidSquare(nextSquare)) break;
                if (board.GetPiece(nextSquare) != null)
                {
                    if (board.GetPiece(nextSquare).Player != Player)
                        horizontalMoves.Add(nextSquare);

                    break;
                }

                horizontalMoves.Add(nextSquare);
            }

            return horizontalMoves;
        }

        private List<Square> HorizontalMoves(Square currentSquare, Board board)
        {
            var horizontalMovesLeft = HorizontalMovesDirection(currentSquare, board, -1);
            var horizontalMovesRight = HorizontalMovesDirection(currentSquare, board, 1);
            return horizontalMovesLeft.Concat(horizontalMovesRight).ToList();
        }

        private List<Square> VerticalMovesDirection(Square currentSquare, Board board, int direction)
        {
            var horizontalMoves = new List<Square>();

            for (var distance = 1; distance < 8; distance++)
            {
                var nextSquare = new Square(currentSquare.Row + distance * direction, currentSquare.Col);
                if (!ValidSquare(nextSquare)) break;
                if (board.GetPiece(nextSquare) != null)
                {
                    if (board.GetPiece(nextSquare).Player != Player)
                        horizontalMoves.Add(nextSquare);

                    break;
                }

                horizontalMoves.Add(nextSquare);
            }

            return horizontalMoves;
        }

        private List<Square> VerticalMoves(Square currentSquare, Board board)
        {
            var verticalMovesLeft = VerticalMovesDirection(currentSquare, board, -1);
            var verticalMovesRight = VerticalMovesDirection(currentSquare, board, 1);
            return verticalMovesLeft.Concat(verticalMovesRight).ToList();
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);

            var horizontalMoves = HorizontalMoves(currentSquare, board);
            var verticalMoves = VerticalMoves(currentSquare, board);

            return horizontalMoves.Concat(verticalMoves);
        }
    }
}