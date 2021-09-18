using System;
using System.Collections.Generic;
using System.Text;

namespace JChessV3.Pieces
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

        public int[,] generateMoves(int[,] inputArr, int row, int column)
        {
            int[,] possibleQueenMoves = new int[8, 8];
            int[,] queenRookMoves = queenWhiteRook.generateMoves(inputArr, row, column);
            int[,] queenBishopMoves = queenWhiteBishop.generateMoves(inputArr, row, column);

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
    }
}
