using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;

namespace JChessV3
{
    class ChessBoardLogic
    {
        private int[,] chessBoardPieces;
        private int[,] chessBoardResetArr;
        public ChessBoardLogic()
        {
            chessBoardResetArr = new int[8, 8]
              { {-4,-2,-3,-5,-6,-3,-2,-4 },
                {-1,-1,-1,-1,-1,-1,-1,-1 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 1 },
                { 4, 2, 3, 5, 6, 3, 2, 4 } };

            chessBoardPieces = new int[8, 8]
              { {-4,-2,-3,-5,-6,-3,-2,-4 },
                {-1,-1,-1,-1,-1,-1,-1,-1 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 1 },
                { 4, 2, 3, 5, 6, 3, 2, 4 } };
        }

        public void chessBoardReset()
        {
            Array.Copy(chessBoardPieces, 0, chessBoardPieces, 0, 64);
            //for (int column = 0; column < 8; column++)
            //{
            //    for (int row = 0; row < 8; row++)
            //    {
            //        chessBoardPieces[column, row] = chessBoardResetArr[column, row];
            //    }
            //}
        }

        public void printChessBoardArray(int[,] arr)
        {
            for (int column = 0; column < 8; column++)
            {
                for (int row = 0; row < 8; row++)
                {
                    Debug.Write(chessBoardPieces[row, column]);
                }
            }
        }

        public int getChessBoardArray(int column, int row)
        {
            return chessBoardPieces[row, column];
        }
    }
}
