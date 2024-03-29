﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JChessV3.Pieces.BlackPieces
{
    class BlackRook
    {
        public BlackRook()
        {

        }

        /// <summary>
        /// Generates the moves for a Black Rook. Does not account for pins.
        /// </summary>
        /// <param name="inputArr"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int[,] GenerateMoves(int[,] inputArr, int row, int column)
        {
            int[,] possibleRookMoves = new int[8, 8];

            bool stopped = false;
            for(int i = 1; !stopped; i++)
            {
                int row_i = row - i;
                if(row_i >= 0)
                {
                    if(inputArr[row_i, column] == 0)
                    {
                        possibleRookMoves[row_i, column] = 1;
                    }
                    else if(inputArr[row_i, column] > 0)
                    {
                        possibleRookMoves[row_i, column] = -1;
                        stopped = true;
                    }
                    else if(inputArr[row_i, column] < 0)
                    {
                        stopped = true;
                    }
                }
                else
                {
                    stopped = true;
                }    
            }

            stopped = false;
            for (int i = 1; !stopped; i++)
            {
                int row_i = row + i;
                if (row_i < 8)
                {
                    if (inputArr[row_i, column] == 0)
                    {
                        possibleRookMoves[row_i, column] = 1;
                    }
                    else if (inputArr[row_i, column] > 0)
                    {
                        possibleRookMoves[row_i, column] = -1;
                        stopped = true;
                    }
                    else if (inputArr[row_i, column] < 0)
                    {
                        stopped = true;
                    }
                }
                else
                {
                    stopped = true;
                }
            }

            stopped = false;
            for (int i = 1; !stopped; i++)
            {
                int col_i = column - i;
                if (col_i >= 0)
                {
                    if (inputArr[row, col_i] == 0)
                    {
                        possibleRookMoves[row, col_i] = 1;
                    }
                    else if (inputArr[row, col_i] > 0)
                    {
                        possibleRookMoves[row, col_i] = -1;
                        stopped = true;
                    }
                    else if (inputArr[row, col_i] < 0)
                    {
                        stopped = true;
                    }
                }
                else
                {
                    stopped = true;
                }
            }

            stopped = false;
            for (int i = 1; !stopped; i++)
            {
                int col_i = column + i;
                if (col_i < 8)
                {
                    if (inputArr[row, col_i] == 0)
                    {
                        possibleRookMoves[row, col_i] = 1;
                    }
                    else if (inputArr[row, col_i] > 0)
                    {
                        possibleRookMoves[row, col_i] = -1;
                        stopped = true;
                    }
                    else if (inputArr[row, col_i] < 0)
                    {
                        stopped = true;
                    }
                }
                else
                {
                    stopped = true;
                }
            }

            return possibleRookMoves;
        }

        /// <summary>
        /// Generates the threats for a black rook.
        /// </summary>
        /// <param name="inputArr"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int[,] GenerateThreats(int[,] inputArr, int row, int column)
        {
            int[,] possibleRookThreats = new int[8, 8];

            bool stopped = false;
            for (int i = 1; !stopped; i++)
            {
                int row_i = row - i;
                if (row_i >= 0)
                {
                    if (inputArr[row_i, column] == 0 || inputArr[row_i, column] == Const.WHITE_KING || inputArr[row_i, column] == Const.C_WHITE_KING)
                    {
                        possibleRookThreats[row_i, column] = 1;
                    }
                    else
                    {
                        possibleRookThreats[row_i, column] = 1;
                        stopped = true;
                    }
                }
                else
                {
                    stopped = true;
                }
            }

            stopped = false;
            for (int i = 1; !stopped; i++)
            {
                int row_i = row + i;
                if (row_i < 8)
                {
                    if (inputArr[row_i, column] == 0 || inputArr[row_i, column] == Const.WHITE_KING || inputArr[row_i, column] == Const.C_WHITE_KING)
                    {
                        possibleRookThreats[row_i, column] = 1;
                    }
                    else
                    {
                        possibleRookThreats[row_i, column] = 1;
                        stopped = true;
                    }
                }
                else
                {
                    stopped = true;
                }
            }

            stopped = false;
            for (int i = 1; !stopped; i++)
            {
                int col_i = column - i;
                if (col_i >= 0)
                {
                    if (inputArr[row, col_i] == 0 || inputArr[row, col_i] == Const.WHITE_KING || inputArr[row, col_i] == Const.C_WHITE_KING)
                    {
                        possibleRookThreats[row, col_i] = 1;
                    }
                    else
                    {
                        possibleRookThreats[row, col_i] = 1;
                        stopped = true;
                    }
                }
                else
                {
                    stopped = true;
                }
            }

            stopped = false;
            for (int i = 1; !stopped; i++)
            {
                int col_i = column + i;
                if (col_i < 8)
                {
                    if (inputArr[row, col_i] == 0 || inputArr[row, col_i] == Const.WHITE_KING || inputArr[row, col_i] == Const.C_WHITE_KING)
                    {
                        possibleRookThreats[row, col_i] = 1;
                    }
                    else
                    {
                        possibleRookThreats[row, col_i] = 1;
                        stopped = true;
                    }
                }
                else
                {
                    stopped = true;
                }
            }

            return possibleRookThreats;
        }
    }
}
