using System;
using System.Collections.Generic;
using System.Text;

namespace JChessV3.Pieces.BlackPieces
{
    class BlackQueen
    {
        BlackBishop queenBlackBishop;
        BlackRook queenBlackRook;

        public BlackQueen()
        {
            queenBlackBishop = new BlackBishop();
            queenBlackRook = new BlackRook();
        }

        public int[,] generateMoves(int[,] inputArr, int row, int column)
        {
            int[,] possibleQueenMoves = new int[8, 8];
            int[,] queenRookMoves = queenBlackRook.generateMoves(inputArr, row, column);
            int[,] queenBishopMoves = queenBlackBishop.generateMoves(inputArr, row, column);

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
