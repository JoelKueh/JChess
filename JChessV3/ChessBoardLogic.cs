using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using JChessV3.Pieces.WhitePieces;
using JChessV3.Pieces.BlackPieces;

using System.Diagnostics;

namespace JChessV3
{
    class ChessBoardLogic
    {
        private int[,] chessBoardPieces;
        private int[,] chessBoardResetArr;
        private int[,] chessBoardTestArr;

        private WhitePawn myWhitePawn;
        private WhiteBishop myWhiteBishop;
        private WhiteRook myWhiteRook;
        private WhiteQueen myWhiteQueen;
        private WhiteKnight myWhiteKnight;
        private WhiteKing myWhiteKing;

        private BlackPawn myBlackPawn;
        private BlackBishop myBlackBishop;
        private BlackRook myBlackRook;
        private BlackQueen myBlackQueen;
        private BlackKnight myBlackKnight;
        private BlackKing myBlackKing;

        public ChessBoardLogic()
        {
            chessBoardResetArr = new int[8, 8]
              { {-41,-2,-3,-5,-61,-3,-2,-41 },
                {-1,-1,-1,-1,-1,-1,-1,-1 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 1 },
                {41, 2, 3, 5, 61, 3, 2,41 } };

            chessBoardPieces = new int[8, 8]
              { {-41,-2,-3,-5,-61,-3,-2,-41 },
                {-1,-1,-1,-1,-1,-1,-1,-1 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 1 },
                {41, 2, 3, 5,61, 3, 2,41 } };

            chessBoardTestArr = new int[8, 8]
              { {-41,0, 0, 0,-61, 0, 0,-41 },
                {-1, 0,-1, 0, 0, 0, 0,-1 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0,-1, 1, 0, 0, 0, 3, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 0, 0, 1, 1, 1 },
                {41, 0, 0, 0,61, 0, 0,41 } };

            myWhitePawn = new WhitePawn();
            myWhiteBishop = new WhiteBishop();
            myWhiteRook = new WhiteRook();
            myWhiteQueen = new WhiteQueen();
            myWhiteKnight = new WhiteKnight();
            myWhiteKing = new WhiteKing();

            myBlackPawn = new BlackPawn();
            myBlackBishop = new BlackBishop();
            myBlackRook = new BlackRook();
            myBlackQueen = new BlackQueen();
            myBlackKnight = new BlackKnight();
            myBlackKing = new BlackKing();
        }

        /// <summary>
        /// Resets the Chess Board
        /// </summary>
        public void ChessBoardReset()
        {
            Array.Copy(chessBoardResetArr, 0, chessBoardPieces, 0, 64);
            //Array.Copy(chessBoardTestArr, 0, chessBoardPieces, 0, 64);
        }

        /// <summary>
        /// Prints the whole chess board array to the Output. (Will be deleted later).
        /// </summary>
        /// <param name="arr"></param>
        public void PrintChessBoardArray(int[,] arr)
        {
            for (int column = 0; column < 8; column++)
            {
                for (int row = 0; row < 8; row++)
                {
                    Debug.Write(chessBoardPieces[row, column]);
                }
            }
        }

        /// <summary>
        /// Returns the chess board piece at the inputted coordinates.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetChessBoardSquare(int column, int row)
        {
            return chessBoardPieces[row, column];
        }

        /// <summary>
        /// Takes an input of a column and row, then checks the chess board array for the piece at those coordinates, then returns the array of possible moves that the piece
        /// can make.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public int[,] GetPieceMoves(int column, int row)
        {
            switch (chessBoardPieces[row, column])
            {
                case Const.WHITE_KING: return myWhiteKing.GenerateMoves(chessBoardPieces, GenerateDangerSquares(Const.WHITE), row, column);
                case Const.WHITE_QUEEN: return myWhiteQueen.GenerateMoves(chessBoardPieces, row, column);
                case Const.WHITE_ROOK: return myWhiteRook.GenerateMoves(chessBoardPieces, row, column);
                case Const.WHITE_BISHOP: return myWhiteBishop.GenerateMoves(chessBoardPieces, row, column);
                case Const.WHITE_KNIGHT: return myWhiteKnight.GenerateMoves(chessBoardPieces, row, column);
                case Const.WHITE_PAWN: return myWhitePawn.GenerateMoves(chessBoardPieces, row, column);

                case Const.BLACK_PAWN: return myBlackPawn.GenerateMoves(chessBoardPieces, row, column);
                case Const.BLACK_KNIGHT: return myBlackKnight.GenerateMoves(chessBoardPieces, row, column);
                case Const.BLACK_BISHOP: return myBlackBishop.GenerateMoves(chessBoardPieces, row, column);
                case Const.BLACK_ROOK: return myBlackRook.GenerateMoves(chessBoardPieces, row, column);
                case Const.BLACK_QUEEN: return myBlackQueen.GenerateMoves(chessBoardPieces, row, column);
                case Const.BLACK_KING: return myBlackKing.GenerateMoves(chessBoardPieces, GenerateDangerSquares(Const.BLACK), row, column);

                case Const.C_BLACK_KING: return myBlackKing.GeneratePreCastledMoves(chessBoardPieces, GenerateDangerSquares(Const.BLACK), row, column);
                case Const.C_WHITE_KING: return myWhiteKing.GeneratePreCastledMoves(chessBoardPieces, GenerateDangerSquares(Const.WHITE), row, column);
                case Const.C_BLACK_ROOK: return myBlackRook.GenerateMoves(chessBoardPieces, row, column);
                case Const.C_WHITE_ROOK: return myWhiteRook.GenerateMoves(chessBoardPieces, row, column);

                    //case Const.C_BLACK_KING: return myBlackKing.GenerateMoves
            }
            return new int[8,8];
        }

