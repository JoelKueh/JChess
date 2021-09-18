﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JChessV3.Pieces.BlackPieces
{
    class BlackPawn : Piece
    {
        public BlackPawn()
        {

        }

        public int[,] generateMoves(int[,] inputBoard, int row, int column)
        {
            int[,] possiblePawnMoves = new int[8,8];

            if (row + 1 < 8)
            {
                if (inputBoard[row + 1, column] == 0)
                {
                    possiblePawnMoves[row + 1, column] = 1;
                }

                if (column - 1 >= 0 && inputBoard[row + 1, column - 1] > 0)
                {
                    possiblePawnMoves[row + 1, column - 1] = -1;
                }

                if (column + 1 < 8 && inputBoard[row - 1, column + 1] > 0)
                {
                    possiblePawnMoves[row + 1, column + 1] = -1;
                }
            }
            
            if (row == 1)
            {
                if (inputBoard[row + 1, column] == 0)
                {
                    possiblePawnMoves[row + 1, column] = 1;

                    if (inputBoard[row + 2, column] == 0)
                    {
                        possiblePawnMoves[row + 2, column] = 1;
                    }
                }
            }

            return possiblePawnMoves;
        }
    }
}