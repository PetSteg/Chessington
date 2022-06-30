using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public abstract class Piece
    {
        protected Square currentSquare;
        protected Board board;

        public Player Player { get; private set; }

        protected Piece(Player player)
        {
            Player = player;
        }

        protected bool OpponentSquare(Square square)
        {
            var piece = board?.GetPiece(square);
            if (piece == null)
            {
                return false;
            }

            return board.GetPiece(square).Player != Player;
        }

        protected bool FriendlySquare(Square square)
        {
            var piece = board?.GetPiece(square);
            if (piece == null)
            {
                return false;
            }

            return board.GetPiece(square).Player == Player;
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

        protected IEnumerable<Square> LateralMoves()
        {
            return HorizontalMoves().Concat(VerticalMoves());
        }

        protected IEnumerable<Square> DiagonalMoves()
        {
            return PrimaryDiagonalMoves().Concat(SecondaryDiagonalMoves());
        }

        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

        public void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
        }
    }
}