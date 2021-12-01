using System;
using System.Collections.Generic;
using System.Text;

namespace JChessV3.Pieces.WhitePieces
{
    class WhitePawn : Piece
    {
        public WhitePawn()
        {

        }

        /// <summary>
        /// Generates the moves for a White Pawn. Does not account for pins, TODO: Enpassant
        /// </summary>
        /// <param name="inputBoard"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int[,] GenerateMoves(int[,] inputBoard, int row, int column)
        {
            int[,] possiblePawnMoves = new int[8,8];

            if (row - 1 >= 0)
            {
                if (inputBoard[row - 1, column] == 0)
                {
                    possiblePawnMoves[row - 1, column] = 1;
                }

                if (column - 1 >= 0 && inputBoard[row - 1, column - 1] < 0)
                {
                    possiblePawnMoves[row - 1, column - 1] = -1;
                }

                if (column + 1 < 8 && inputBoard[row - 1, column + 1] < 0)
                {
                    possiblePawnMoves[row - 1, column + 1] = -1;
                }

                if (column + 1 < 8 && inputBoard[row, column + 1] == -11)
                {
                    possiblePawnMoves[row - 1, column + 1] = 2;
                }

                if (column + 1 < 8 && inputBoard[row, column - 1] == -11)
                {
                    possiblePawnMoves[row - 1, column + 1] = 2;
                }
            }
            
            if (row == 6)
            {
                if (inputBoard[row - 1, column] == 0)
                {
                    possiblePawnMoves[row - 1, column] = 1;

                    if (inputBoard[row - 2, column] == 0)
                    {
                        possiblePawnMoves[row - 2, column] = 1;
                    }
                }
            }

            return possiblePawnMoves;
        }

        /// <summary>
        /// Generates the squares which the pawn is threatening.
        /// </summary>
        /// <param name="inputBoard"></param>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public int[,] GenerateThreats(int[,] inputBoard, int row, int column)
        {
            int[,] possiblePawnThreats = new int[8, 8];

            if (row - 1 >= 0)
            {
                if (column - 1 >= 0)
                {
                    possiblePawnThreats[row - 1, column - 1] = 1;
                }

                if (column + 1 < 8)
                {
                    possiblePawnThreats[row - 1, column + 1] = 1;
                }
            }

            return possiblePawnThreats;
        }
    }
}
