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

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            Square nextSquare;
            if (Player == Player.White)
            {
                nextSquare = new Square(currentSquare.Row - 1, currentSquare.Col);
            }
            else
            {
                nextSquare = new Square(currentSquare.Row + 1, currentSquare.Col);
            }

            return new List<Square> { nextSquare };
        }
    }
}