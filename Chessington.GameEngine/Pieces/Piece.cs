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

        private Square GetNextSquare(int distance, Direction rowDirection, Direction colDirection)
        {
            var nextRow = currentSquare.Row + distance * (int)rowDirection;
            var nextCol = currentSquare.Col + distance * (int)colDirection;
            return Square.At(nextRow, nextCol);
        }

        private IEnumerable<Square> MovesOnDirection(Direction rowDirection, Direction colDirection)
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
            var movesDownLeft = MovesOnDirection(Direction.Down, Direction.Left);
            var movesUpRight = MovesOnDirection(Direction.Up, Direction.Right);

            return movesDownLeft.Concat(movesUpRight);
        }

        private IEnumerable<Square> SecondaryDiagonalMoves()
        {
            var movesDownRight = MovesOnDirection(Direction.Down, Direction.Right);
            var movesUpLeft = MovesOnDirection(Direction.Up, Direction.Left);

            return movesDownRight.Concat(movesUpLeft);
        }

        private IEnumerable<Square> HorizontalMoves()
        {
            var horizontalMovesLeft = MovesOnDirection(Direction.Stay, Direction.Left);
            var horizontalMovesRight = MovesOnDirection(Direction.Stay, Direction.Right);
            return horizontalMovesLeft.Concat(horizontalMovesRight);
        }

        private IEnumerable<Square> VerticalMoves()
        {
            var verticalMovesUp = MovesOnDirection(Direction.Up, Direction.Stay);
            var verticalMovesDown = MovesOnDirection(Direction.Down, Direction.Stay);
            return verticalMovesUp.Concat(verticalMovesDown);
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