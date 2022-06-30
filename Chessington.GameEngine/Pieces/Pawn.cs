using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player)
            : base(player)
        {
        }

        private bool NeverMoved()
        {
            var initialRow = Player == Player.White ? 7 : 1;

            return currentSquare.Row == initialRow;
        }

        private IEnumerable<Square> StraightMoves()
        {
            // black moves down (+), white moves up (-)
            var direction = Player == Player.Black ? 1 : -1;

            var possibleMoves = new List<Square>();

            var nextSquare = Square.At(currentSquare.Row + 1 * direction, currentSquare.Col);
            if (!nextSquare.IsInBounds() || board.GetPiece(nextSquare) != null) return possibleMoves;

            possibleMoves.Add(nextSquare);
            if (NeverMoved())
            {
                possibleMoves.Add(Square.At(currentSquare.Row + 2 * direction, currentSquare.Col));
            }

            return possibleMoves;
        }

        private IEnumerable<Square> DiagonalMoves()
        {
            // black moves down (+), white moves up (-)
            var direction = Player == Player.Black ? 1 : -1;

            var possibleMoves = new List<Square>();
            var nextSquareLeft = Square.At(currentSquare.Row + 1 * direction, currentSquare.Col - 1);
            var nextSquareRight = Square.At(currentSquare.Row + 1 * direction, currentSquare.Col + 1);

            if (nextSquareLeft.IsInBounds() && OpponentSquare(nextSquareLeft))
                possibleMoves.Add(nextSquareLeft);

            if (nextSquareRight.IsInBounds() && OpponentSquare(nextSquareRight))
                possibleMoves.Add(nextSquareRight);

            return possibleMoves;
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            this.board = board;
            currentSquare = board.FindPiece(this);

            return StraightMoves().Where(move => board.GetPiece(move) == null).Concat(DiagonalMoves());
        }
    }
}