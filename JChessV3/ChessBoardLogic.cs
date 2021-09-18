using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using JChessV3.Pieces;

using System.Diagnostics;

namespace JChessV3
{
    class ChessBoardLogic
    {
        private int[,] chessBoardPieces;
        private int[,] chessBoardResetArr;

        private WhitePawn myWhitePawn;
        private WhiteBishop myWhiteBishop;
        private WhiteRook myWhiteRook;
        private WhiteQueen myWhiteQueen;

        public ChessBoardLogic()
        {
            chessBoardResetArr = new int[8, 8]
              { {-4,-2,-3,-5,-6,-3,-2,-4 },
                {-1,-1,-1,-1,-1,-1,-1,-1 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 0, 0, 5, 0, 0, 0, -1 },
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

            myWhitePawn = new WhitePawn();
            myWhiteBishop = new WhiteBishop();
            myWhiteRook = new WhiteRook();
            myWhiteQueen = new WhiteQueen();
        }

        public void chessBoardReset()
        {
            Array.Copy(chessBoardResetArr, 0, chessBoardPieces, 0, 64);
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

        public int getChessBoardSquare(int column, int row)
        {
            return chessBoardPieces[row, column];
        }

        public int[,] getPieceMoves(int column, int row)
        {
            switch (chessBoardPieces[row, column])
            {
                //case Const.WHITE_KING: tempTexture = whiteKingSprite;
                case Const.WHITE_QUEEN: return myWhiteQueen.generateMoves(chessBoardPieces, row, column);
                case Const.WHITE_ROOK: return myWhiteRook.generateMoves(chessBoardPieces, row, column);
                case Const.WHITE_BISHOP: return myWhiteBishop.generateMoves(chessBoardPieces, row, column);
                //case Const.WHITE_KNIGHT: tempTexture = whiteKnightSprite;
                case Const.WHITE_PAWN: return myWhitePawn.generateMoves(chessBoardPieces, row, column);

                //case Const.BLACK_PAWN: tempTexture = blackPawnSprite;
                //case Const.BLACK_KNIGHT: tempTexture = blackKnightSprite;
                //case Const.BLACK_BISHOP: tempTexture = blackBishopSprite;
                //case Const.BLACK_ROOK: tempTexture = blackRookSprite;
                //case Const.BLACK_QUEEN: tempTexture = blackQueenSprite;
                //case Const.BLACK_KING: tempTexture = blackKingSprite;
            }
            return new int[8,8];
        }

        //public int[,] readFenString(string fenInput)
        //{
        //    for (int i = 0; i < fenInput.Length; i++)
        //    {
        //        int rowNum = i / 8;
        //    }
        //}
    }
}
