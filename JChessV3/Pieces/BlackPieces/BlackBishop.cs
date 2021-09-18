using System;
using System.Collections.Generic;
using System.Text;

namespace JChessV3.Pieces.BlackPieces
{
    class BlackBishop
    {
        public BlackBishop()
        {

        }

        /// <summary>
        /// Generates the moves for a Black Bishop. Does not account for pins.
        /// </summary>
        /// <param name="inputArr"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int[,] GenerateMoves(int[,] inputArr, int row, int column)
        {
            int[,] possibleBishopMoves = new int[8, 8];
            
            bool stopped = false;
            for(int i = 1; !stopped; i++)
            {
                int col_i = column - i;
                int row_i = row - i;
                if (col_i >= 0 && row_i >= 0)
                {
                    if(inputArr[row_i, col_i] == 0)
                    {
                        possibleBishopMoves[row_i, col_i] = 1;
                    }
                    else if (inputArr[row_i, col_i] > 0)
                    {
                        possibleBishopMoves[row_i, col_i] = -1;
                        stopped = true;
                    }
                    else if (inputArr[row_i, col_i] < 0)
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
            for(int i = 1; !stopped; i++)
            {
                int col_i = column + i;
                int row_i = row - i;
                if (col_i < 8 && row_i >= 0)
                {
                    if (inputArr[row_i, col_i] == 0)
                    {
                        possibleBishopMoves[row_i, col_i] = 1;
                    }
                    else if (inputArr[row_i, col_i] > 0)
                    {
                        possibleBishopMoves[row_i, col_i] = -1;
                        stopped = true;
                    }
                    else if (inputArr[row_i, col_i] < 0)
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
                int row_i = row + i;
                if (col_i >= 0 && row_i < 8)
                {
                    if (inputArr[row_i, col_i] == 0)
                    {
                        possibleBishopMoves[row_i, col_i] = 1;
                    }
                    else if (inputArr[row_i, col_i] > 0)
                    {
                        possibleBishopMoves[row_i, col_i] = -1;
                        stopped = true;
                    }
                    else if (inputArr[row_i, col_i] < 0)
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
                int row_i = row + i;
                if (col_i < 8 && row_i < 8)
                {
                    if (inputArr[row_i, col_i] == 0)
                    {
                        possibleBishopMoves[row_i, col_i] = 1;
                    }
                    else if (inputArr[row_i, col_i] > 0)
                    {
                        possibleBishopMoves[row_i, col_i] = -1;
                        stopped = true;
                    }
                    else if (inputArr[row_i, col_i] < 0)
                    {
                        stopped = true;
                    }
                }
                else
                {
                    stopped = true;
                }
            }

            return possibleBishopMoves;
        }
    }
}
