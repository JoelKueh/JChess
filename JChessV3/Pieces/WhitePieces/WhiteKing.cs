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

        /// <summary>
        /// Generates the moves for a White King. Does not account for danger squares.
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
                else if (inputArr[row - 1, column - 1] < 0)
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
                else if (inputArr[row - 1, column] < 0)
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
                else if (inputArr[row - 1, column + 1] < 0)
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
                else if (inputArr[row, column - 1] < 0)
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
                else if (inputArr[row, column + 1] < 0)
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
                else if (inputArr[row + 1, column - 1] < 0)
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
                else if (inputArr[row + 1, column] < 0)
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
                else if (inputArr[row + 1, column + 1] < 0)
                {
                    possibleKingMoves[row + 1, column + 1] = -1;
                }
            }

            return possibleKingMoves;
        }

        /// <summary>
        /// Generates the PreCastle moves for a white king.
        /// </summary>
        /// <param name="inputArr"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int[,] GeneratePreCastledMoves(int[,] inputArr, int[,] dangerSquares, int row, int column)
        {
            int[,] preCastledMoves = GenerateMoves(inputArr, dangerSquares, row, column);

            if (dangerSquares[row, column] == 0)
            {
                if (inputArr[7, 0] == 41)
                {
                    if (inputArr[7, 1] == 0 && inputArr[7, 2] == 0 && inputArr[7, 3] == 0 && dangerSquares[7, 1] == 0 && dangerSquares[7, 2] == 0 && dangerSquares[7, 3] == 0)
                    {
                        preCastledMoves[7, 2] = 2;
                    }
                }

                if (inputArr[7, 7] == 41)
                {
                    if (inputArr[7, 6] == 0 && inputArr[7, 5] == 0 && dangerSquares[7, 6] == 0 && dangerSquares[7, 5] == 0)
                    {
                        preCastledMoves[7, 6] = 2;
                    }
                }
            }

            return preCastledMoves;
        }

        /// <summary>
        /// Generates the threats for a white king
        /// </summary>
        /// <param name="inputArr"></param>
        /// <param name="dangerSquares"></param>
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
