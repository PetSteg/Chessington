﻿using System;
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

        private bool neverMoved(Square currentSquare)
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

            if (currentSquare.Row == initialRow) return true;

            return false;
        }

        private List<Square> MoveStraight(Square currentSquare)
        {
            // black moves down (+), white moves up (-)
            int direction = Player == Player.Black ? 1 : -1;

            var possibleMoves = new List<Square>();

            possibleMoves.Add(new Square(currentSquare.Row + 1 * direction, currentSquare.Col));
            if (neverMoved(currentSquare))
            {
                possibleMoves.Add(new Square(currentSquare.Row + 2 * direction, currentSquare.Col));
            }

            return possibleMoves;
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);

            return MoveStraight(currentSquare);
        }
    }
}