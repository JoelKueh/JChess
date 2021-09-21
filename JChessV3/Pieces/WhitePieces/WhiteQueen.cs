using System;
using System.Collections.Generic;
using System.Text;

namespace JChessV3.Pieces.WhitePieces
{
    class WhiteQueen
    {
        WhiteBishop queenWhiteBishop;
        WhiteRook queenWhiteRook;

        public WhiteQueen()
        {
            queenWhiteBishop = new WhiteBishop();
            queenWhiteRook = new WhiteRook();
        }

        /// <summary>
        /// Generates the moves for a White Queen. Does not account for pins.
        /// </summary>
        /// <param name="inputArr"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int[,] GenerateMoves(int[,] inputArr, int row, int column)
        {
            int[,] possibleQueenMoves = new int[8, 8];
            int[,] queenRookMoves = queenWhiteRook.GenerateMoves(inputArr, row, column);
            int[,] queenBishopMoves = queenWhiteBishop.GenerateMoves(inputArr, row, column);

            for(int row_x = 0; row_x < 8; row_x++)
            {
                for(int column_x = 0; column_x < 8; column_x++)
                {
                    if(queenRookMoves[row_x, column_x] != 0)
                    {
                        possibleQueenMoves[row_x, column_x] = queenRookMoves[row_x, column_x];
                    }

                    if(queenBishopMoves[row_x, column_x] != 0)
                    {
                        possibleQueenMoves[row_x, column_x] = queenBishopMoves[row_x, column_x];
                    }
                }
            }

            return possibleQueenMoves;
        }

        /// <summary>
        /// Generates the threats for a white queen.
        /// </summary>
        /// <param name="inputArr"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int[,] GenerateThreats(int[,] inputArr, int row, int column)
        {
            int[,] possibleQueenThreats = new int[8, 8];
            int[,] queenRookThreats = queenWhiteRook.GenerateThreats(inputArr, row, column);
            int[,] queenBishopThreats = queenWhiteBishop.GenerateThreats(inputArr, row, column);

            for (int row_x = 0; row_x < 8; row_x++)
            {
                for (int column_x = 0; column_x < 8; column_x++)
                {
                    if (queenRookThreats[row_x, column_x] != 0)
                    {
                        possibleQueenThreats[row_x, column_x] = queenRookThreats[row_x, column_x];
                    }

                    if (queenBishopThreats[row_x, column_x] != 0)
                    {
                        possibleQueenThreats[row_x, column_x] = queenBishopThreats[row_x, column_x];
                    }
                }
            }

            return possibleQueenThreats;
        }
    }
}
