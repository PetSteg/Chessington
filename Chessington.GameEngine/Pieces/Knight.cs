using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player)
        {
        }

        private Square GetMove((int, int) offset)
        {
            var (rowOffset, colOffset) = offset;
            return new Square(currentSquare.Row + rowOffset, currentSquare.Col + colOffset);
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            this.board = board;
            currentSquare = board.FindPiece(this);
            var offsets = new List<(int, int)>
                { (1, 2), (1, -2), (-1, 2), (-1, -2), (2, 1), (2, -1), (-2, 1), (-2, -1) };

            return offsets.Select(GetMove)
                .Where(move => move.IsInBounds() && !FriendlySquare(move));
        }
    }
}