        public int[,] GetPieceThreats(int column, int row)
        {
            switch (chessBoardPieces[row, column])
            {
                case Const.WHITE_KING: return myWhiteKing.GenerateThreats(chessBoardPieces, row, column);
                case Const.WHITE_QUEEN: return myWhiteQueen.GenerateThreats(chessBoardPieces, row, column);
                case Const.WHITE_ROOK: return myWhiteRook.GenerateThreats(chessBoardPieces, row, column);
                case Const.WHITE_BISHOP: return myWhiteBishop.GenerateThreats(chessBoardPieces, row, column);
                case Const.WHITE_KNIGHT: return myWhiteKnight.GenerateThreats(chessBoardPieces, row, column);
                case Const.WHITE_PAWN: return myWhitePawn.GenerateThreats(chessBoardPieces, row, column);

                case Const.BLACK_PAWN: return myBlackPawn.GenerateThreats(chessBoardPieces, row, column);
                case Const.BLACK_KNIGHT: return myBlackKnight.GenerateThreats(chessBoardPieces, row, column);
                case Const.BLACK_BISHOP: return myBlackBishop.GenerateThreats(chessBoardPieces, row, column);
                case Const.BLACK_ROOK: return myBlackRook.GenerateThreats(chessBoardPieces, row, column);
                case Const.BLACK_QUEEN: return myBlackQueen.GenerateThreats(chessBoardPieces, row, column);
                case Const.BLACK_KING: return myBlackKing.GenerateThreats(chessBoardPieces, row, column);

                case Const.C_BLACK_KING: return myBlackKing.GenerateThreats(chessBoardPieces, row, column);
                case Const.C_WHITE_KING: return myWhiteKing.GenerateThreats(chessBoardPieces, row, column);
                case Const.C_BLACK_ROOK: return myBlackRook.GenerateThreats(chessBoardPieces, row, column);
                case Const.C_WHITE_ROOK: return myWhiteRook.GenerateThreats(chessBoardPieces, row, column);
            }
            return new int[8, 8];
        }

        //public int[,] readFenString(string fenInput)
        //{
        //    for (int i = 0; i < fenInput.Length; i++)
        //    {
        //        int rowNum = i / 8;
        //    }
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="possibleMoves"></param>
        /// <param name="currentBoard"></param>
        /// <returns></returns>
        public int[,] CheckPinnedPiece(int[,] possibleMoves, int[,] currentBoard)
        {
            return new int[8, 8];
        }

        /// <summary>
        /// Returns the danger squares that the king cannot move to given the color of the king. TODO: Make a generate threats method for each of the pieces because it is different
        /// than the generate possible moves method.
        /// </summary>
        /// <param name="inputArr"></param>
        /// <returns></returns>
        public int[,] GenerateDangerSquares(int defendingColor)
        {
            int[,] dangerSquares = new int[8, 8];

            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    int[,] tempStorage = new int[8, 8];
                    
                    if (chessBoardPieces[row, column] < 0 && defendingColor > 0)
                    {
                        tempStorage = GetPieceThreats(column, row);
                    }
                    else if (chessBoardPieces[row, column] > 0 && defendingColor < 0)
                    {
                        tempStorage = GetPieceThreats(column, row);
                    }

                    for (int row_temp = 0; row_temp < 8; row_temp++)
                    {
                        for (int column_temp = 0; column_temp < 8; column_temp++)
                        {
                            if (tempStorage[row_temp, column_temp] == 1)
                            {
                                dangerSquares[row_temp, column_temp] = 1;
                            }
                        }
                    }
                }
            }

            return dangerSquares;
        }
    }
}
