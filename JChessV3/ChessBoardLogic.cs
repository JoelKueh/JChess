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

        private bool whiteTurn = true;

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
              { { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0,-3, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0,-61, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0,-4, 0, 3, 0, 0, 0, 0 } };

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

        public void AttemptMove(int startColumn, int startRow, int endColumn, int endRow)
        {
            int[,] moveStorage = GetMovesIfTurn(startColumn, startRow);
            if (moveStorage[endRow, endColumn] == 1 || moveStorage[endRow, endColumn] == -1)
            {
                ClearEnpassant();
                chessBoardPieces[endRow, endColumn] = chessBoardPieces[startRow, startColumn];
                chessBoardPieces[startRow, startColumn] = 0;
                whiteTurn = !whiteTurn;
                //CheckForMate();
            }
            else if (moveStorage[endRow, endColumn] == Const.ENP_WHITE_PAWN)
            {
                ClearEnpassant();
                chessBoardPieces[endRow, endColumn] = Const.ENP_WHITE_PAWN;
                chessBoardPieces[startRow, startColumn] = 0;
                whiteTurn = !whiteTurn;
                //CheckForMate();
            }
            else if (moveStorage[endRow, endColumn] == Const.ENP_BLACK_PAWN)
            {
                ClearEnpassant();
                chessBoardPieces[endRow, endColumn] = Const.ENP_BLACK_PAWN;
                chessBoardPieces[startRow, startColumn] = 0;
                whiteTurn = !whiteTurn;
                //CheckForMate();
            }
            else if (moveStorage[endRow, endColumn] == 2)
            {
                ClearEnpassant();
                chessBoardPieces[endRow, endColumn] = chessBoardPieces[startRow, startColumn];
                chessBoardPieces[startRow, endColumn] = 0;
                chessBoardPieces[startRow, startColumn] = 0;
                whiteTurn = !whiteTurn;
                //CheckForMate();
            }
            else if (moveStorage[endRow, endColumn] == 3)
            {
                ClearEnpassant();
                chessBoardPieces[endRow, endColumn] = chessBoardPieces[startRow, startColumn];
                chessBoardPieces[startRow, 3] = chessBoardPieces[startRow, 0];
                chessBoardPieces[startRow, 0] = 0;
                whiteTurn = !whiteTurn;
                //CheckForMate();
            }
            else if (moveStorage[endRow, endColumn] == 4)
            {
                ClearEnpassant();
                chessBoardPieces[endRow, endColumn] = chessBoardPieces[startRow, startColumn];
                chessBoardPieces[startRow, 5] = chessBoardPieces[startRow, 7];
                chessBoardPieces[startRow, 7] = 0;
                whiteTurn = !whiteTurn;
                //CheckForMate();
            }
        }

        /// <summary>
        /// Adjusts the output of GetChessBoardMoves for the color of the current turn.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public int[,] GetMovesIfTurn(int column, int row)
        {
            if (chessBoardPieces[row, column] > 0 && whiteTurn)
            {
                return GetFinalMoveset(column, row);
            }
            else if (chessBoardPieces[row, column] < 0 && !whiteTurn)
            {
                return GetFinalMoveset(column, row);
            }
            else
            {
                return new int[8, 8];
            }
        }

        /// <summary>
        /// Resets the Chess Board
        /// </summary>
        public void ChessBoardReset()
        {
            whiteTurn = true;

            Array.Copy(chessBoardResetArr, 0, chessBoardPieces, 0, 64);
            //Array.Copy(chessBoardTestArr, 0, chessBoardPieces, 0, 64);
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
        /// Returns the moves for the chess board pieces but then removes moves that are illegal due to pins.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public int[,] GetPinAdjPieceMoves(int column, int row)
        {
            return AdjustMovesIfPinned(chessBoardPieces, GetChessBoardMoves(column, row), row, column);
        }

        /// <summary>
        /// Takes an input of a column and row, then checks the chess board array for the piece at those coordinates, then returns the array of possible moves that the piece
        /// can make.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private int[,] GetPieceMoves(int[,] inputBoard, int column, int row)
        {
            switch (chessBoardPieces[row, column])
            {
                case Const.WHITE_KING: return myWhiteKing.GenerateMoves(inputBoard, GenerateDangerSquares(inputBoard, Const.WHITE), row, column);
                case Const.WHITE_QUEEN: return myWhiteQueen.GenerateMoves(inputBoard, row, column);
                case Const.WHITE_ROOK: return myWhiteRook.GenerateMoves(inputBoard, row, column);
                case Const.WHITE_BISHOP: return myWhiteBishop.GenerateMoves(inputBoard, row, column);
                case Const.WHITE_KNIGHT: return myWhiteKnight.GenerateMoves(inputBoard, row, column);
                case Const.WHITE_PAWN: return myWhitePawn.GenerateMoves(inputBoard, row, column);

                case Const.BLACK_PAWN: return myBlackPawn.GenerateMoves(inputBoard, row, column);
                case Const.BLACK_KNIGHT: return myBlackKnight.GenerateMoves(inputBoard, row, column);
                case Const.BLACK_BISHOP: return myBlackBishop.GenerateMoves(inputBoard, row, column);
                case Const.BLACK_ROOK: return myBlackRook.GenerateMoves(inputBoard, row, column);
                case Const.BLACK_QUEEN: return myBlackQueen.GenerateMoves(inputBoard, row, column);
                case Const.BLACK_KING: return myBlackKing.GenerateMoves(inputBoard, GenerateDangerSquares(inputBoard, Const.BLACK), row, column);

                case Const.C_BLACK_KING: return myBlackKing.GeneratePreCastledMoves(inputBoard, GenerateDangerSquares(inputBoard, Const.BLACK), row, column);
                case Const.C_WHITE_KING: return myWhiteKing.GeneratePreCastledMoves(inputBoard, GenerateDangerSquares(inputBoard, Const.WHITE), row, column);
                case Const.C_BLACK_ROOK: return myBlackRook.GenerateMoves(inputBoard, row, column);
                case Const.C_WHITE_ROOK: return myWhiteRook.GenerateMoves(inputBoard, row, column);

                case Const.ENP_BLACK_PAWN: return myBlackPawn.GenerateMoves(inputBoard, row, column);
                case Const.ENP_WHITE_PAWN: return myWhitePawn.GenerateMoves(inputBoard, row, column);
            }
            return new int[8,8];
        }

        public int[,] GetChessBoardMoves(int column, int row)
        {
            return GetPieceMoves(chessBoardPieces, column, row);
        }

        /// <summary>
        /// Takes an input of a column and row, then returns the squares that the piece at those coordinates is threatening.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private int[,] GetPieceThreats(int[,] inputBoard, int column, int row)
        {
            switch (chessBoardPieces[row, column])
            {
                case Const.WHITE_KING: return myWhiteKing.GenerateThreats(inputBoard, row, column);
                case Const.WHITE_QUEEN: return myWhiteQueen.GenerateThreats(inputBoard, row, column);
                case Const.WHITE_ROOK: return myWhiteRook.GenerateThreats(inputBoard, row, column);
                case Const.WHITE_BISHOP: return myWhiteBishop.GenerateThreats(inputBoard, row, column);
                case Const.WHITE_KNIGHT: return myWhiteKnight.GenerateThreats(inputBoard, row, column);
                case Const.WHITE_PAWN: return myWhitePawn.GenerateThreats(inputBoard, row, column);

                case Const.BLACK_PAWN: return myBlackPawn.GenerateThreats(inputBoard, row, column);
                case Const.BLACK_KNIGHT: return myBlackKnight.GenerateThreats(inputBoard, row, column);
                case Const.BLACK_BISHOP: return myBlackBishop.GenerateThreats(inputBoard, row, column);
                case Const.BLACK_ROOK: return myBlackRook.GenerateThreats(inputBoard, row, column);
                case Const.BLACK_QUEEN: return myBlackQueen.GenerateThreats(inputBoard, row, column);
                case Const.BLACK_KING: return myBlackKing.GenerateThreats(inputBoard, row, column);

                case Const.C_BLACK_KING: return myBlackKing.GenerateThreats(inputBoard, row, column);
                case Const.C_WHITE_KING: return myWhiteKing.GenerateThreats(inputBoard, row, column);
                case Const.C_BLACK_ROOK: return myBlackRook.GenerateThreats(inputBoard, row, column);
                case Const.C_WHITE_ROOK: return myWhiteRook.GenerateThreats(inputBoard, row, column);

                case Const.ENP_BLACK_PAWN: return myBlackPawn.GenerateThreats(inputBoard, row, column);
                case Const.ENP_WHITE_PAWN: return myWhitePawn.GenerateThreats(inputBoard, row, column);

            }
            return new int[8, 8];
        }

        public int[,] GetChessBoardThreats(int column, int row)
        {
            return GetPieceThreats(chessBoardPieces, column, row);
        }

        //public int[,] readFenString(string fenInput)
        //{
        //    for (int i = 0; i < fenInput.Length; i++)
        //    {
        //        int rowNum = i / 8;
        //    }
        //}

        /// <summary>
        /// Returns the danger squares that the king cannot move to given the color of the king.
        /// </summary>
        /// <param name="inputArr"></param>
        /// <returns></returns>
        private int[,] GenerateDangerSquares(int[,] inputBoard, int defendingColor)
        {
            int[,] dangerSquares = new int[8, 8];

            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    int[,] tempStorage = new int[8, 8];
                    
                    if (inputBoard[row, column] < 0 && defendingColor > 0)
                    {
                        tempStorage = GetPieceThreats(inputBoard, column, row);
                    }
                    else if (inputBoard[row, column] > 0 && defendingColor < 0)
                    {
                        tempStorage = GetPieceThreats(inputBoard, column, row);
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

        public int[,] GenerateChessBoardDangerSquares(int defendingColor)
        {
            return GenerateDangerSquares(chessBoardPieces, defendingColor);
        }

        /// <summary>
        /// Takes a piece at a column and row, along with a set of possible moves, then removes all of the moves that are illegal for one reason or another
        /// </summary>
        /// <param name="inputArr"></param>
        /// <param name="inputMoves"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int[,] AdjustMovesIfPinned(int[,] inputArr, int[,] inputMoves, int row, int column)
        {
            // This relies on the assumption that the most pins are accounted for by rooks / bishops / queens.
            // The easiest way for me to figure out pins is to generate the queen threats for a certain piece then use that information to discover whether a piece is pinned.
            // En-Passant can bypass the origional check so it is handled individually.

            bool white = false;
            if (inputArr[row, column] > 0)
            {
                white = true;
            }

            int[,] queenThreatStorage = new int[8, 8];
            int kingX = -1;
            int kingY = -1;

            int pinningPieceX = -1;
            int pinningPieceY = -1;

            if ( // the piece at (row, column) is a pawn and is next to an enpassant pawn
                   (inputArr[row, column] == Const.WHITE_PAWN && (column - 1 >= 0 && (inputArr[row, column - 1] == Const.ENP_BLACK_PAWN) || (column + 1 <= 7 && inputArr[row, column + 1] == Const.ENP_BLACK_PAWN))) ||
                   (inputArr[row, column] == Const.BLACK_PAWN && (column - 1 >= 0 && (inputArr[row, column - 1] == Const.ENP_WHITE_PAWN) || (column + 1 <= 7 && inputArr[row, column + 1] == Const.ENP_WHITE_PAWN)))
               )
            {
                // Create temporary arrays to store the chessboard so we can run tests to see if the king would be in check.
                int[,] tempSecondBoard = new int[8, 8];
                Array.Copy(inputArr, 0, tempSecondBoard, 0, 64);

                int[,] tempDangerSquares = new int[8, 8];

                // Check the inputted board to see whether or not there is a possible enpassant, if there is, generate the danger squares
                // in the temporary array to see if the king would be in danger after the move happened.
                if (column - 1 >= 0 && inputArr[row, column - 1] == Const.ENP_BLACK_PAWN)
                {
                    tempSecondBoard[row, column - 1] = 0;
                    tempSecondBoard[row, column] = 0;
                    tempSecondBoard[row - 1, column - 1] = Const.WHITE_PAWN;

                    tempDangerSquares = GenerateDangerSquares(tempSecondBoard, Const.WHITE);
                }
                else if (column + 1 <= 7 && inputArr[row, column + 1] == Const.ENP_BLACK_PAWN)
                {
                    tempSecondBoard[row, column + 1] = 0;
                    tempSecondBoard[row, column] = 0;
                    tempSecondBoard[row - 1, column + 1] = Const.WHITE_PAWN;

                    tempDangerSquares = GenerateDangerSquares(tempSecondBoard, Const.WHITE);
                }
                else if (column - 1 >= 0 && inputArr[row, column - 1] == Const.ENP_WHITE_PAWN)
                {
                    tempSecondBoard[row, column - 1] = 0;
                    tempSecondBoard[row, column] = 0;
                    tempSecondBoard[row + 1, column - 1] = Const.BLACK_PAWN;

                    tempDangerSquares = GenerateDangerSquares(tempSecondBoard, Const.BLACK);
                }
                else if (column + 1 <= 7 && inputArr[row, column + 1] == Const.ENP_BLACK_PAWN)
                {
                    tempSecondBoard[row, column + 1] = 0;
                    tempSecondBoard[row, column] = 0;
                    tempSecondBoard[row + 1, column + 1] = Const.BLACK_PAWN;

                    tempDangerSquares = GenerateDangerSquares(tempSecondBoard, Const.BLACK);
                }

                // Run through every square in the tempDangerSquares array to see if there is a king that is in check.
                bool kingInCheck = false;
                if (white)
                {
                    for (int row_1 = 0; row_1 < 8; row_1++)
                    {
                        for (int column_1 = 0; column_1 < 8; column_1++)
                        {
                            if (tempDangerSquares[row_1, column_1] != 0 && (tempSecondBoard[row_1, column_1] == Const.WHITE_KING || tempSecondBoard[row_1, column_1] == Const.C_WHITE_KING))
                            {
                                kingInCheck = true;
                            }
                        }
                    }
                }
                else
                {
                    for (int row_1 = 0; row_1 < 8; row_1++)
                    {
                        for (int column_1 = 0; column_1 < 8; column_1++)
                        {
                            if (tempDangerSquares[row_1, column_1] != 0 && (tempSecondBoard[row_1, column_1] == Const.BLACK_KING || tempSecondBoard[row_1, column_1] == Const.C_BLACK_KING))
                            {
                                kingInCheck = true;
                            }
                        }
                    }
                }

                // If there is a king in check, then get rid of the affected enpassant moves (but not the normal takes)
                if (kingInCheck)
                {
                    if(inputMoves[row - 1, column - 1] == 2)
                    {
                        inputMoves[row - 1, column - 1] = 0;
                    }
                    
                    if(inputMoves[row + 1, column - 1] == 2)
                    {
                        inputMoves[row + 1, column - 1] = 0;
                    }

                    if(inputMoves[row - 1, column + 1] == 2)
                    {
                        inputMoves[row - 1, column + 1] = 0;
                    }

                    if (inputMoves[row + 1, column + 1] == 2)
                    {
                        inputMoves[row + 1, column + 1] = 0;
                    }
                }

                Array.Copy(inputArr, 0, tempSecondBoard, 0, 64);
                Array.Clear(tempDangerSquares, 0, 64);

                if (inputArr[row, column] == Const.WHITE_PAWN)
                {
                    if (inputArr[row - 1, column] == 0)
                    {
                        tempSecondBoard[row - 1, column] = 1;
                        tempSecondBoard[row, column] = 0;

                        tempDangerSquares = GenerateDangerSquares(tempSecondBoard, Const.WHITE_PAWN);
                    }
                }
                if (inputArr[row, column] == Const.BLACK_PAWN)
                {
                    if (inputArr[row + 1, column] == 0)
                    {
                        tempSecondBoard[row + 1, column] = 1;
                        tempSecondBoard[row, column] = 0;

                        tempDangerSquares = GenerateDangerSquares(tempSecondBoard, Const.BLACK_PAWN);
                    }
                }

                kingInCheck = false;
                if (white)
                {
                    for (int row_1 = 0; row_1 < 8; row_1++)
                    {
                        for (int column_1 = 0; column_1 < 8; column_1++)
                        {
                            if (tempDangerSquares[row_1, column_1] != 0 && (tempSecondBoard[row_1, column_1] == Const.WHITE_KING || tempSecondBoard[row_1, column_1] == Const.C_WHITE_KING))
                            {
                                kingInCheck = true;
                            }
                        }
                    }
                }
                else
                {
                    for (int row_1 = 0; row_1 < 8; row_1++)
                    {
                        for (int column_1 = 0; column_1 < 8; column_1++)
                        {
                            if (tempDangerSquares[row_1, column_1] != 0 && (tempSecondBoard[row_1, column_1] == Const.BLACK_KING || tempSecondBoard[row_1, column_1] == Const.C_BLACK_KING))
                            {
                                kingInCheck = true;
                            }
                        }
                    }
                }

                if (kingInCheck)
                {
                    inputMoves[row - 1, column] = 0;
                    inputMoves[row + 1, column] = 0;
                }

                return inputMoves;
            }
            else
            {
                if (white)
                {
                    queenThreatStorage = myWhiteQueen.GenerateThreats(inputArr, row, column);

                    for (int row_1 = 0; row_1 < 8 && kingX == -1; row_1++)
                    {
                        for (int column_1 = 0; column_1 < 8 && kingX == -1; column_1++)
                        {
                            if (queenThreatStorage[row_1, column_1] == 1 && (inputArr[row_1, column_1] == Const.WHITE_KING || inputArr[row_1, column_1] == Const.C_WHITE_KING))
                            {
                                kingX = column_1;
                                kingY = row_1;
                            }
                        }
                    }

                    for (int row_1 = 0; row_1 < 8 && pinningPieceX == -1; row_1++)
                    {
                        for (int column_1 = 0; column_1 < 8 && pinningPieceX == -1; column_1++)
                        {
                            if (queenThreatStorage[row_1, column_1] == 1 &&
                                (inputArr[row_1, column_1] == Const.BLACK_QUEEN || inputArr[row_1, column_1] == Const.BLACK_ROOK || inputArr[row_1, column_1] == Const.C_BLACK_ROOK || inputArr[row_1, column_1] == Const.BLACK_BISHOP))
                            {
                                if (queenThreatStorage[row_1, column_1] == 1)
                                {
                                    if ( (kingX == column && column_1 == column) || (kingY == row && row_1 == row) )
                                    {
                                        if (inputArr[row_1, column_1] != Const.BLACK_BISHOP)
                                        {
                                            pinningPieceX = column_1;
                                            pinningPieceY = row_1;
                                        }
                                    }
                                    else if (kingX - kingY == column_1 - row_1)
                                    {
                                        if (inputArr[row_1, column_1] != Const.BLACK_ROOK && inputArr[row_1, column_1] != Const.C_BLACK_ROOK)
                                        {
                                            pinningPieceX = column_1;
                                            pinningPieceY = row_1;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (kingX == -1 || pinningPieceX == -1)
                    {
                        return inputMoves;
                    }

                    int stepDirX = 0;
                    int stepDirY = 0;
                    if (kingX == pinningPieceX)
                    {
                        stepDirX = 0;
                    }
                    else if (kingX < pinningPieceX)
                    {
                        stepDirX = -1;
                    }
                    else
                    {
                        stepDirX = 1;
                    }

                    if (kingY == pinningPieceY)
                    {
                        stepDirY = 0;
                    }
                    else if (kingY < pinningPieceY)
                    {
                        stepDirY = -1;
                    }
                    else
                    {
                        stepDirY = 1;
                    }

                    int[,] legalLeftoverMoves = new int[8, 8];
                    for (int row_1 = pinningPieceY, column_1 = pinningPieceX; row_1 < 8 && column_1 < 8 && row_1 >= 0 && column_1 >= 0; row_1 += stepDirY)
                    {
                        legalLeftoverMoves[row_1, column_1] = 1;
                        column_1 += stepDirX;
                    }

                    for (int row_1 = pinningPieceY, column_1 = pinningPieceX; row_1 < 8 && column_1 < 8 && row_1 >= 0 && column_1 >= 0; row_1 += stepDirY)
                    {
                        legalLeftoverMoves[row_1, column_1] = 1;
                        column_1 += stepDirX;
                    }

                    for (int row_1 = 0; row_1 < 8; row_1++)
                    {
                        for (int column_1 = 0; column_1 < 8; column_1++)
                        {
                            if (legalLeftoverMoves[row_1, column_1] == 0)
                            {
                                inputMoves[row_1, column_1] = 0;
                            }
                        }
                    }

                    return inputMoves;
                }
                else
                {
                    queenThreatStorage = myBlackQueen.GenerateThreats(inputArr, row, column);

                    for (int row_1 = 0; row_1 < 8 && kingX == -1; row_1++)
                    {
                        for (int column_1 = 0; column_1 < 8 && kingX == -1; column_1++)
                        {
                            if (queenThreatStorage[row_1, column_1] == 1 && (inputArr[row_1, column_1] == Const.BLACK_KING || inputArr[row_1, column_1] == Const.C_BLACK_KING))
                            {
                                kingX = column_1;
                                kingY = row_1;
                            }
                        }
                    }

                    for (int row_1 = 0; row_1 < 8 && pinningPieceX == -1; row_1++)
                    {
                        for (int column_1 = 0; column_1 < 8 && pinningPieceX == -1; column_1++)
                        {
                            if (queenThreatStorage[row_1, column_1] == 1 &&
                                (inputArr[row_1, column_1] == Const.WHITE_QUEEN || inputArr[row_1, column_1] == Const.WHITE_ROOK || inputArr[row_1, column_1] == Const.C_WHITE_ROOK || inputArr[row_1, column_1] == Const.WHITE_BISHOP))
                            {
                                if (queenThreatStorage[row_1, column_1] == 1)
                                {
                                    if ((kingX == column && column_1 == column) || (kingY == row && row_1 == row))
                                    {
                                        if (inputArr[row_1, column_1] != Const.WHITE_BISHOP)
                                        {
                                            pinningPieceX = column_1;
                                            pinningPieceY = row_1;
                                        }
                                    }
                                    else if (kingX - kingY == column_1 - row_1 || kingX + kingY == column_1 + row_1)
                                    {
                                        if (inputArr[row_1, column_1] != Const.WHITE_ROOK && inputArr[row_1, column_1] != Const.C_WHITE_ROOK)
                                        {
                                            pinningPieceX = column_1;
                                            pinningPieceY = row_1;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (kingX == -1 || pinningPieceX == -1)
                    {
                        return inputMoves;
                    }

                    int stepDirX = 0;
                    int stepDirY = 0;
                    if (kingX == pinningPieceX)
                    {
                        stepDirX = 0;
                    }
                    else if (kingX < pinningPieceX)
                    {
                        stepDirX = -1;
                    }
                    else
                    {
                        stepDirX = 1;
                    }

                    if (kingY == pinningPieceY)
                    {
                        stepDirY = 0;
                    }
                    else if (kingY < pinningPieceY)
                    {
                        stepDirY = -1;
                    }
                    else
                    {
                        stepDirY = 1;
                    }

                    int[,] legalLeftoverMoves = new int[8, 8];
                    for (int row_1 = pinningPieceY, column_1 = pinningPieceX; row_1 < 8 && column_1 < 8 && row_1 >= 0 && column_1 >= 0; row_1 += stepDirY)
                    {
                        legalLeftoverMoves[row_1, column_1] = 1;
                        column_1 += stepDirX;
                    }

                    for (int row_1 = pinningPieceY, column_1 = pinningPieceX; row_1 < 8 && column_1 < 8 && row_1 >= 0 && column_1 >= 0; row_1 += stepDirY)
                    {
                        legalLeftoverMoves[row_1, column_1] = 1;
                        column_1 += stepDirX;
                    }

                    for (int row_1 = 0; row_1 < 8; row_1++)
                    {
                        for (int column_1 = 0; column_1 < 8; column_1++)
                        {
                            if (legalLeftoverMoves[row_1, column_1] == 0)
                            {
                                inputMoves[row_1, column_1] = 0;
                            }
                        }
                    }

                    return inputMoves;
                }
            }
        }
        
        public void ClearEnpassant()
        {
            for (int row_1 = 0; row_1 < 8; row_1++)
            {
                for (int column_1 = 0; column_1 < 8; column_1++)
                {
                    if (chessBoardPieces[row_1, column_1] == Const.ENP_BLACK_PAWN)
                    {
                        chessBoardPieces[row_1, column_1] = Const.BLACK_PAWN;
                    }

                    if (chessBoardPieces[row_1, column_1] == Const.ENP_WHITE_PAWN)
                    {
                        chessBoardPieces[row_1, column_1] = Const.WHITE_PAWN;
                    }
                }
            }
        }

        public int[,] AdjustMovesIfInCheck(int[,] inputMoves, bool whiteTurn, int heldPiece)
        {
            int[,] outputMoves = new int[8, 8];
            int defendingColor = 0;
            int kingX = 0;
            int kingY = 0;
            if(whiteTurn)
            {
                defendingColor = 1;
            }
            else
            {
                defendingColor = -1;
            }

            if (heldPiece == defendingColor * Const.WHITE_KING || heldPiece == defendingColor * Const.C_WHITE_KING)
            {
                return inputMoves;
            }

            // First of all, uses the GenerateDangerSquares method to check if the king is in check.
            bool inCheck = false;
            int[,] tempDangerSquares = GenerateDangerSquares(chessBoardPieces, defendingColor);
            for (int row_1 = 0; row_1 < 8 && kingX != -1; row_1++)
            {
                for (int column_1 = 0; column_1 < 8 && kingX != -1; column_1++)
                {
                    if (chessBoardPieces[row_1, column_1] == defendingColor * Const.WHITE_KING || chessBoardPieces[row_1, column_1] == defendingColor * Const.C_WHITE_KING)
                    {
                        kingX = column_1;
                        kingY = row_1;
                    }

                    if (tempDangerSquares[row_1, column_1] == 1 && (chessBoardPieces[row_1, column_1] == defendingColor * Const.WHITE_KING || chessBoardPieces[row_1, column_1] == defendingColor * Const.C_WHITE_KING))
                    {
                        inCheck = true;
                    }
                }
            }
            if (!inCheck)
            {
                return inputMoves;
            }

            // If it is, check first for horses that are threatening the king.
            // If there are any, then the only moves will be king moves and takes on the horse.
            // We only worry about the position of the first knight because, if there are two, the only valid moves are king moves anyway. Those moves
            // will be calculated with by using the danger squares map.
            int[,] kingKnightMoves = myBlackKnight.GenerateThreats(chessBoardPieces, kingY, kingX);
            int numOfKnightChecks = 0;
            int knightX = -1;
            int knightY = -1;
            for (int row_1 = 0; row_1 < 8 && numOfKnightChecks < 2; row_1++)
            {
                for (int column_1 = 0; column_1 < 8 && numOfKnightChecks < 2; column_1++)
                {
                    if (kingKnightMoves[row_1, column_1] == 1 && chessBoardPieces[row_1, column_1] == defendingColor * Const.BLACK_KNIGHT)
                    {
                        numOfKnightChecks++;
                        knightX = column_1;
                        knightY = row_1;
                    }
                }
            }
            // Skip the rest of calculation if we already have 2 attackers.
            if (numOfKnightChecks > 1)
            {
                return new int[8, 8];
            }

            // We only care about the first attacker for the same reason as the knights.
            int[,] kingQueenMoves = new int[8,8];
            if (defendingColor == Const.WHITE)
            {
                kingQueenMoves = myWhiteQueen.GenerateThreats(chessBoardPieces, kingY, kingX);
            }
            if (defendingColor == Const.BLACK)
            { 
                kingQueenMoves = myBlackQueen.GenerateThreats(chessBoardPieces, kingY, kingX);
            }
            int numOfOtherChecks = 0;
            int checkingPieceX = -1;
            int checkingPieceY = -1;
            // Look through chessBoardPieces array and kingQueenMoves array simultaneously.
            for (int row_1 = 0; row_1 < 8 && numOfOtherChecks + numOfKnightChecks < 2; row_1++)
            {
                for (int column_1 = 0; column_1 < 8 && numOfOtherChecks + numOfKnightChecks < 2; column_1++)
                {
                    // Check to see if there is a piece seen by the queenThreats method, then check to see if it is a Queen, Rook, or Bishop
                    if (kingQueenMoves[row_1, column_1] == 1 &&
                        (chessBoardPieces[row_1, column_1] == defendingColor * Const.BLACK_QUEEN || chessBoardPieces[row_1, column_1] == defendingColor * Const.BLACK_ROOK
                        || chessBoardPieces[row_1, column_1] == defendingColor * Const.C_BLACK_ROOK || chessBoardPieces[row_1, column_1] == defendingColor * Const.BLACK_BISHOP))
                    {
                        //Check to see if the piece is on a straight ray or on a diagonal ray from the king.
                        // If it's on a straight ray, then bishops don't count. If it's on a diagonal ray, then rooks don't count.
                        if (kingX == column_1 || kingY == row_1)
                        {
                            if (chessBoardPieces[row_1, column_1] != defendingColor * Const.BLACK_BISHOP)
                            {
                                checkingPieceX = column_1;
                                checkingPieceY= row_1;
                                numOfOtherChecks++;
                            }
                        }
                        else if (kingX - kingY == column_1 - row_1 || kingX + kingY == column_1 + row_1)
                        {
                            if (chessBoardPieces[row_1, column_1] != defendingColor * Const.BLACK_ROOK && chessBoardPieces[row_1, column_1] != defendingColor * Const.C_BLACK_ROOK)
                            {
                                checkingPieceX = column_1;
                                checkingPieceY = row_1;
                                numOfOtherChecks++;
                            }
                        }
                    }
                }
            }
            // Skip the rest of calculation if we already have 2 attackers.
            if (numOfOtherChecks + numOfKnightChecks > 1)
            {
                return new int[8, 8];
            }

            // You get the picture, only care about the first attacking pawn.
            int checkingPawnX = 0;
            int checkingPawnY = kingY - defendingColor;
            if (kingY - defendingColor >= 0 && kingX + 1 < 8 && chessBoardPieces[kingY - defendingColor, kingX + 1] == defendingColor * Const.BLACK_PAWN)
            {
                checkingPawnX = kingX + 1;
                numOfOtherChecks++;
            }
            if (kingY - defendingColor >= 0 && kingX - 1 >= 0 && chessBoardPieces[kingY - defendingColor, kingX - 1] == defendingColor * Const.BLACK_PAWN)
            {
                checkingPawnX = kingX - 1;
                numOfOtherChecks++;
            }

            if (numOfKnightChecks + numOfOtherChecks > 1)
            {
                return new int[8,8];
            }
            else if (numOfKnightChecks == 1)
            {
                if (inputMoves[knightY, knightX] == -1)
                {
                    outputMoves[knightY, knightX] = -1;
                }
                return outputMoves;
            }
            else
            {
                int stepDirX = 0;
                int stepDirY = 0;
                if (kingX == checkingPieceX)
                {
                    stepDirX = 0;
                }
                else if (kingX < checkingPieceX)
                {
                    stepDirX = -1;
                }
                else
                {
                    stepDirX = 1;
                }

                if (kingY == checkingPieceY)
                {
                    stepDirY = 0;
                }
                else if (kingY < checkingPieceY)
                {
                    stepDirY = -1;
                }
                else
                {
                    stepDirY = 1;
                }

                for (int row_1 = checkingPieceY, column_1 = checkingPieceX; row_1 < 8 && column_1 < 8 && row_1 >= 0 && column_1 >= 0; row_1 += stepDirY)
                {
                    outputMoves[row_1, column_1] = inputMoves[row_1, column_1];
                    column_1 += stepDirX;
                }

                for (int row_1 = checkingPieceY, column_1 = checkingPieceX; row_1 < 8 && column_1 < 8 && row_1 >= 0 && column_1 >= 0; row_1 += stepDirY)
                {
                    outputMoves[row_1, column_1] = inputMoves[row_1, column_1];
                    column_1 += stepDirX;
                }

                if (chessBoardPieces[checkingPawnY, checkingPawnX] == defendingColor * Const.BLACK_PAWN && inputMoves[checkingPawnY, checkingPawnX] == -1)
                {
                    outputMoves[checkingPawnY, checkingPawnX] = -1;
                }

                return outputMoves;
            }
        }

        public int[,] AdjustMovesIfInCheck(int[,] inputMoves, bool whiteTurn, int startingRow, int startingColumn)
        {
            int heldPiece = chessBoardPieces[startingColumn, startingRow];
            return AdjustMovesIfInCheck(inputMoves, whiteTurn, heldPiece);
        }
        public int[,] GetFinalMoveset(int row, int column)
        {
            return AdjustMovesIfInCheck(GetPinAdjPieceMoves(row, column), whiteTurn, row, column);
        }

        public bool CheckForMate()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    int[,] possibleMoves = new int[8, 8];
                    if (chessBoardPieces[row, column] > 0 && whiteTurn)
                    {
                        possibleMoves = GetMovesIfTurn(row, column);
                        for (int row_1 = 0; row_1 < 8; row++)
                        {
                            for (int column_1 = 0; column_1 < 8; column_1++)
                            {
                                if (possibleMoves[row_1, column_1] != 0)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    else if(chessBoardPieces[row, column] < 0 && !whiteTurn)
                    {
                        possibleMoves = GetMovesIfTurn(row, column);
                        for (int row_1 = 0; row_1 < 8; row++)
                        {
                            for (int column_1 = 0; column_1 < 8; column_1++)
                            {
                                if (possibleMoves[row_1, column_1] != 0)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}