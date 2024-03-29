﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JChessV3.Pieces.BlackPieces
{
    class BlackKnight
    {
        public BlackKnight()
        {

        }

        /// <summary>
        /// Generates the moves for a Black Knight. Does not account for pins.
        /// </summary>
        /// <param name="inputArr"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int[,] GenerateMoves(int[,] inputArr, int row, int column)
        {
            int[,] possibleKnightMoves = new int[8, 8];
            int[] rowOrder = { -1, -2, -2, -1 };
            int[] columnOrder = { -2, -1, 1, 2 };

            for (int i = 0; i < 4; i++)
            {
                int rowTemp = row + rowOrder[i];
                int colTemp = column + columnOrder[i];
                if (rowTemp >= 0 && colTemp >= 0 && colTemp < 8)
                {
                    if (inputArr[rowTemp, colTemp] == 0)
                    {
                        possibleKnightMoves[rowTemp, colTemp] = 1;
                    }
                    else if (inputArr[rowTemp, colTemp] > 0)
                    {
                        possibleKnightMoves[rowTemp, colTemp] = -1;
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                int rowTemp = row - rowOrder[i];
                int colTemp = column - columnOrder[i];
                if (rowTemp < 8 && colTemp >= 0 && colTemp < 8)
                {
                    if (inputArr[rowTemp, colTemp] == 0)
                    {
                        possibleKnightMoves[rowTemp, colTemp] = 1;
                    }
                    else if (inputArr[rowTemp, colTemp] > 0)
                    {
                        possibleKnightMoves[rowTemp, colTemp] = -1;
                    }
                }
            }

            return possibleKnightMoves;
        }

        /// <summary>
        /// Generates the threats for a black knight.
        /// </summary>
        /// <param name="inputArr"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int[,] GenerateThreats(int[,] inputArr, int row, int column)
        {
            int[,] possibleKnightThreats = new int[8, 8];
            int[] rowOrder = { -1, -2, -2, -1 };
            int[] columnOrder = { -2, -1, 1, 2 };

            for (int i = 0; i < 4; i++)
            {
                int rowTemp = row + rowOrder[i];
                int colTemp = column + columnOrder[i];
                if (rowTemp >= 0 && colTemp >= 0 && colTemp < 8)
                {
                    possibleKnightThreats[rowTemp, colTemp] = 1;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                int rowTemp = row - rowOrder[i];
                int colTemp = column - columnOrder[i];
                if (rowTemp < 8 && colTemp >= 0 && colTemp < 8)
                {
                    possibleKnightThreats[rowTemp, colTemp] = 1;
                }
            }

            return possibleKnightThreats;
        }
    }
}
