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

        private bool ValidSquare(Square square)
        {
            var row = square.Row;
            if (row < 0 || row > 7) return false;

            var col = square.Col;
            if (col < 0 || col > 7) return false;

            return true;
        }

        private bool OpponentSquare(Square square, Board board)
        {
            var piece = board.GetPiece(square);
            if (piece == null) return false;

            return board.GetPiece(square).Player != Player;
        }

        private bool NeverMoved(Square currentSquare)
        {
            int initialRow;
            if (Player == Player.White)
            {
                initialRow = 7;
            }
            else
            {
                initialRow = 1;
            }

            return currentSquare.Row == initialRow;
        }

        private List<Square> StraightMoves(Square currentSquare, Board board)
        {
            // black moves down (+), white moves up (-)
            var direction = Player == Player.Black ? 1 : -1;

            var possibleMoves = new List<Square>();

            var nextSquare = new Square(currentSquare.Row + 1 * direction, currentSquare.Col);
            if (!ValidSquare(nextSquare) || board.GetPiece(nextSquare) != null) return possibleMoves;

            possibleMoves.Add(nextSquare);
            if (NeverMoved(currentSquare))
            {
                possibleMoves.Add(new Square(currentSquare.Row + 2 * direction, currentSquare.Col));
            }

            return possibleMoves;
        }

        private List<Square> DiagonalMoves(Square currentSquare, Board board)
        {
            // black moves down (+), white moves up (-)
            var direction = Player == Player.Black ? 1 : -1;

            var possibleMoves = new List<Square>();
            var nextSquareLeft = new Square(currentSquare.Row + 1 * direction, currentSquare.Col - 1);
            var nextSquareRight = new Square(currentSquare.Row + 1 * direction, currentSquare.Col + 1);

            if (ValidSquare(nextSquareLeft) && OpponentSquare(nextSquareLeft, board))
            {
                possibleMoves.Add(nextSquareLeft);
            }

            if (ValidSquare(nextSquareRight) && OpponentSquare(nextSquareRight, board))
            {
                possibleMoves.Add(nextSquareRight);
            }

            return possibleMoves;
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var straightMoves = StraightMoves(currentSquare, board);
            var diagonalMoves = DiagonalMoves(currentSquare, board);

            return straightMoves.Where(move => board.GetPiece(move) == null).Concat(diagonalMoves);
        }
    }
}