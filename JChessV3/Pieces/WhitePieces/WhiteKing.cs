using System;
using System.Collections.Generic;
using System.Text;

namespace JChessV3.Pieces.WhitePieces
{
    class WhiteKing
    {
        public WhiteKing()
        {

        }

        public int[,] generateMoves(int[,] inputArr, int row, int column)
        {
            int[,] possibleKingMoves = new int[8, 8];

            if (row - 1 >= 0 && column - 1 >= 0)
            {
                if (inputArr[row - 1, column - 1] == 0)
                {
                    possibleKingMoves[row - 1, column - 1] = 1;
                }
                else if (inputArr[row - 1, column - 1] < 0)
                {
                    possibleKingMoves[row - 1, column - 1] = -1;
                }
            }

            if (row - 1 >= 0)
            {
                if (inputArr[row - 1, column] == 0)
                {
                    possibleKingMoves[row - 1, column] = 1;
                }
                else if (inputArr[row - 1, column] < 0)
                {
                    possibleKingMoves[row - 1, column] = -1;
                }
            }

            if (row - 1 >= 0 && column + 1 < 8)
            {
                if (inputArr[row - 1, column + 1] == 0)
                {
                    possibleKingMoves[row - 1, column + 1] = 1;
                }
                else if (inputArr[row - 1, column + 1] < 0)
                {
                    possibleKingMoves[row - 1, column + 1] = -1;
                }
            }

            if (column - 1 >= 0)
            {
                if (inputArr[row, column - 1] == 0)
                {
                    possibleKingMoves[row, column - 1] = 1;
                }
                else if (inputArr[row, column - 1] < 0)
                {
                    possibleKingMoves[row, column - 1] = -1;
                }
            }

            if (column + 1 < 8)
            {
                if (inputArr[row, column + 1] == 0)
                {
                    possibleKingMoves[row, column + 1] = 1;
                }
                else if (inputArr[row, column + 1] < 0)
                {
                    possibleKingMoves[row, column + 1] = -1;
                }
            }

            if (row + 1 < 8 && column - 1 >= 0)
            {
                if (inputArr[row + 1, column - 1] == 0)
                {
                    possibleKingMoves[row + 1, column - 1] = 1;
                }
                else if (inputArr[row + 1, column - 1] < 0)
                {
                    possibleKingMoves[row + 1, column - 1] = -1;
                }
            }

            if (row + 1 < 8)
            {
                if (inputArr[row + 1, column] == 0)
                {
                    possibleKingMoves[row + 1, column] = 1;
                }
                else if (inputArr[row + 1, column] < 0)
                {
                    possibleKingMoves[row + 1, column] = -1;
                }
            }

            if (row + 1 < 8 && column + 1 < 8)
            {
                if (inputArr[row + 1, column + 1] == 0)
                {
                    possibleKingMoves[row + 1, column + 1] = 1;
                }
                else if (inputArr[row + 1, column + 1] < 0)
                {
                    possibleKingMoves[row + 1, column + 1] = -1;
                }
            }

            return possibleKingMoves;
        }
    }
}
