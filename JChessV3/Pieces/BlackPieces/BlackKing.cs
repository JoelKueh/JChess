using System;
using System.Collections.Generic;
using System.Text;

namespace JChessV3.Pieces.BlackPieces
{
    class BlackKing
    {
        public BlackKing()
        {

        }

        /// <summary>
        /// Generates the moves for a Black King. Does not account for danger squares.
        /// </summary>
        /// <param name="inputArr"></param>
        /// <param name="dangerSquares"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int[,] GenerateMoves(int[,] inputArr, int[,] dangerSquares, int row, int column)
        {
            int[,] possibleKingMoves = new int[8, 8];

            if (row - 1 >= 0 && column - 1 >= 0 && dangerSquares[row - 1, column - 1] == 0)
            {
                if (inputArr[row - 1, column - 1] == 0)
                {
                    possibleKingMoves[row - 1, column - 1] = 1;
                }
                else if (inputArr[row - 1, column - 1] > 0)
                {
                    possibleKingMoves[row - 1, column - 1] = -1;
                }
            }

            if (row - 1 >= 0 && dangerSquares[row - 1, column] == 0)
            {
                if (inputArr[row - 1, column] == 0)
                {
                    possibleKingMoves[row - 1, column] = 1;
                }
                else if (inputArr[row - 1, column] > 0)
                {
                    possibleKingMoves[row - 1, column] = -1;
                }
            }

            if (row - 1 >= 0 && column + 1 < 8 && dangerSquares[row - 1, column + 1] == 0)
            {
                if (inputArr[row - 1, column + 1] == 0)
                {
                    possibleKingMoves[row - 1, column + 1] = 1;
                }
                else if (inputArr[row - 1, column + 1] > 0)
                {
                    possibleKingMoves[row - 1, column + 1] = -1;
                }
            }

            if (column - 1 >= 0 && dangerSquares[row, column - 1] == 0)
            {
                if (inputArr[row, column - 1] == 0)
                {
                    possibleKingMoves[row, column - 1] = 1;
                }
                else if (inputArr[row, column - 1] > 0)
                {
                    possibleKingMoves[row, column - 1] = -1;
                }
            }

            if (column + 1 < 8 && dangerSquares[row, column + 1] == 0)
            {
                if (inputArr[row, column + 1] == 0)
                {
                    possibleKingMoves[row, column + 1] = 1;
                }
                else if (inputArr[row, column + 1] > 0)
                {
                    possibleKingMoves[row, column + 1] = -1;
                }
            }

            if (row + 1 < 8 && column - 1 >= 0 && dangerSquares[row + 1, column - 1] == 0)
            {
                if (inputArr[row + 1, column - 1] == 0)
                {
                    possibleKingMoves[row + 1, column - 1] = 1;
                }
                else if (inputArr[row + 1, column - 1] > 0)
                {
                    possibleKingMoves[row + 1, column - 1] = -1;
                }
            }

            if (row + 1 < 8 && dangerSquares[row + 1, column] == 0)
            {
                if (inputArr[row + 1, column] == 0)
                {
                    possibleKingMoves[row + 1, column] = 1;
                }
                else if (inputArr[row + 1, column] > 0)
                {
                    possibleKingMoves[row + 1, column] = -1;
                }
            }

            if (row + 1 < 8 && column + 1 < 8 && dangerSquares[row + 1, column + 1] == 0)
            {
                if (inputArr[row + 1, column + 1] == 0)
                {
                    possibleKingMoves[row + 1, column + 1] = 1;
                }
                else if (inputArr[row + 1, column + 1] > 0)
                {
                    possibleKingMoves[row + 1, column + 1] = -1;
                }
            }

            return possibleKingMoves;
        }

        /// <summary>
        /// Generates the PreCastle moves for a black king.
        /// </summary>
        /// <param name="inputArr"></param>
        /// <param name="dangerSquares"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int[,] GeneratePreCastledMoves(int[,] inputArr, int[,] dangerSquares, int row, int column)
        {
            int[,] preCastledMoves = GenerateMoves(inputArr, dangerSquares, row, column);

            if (dangerSquares[row, column] == 0)
            {
                if (inputArr[0, 0] == -41)
                {
                    if (inputArr[0, 1] == 0 && inputArr[0, 2] == 0 && inputArr[0, 3] == 0 && dangerSquares[0, 1] == 0 && dangerSquares[0, 2] == 0 && dangerSquares[0, 3] == 0)
                    {
                        preCastledMoves[0, 2] = 3;
                    }
                }

                if (inputArr[0, 7] == -41)
                {
                    if (inputArr[0, 6] == 0 && inputArr[0, 5] == 0 && dangerSquares[0, 6] == 0 && dangerSquares[0, 5] == 0)
                    {
                        preCastledMoves[0, 6] = 4;
                    }
                }
            }

            return preCastledMoves;
        }

        /// <summary>
        /// Generates the threats for the King.
        /// </summary>
        /// <param name="inputArr"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int[,] GenerateThreats(int[,] inputArr, int row, int column)
        {
            int[,] possibleKingMoves = new int[8, 8];

            if (row - 1 >= 0 && column - 1 >= 0)
            {
                possibleKingMoves[row - 1, column - 1] = 1;
            }

            if (row - 1 >= 0)
            {
                possibleKingMoves[row - 1, column] = 1;
            }

            if (row - 1 >= 0 && column + 1 < 8)
            {
                possibleKingMoves[row - 1, column + 1] = 1;
            }

            if (column - 1 >= 0)
            {
                possibleKingMoves[row, column - 1] = 1;
            }

            if (column + 1 < 8)
            {
                possibleKingMoves[row, column + 1] = 1;
            }

            if (row + 1 < 8 && column - 1 >= 0)
            {
                possibleKingMoves[row + 1, column - 1] = 1;
            }

            if (row + 1 < 8)
            {
                possibleKingMoves[row + 1, column] = 1;
            }

            if (row + 1 < 8 && column + 1 < 8)
            {
                possibleKingMoves[row + 1, column + 1] = 1;
            }

            return possibleKingMoves;
        }
    }
}
