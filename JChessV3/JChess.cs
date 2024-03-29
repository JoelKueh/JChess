﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// DEBUG
using System.Diagnostics;

namespace JChessV3
{
    public class JChess : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ChessBoardLogic myChessBoard;
        private Const myConst;

        #region Sprites
        // Creating the objects that store out Sprites. Will be assigned in the LoadContent Method.

        Texture2D fullChessSet;

        Texture2D blackKingSprite;
        Texture2D blackQueenSprite;
        Texture2D blackRookSprite;
        Texture2D blackBishopSprite;
        Texture2D blackKnightSprite;
        Texture2D blackPawnSprite;

        Texture2D whiteKingSprite;
        Texture2D whiteQueenSprite;
        Texture2D whiteRookSprite;
        Texture2D whiteBishopSprite;
        Texture2D whiteKnightSprite;
        Texture2D whitePawnSprite;

        Texture2D whiteElephantSprite;
        Texture2D blackElephantSprite;

        Texture2D rect;
        #endregion

        #region Other Variables
        // Creating some variables to handle important information
        //int windWidth;
        //int windHeight;

        private MouseState mouseState;
        bool mouseLeftPressed;
        struct HeldPiece
        {
            public int piece;
            public int column;
            public int row;
        }
        private HeldPiece heldPiece;

        struct Move
        {
            public int startColumn;
            public int startRow;
            public int endColumn;
            public int endRow;
        }
        private Move userMove;


        int[] columnPositions = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] rowPositions = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int chessBoardWidth = 0;
        #endregion

        public JChess()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            this.Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            // Setting up some value things
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            
            // TODO: Make Fullscreen toggleable
            _graphics.IsFullScreen = false;

            _graphics.ApplyChanges();

            myConst = new Const();

            myChessBoard = new ChessBoardLogic();
            myChessBoard.ChessBoardReset();

            userMove.startColumn = -1;
            userMove.startRow = -1;
            userMove.endColumn = -1;
            userMove.endRow = -1;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Assigning sprites.
            fullChessSet = Content.Load<Texture2D>("Chess Pieces");

            blackKingSprite = Content.Load<Texture2D>("BlackKing");
            blackQueenSprite = Content.Load<Texture2D>("BlackQueen");
            blackRookSprite = Content.Load<Texture2D>("BlackRook");
            blackBishopSprite = Content.Load<Texture2D>("BlackBishop");
            blackKnightSprite = Content.Load<Texture2D>("BlackKnight");
            blackPawnSprite = Content.Load<Texture2D>("BlackPawn");

            whiteKingSprite = Content.Load<Texture2D>("WhiteKing");
            whiteQueenSprite = Content.Load<Texture2D>("WhiteQueen");
            whiteRookSprite = Content.Load<Texture2D>("WhiteRook");
            whiteBishopSprite = Content.Load<Texture2D>("WhiteBishop");
            whiteKnightSprite = Content.Load<Texture2D>("WhiteKnight");
            whitePawnSprite = Content.Load<Texture2D>("WhitePawn");

            //blackElephantSprite = Content.Load<Texture2D>("BlackElephant");
            //whiteElephantSprite = Content.Load<Texture2D>("WhiteElephant");


            rect = new Texture2D(GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Updating some important variables

            HandleMouseCheck();

            // TODO: Update to allow for nonfullscreen play.
            //windWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //windHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(myConst.backgroundColor);

            _spriteBatch.Begin();

            DrawChessBoardFromCenter(960, 540, 800);

            if (heldPiece.piece != 0)
            {
                // DEBUG
                //DrawHeldPieceMoves(myChessBoard.GenerateChessBoardDangerSquares(-6));
                //DrawHeldPieceMoves(myChessBoard.GetChessBoardThreats(heldPiece.column, heldPiece.row));

                //DrawHeldPieceMoves(myChessBoard.GetChessBoardMoves(heldPiece.column, heldPiece.row));
                DrawHeldPieceMoves(myChessBoard.GetMovesIfTurn(heldPiece.column, heldPiece.row));
                DrawAllPiecesWithSkip(heldPiece.column, heldPiece.row);
                DrawHeldPiece(heldPiece.piece);
            }
            else
            {
                DrawAllPieces();
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Calls the draw chess board method but taks coords for the center of the chess board.
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="chessBoardDrawWidthCenter"></param>
        private void DrawChessBoardFromCenter(int centerX, int centerY, int chessBoardDrawWidthCenter)
        {
            DrawChessBoard(centerX - chessBoardDrawWidthCenter / 2, centerY - chessBoardDrawWidthCenter / 2, chessBoardDrawWidthCenter);
        }

        /// <summary>
        /// Draws the chess board given the top left point and the width of the board.
        /// </summary>
        /// <param name="topLeftX"></param>
        /// <param name="topLeftY"></param>
        /// <param name="chessBoardDrawWidth"></param>
        private void DrawChessBoard(int topLeftX, int topLeftY, int chessBoardDrawWidth)
        {
            for (int column = 0; column < 8; column++)
            {
                for (int row = 0; row < 8; row++)
                {
                    Color tempColor;
                    if ((column + 7 * row) % 2 == 1)
                    {
                        tempColor = myConst.darkSquare;
                    }
                    else
                    {
                        tempColor = myConst.lightSquare;
                    }

                    columnPositions[column] = topLeftX + (chessBoardDrawWidth / 8) * column;
                    rowPositions[row] = topLeftY + (chessBoardDrawWidth / 8) * row;
                    chessBoardWidth = chessBoardDrawWidth;

                    Rectangle tempDrawSquare = new Rectangle(columnPositions[column], rowPositions[row], chessBoardDrawWidth / 8, chessBoardDrawWidth / 8);
                    _spriteBatch.Draw(rect, tempDrawSquare, tempColor);
                }
            }

            columnPositions[8] = topLeftX + chessBoardDrawWidth;
            rowPositions[8] = topLeftY + chessBoardDrawWidth;
        }

        /// <summary>
        /// Draws an array of possible moves to the chess board. Blue for open squares and red for captures.
        /// </summary>
        /// <param name="inputArray"></param>
        private void DrawHeldPieceMoves(int[,] inputArray)
        {
            for (int column = 0; column < 8; column++)
            {
                for (int row = 0; row < 8; row++)
                {
                    Color tempColor = new Color(0, 0, 0);
                    //Debug.WriteLine(inputArray[column, row]);
                    if (inputArray[row, column] != 0)
                    {
                        if (inputArray[row, column] == 1 || inputArray[row, column] == 11 || inputArray[row, column] == -11)
                        {
                            tempColor = myConst.freeSquare;
                        }
                        else if (inputArray[row, column] == -1)
                        {
                            tempColor = myConst.takeSquare;
                        }
                        else if (inputArray[row, column] == 2 || inputArray[row, column] == 3 || inputArray[row, column] == 4)
                        {
                            tempColor = myConst.specialMove;
                        }

                        int squareWidthTemp = columnPositions[1] - columnPositions[0];

                        Rectangle tempDrawSquare = new Rectangle(columnPositions[column], rowPositions[row], squareWidthTemp, squareWidthTemp);
                        _spriteBatch.Draw(rect, tempDrawSquare, tempColor * 0.5f);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the pieces on the chess board.
        /// </summary>
        private void DrawAllPieces()
        {
            for (int column = 0; column < 8; column++)
            {
                for (int row = 0; row < 8; row++)
                {
                    DrawPiece(column, row, myChessBoard.GetChessBoardSquare(column, row));
                }
            }
        }

        /// <summary>
        /// Draws the pieces on the chess board, skipping the one that is currently held.
        /// </summary>
        /// <param name="columnSkip"></param>
        /// <param name="rowSkip"></param>
        private void DrawAllPiecesWithSkip(int columnSkip, int rowSkip)
        {
            for (int column = 0; column < 8; column++)
            {
                for (int row = 0; row < 8; row++)
                {
                    if (column != columnSkip || row != rowSkip)
                    {
                        DrawPiece(column, row, myChessBoard.GetChessBoardSquare(column, row));
                    }
                }
            }
        }

        /// <summary>
        /// Draws a piece texture given its column, row, and number. (Uses the identify piece method).
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <param name="piece"></param>
        private void DrawPiece(int column, int row, int piece)
        {
            Texture2D tempBoardPieceTexture = IdentifyPiece(piece);

            if (tempBoardPieceTexture != null)
            {
                _spriteBatch.Draw(tempBoardPieceTexture, new Rectangle(columnPositions[column], rowPositions[row], chessBoardWidth / 8, chessBoardWidth / 8), Color.White);
            }
        }

        /// <summary>
        /// Checks the column that the mouse is currently over.
        /// </summary>
        /// <returns></returns>
        private int CheckMouseColumn()
        {
            for (int column = 0; column <= 7; column++)
            {
                if (mouseState.X > columnPositions[column] && mouseState.X < columnPositions[column + 1])
                {
                    return column;
                }
            }
            return -1;
        }

        /// <summary>
        /// Checks the row that the mouse is currently over.
        /// </summary>
        /// <returns></returns>
        private int CheckMouseRow()
        {
            for (int row = 0; row <= 7; row++)
            {
                if (mouseState.Y > rowPositions[row] && mouseState.Y < rowPositions[row + 1])
                {
                    return row;
                }
            }
            return -1;
        }

        /// <summary>
        /// Takes and input of the piece number and returns the Texture2D object associated with it.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        private Texture2D IdentifyPiece(int piece)
        {
            Texture2D tempTexture = null;
            switch (piece)
            {
                case Const.WHITE_KING: tempTexture = whiteKingSprite; break;
                case Const.WHITE_QUEEN: tempTexture = whiteQueenSprite; break;
                case Const.WHITE_ROOK: tempTexture = whiteRookSprite; break;
                case Const.WHITE_BISHOP: tempTexture = whiteBishopSprite; break;
                case Const.WHITE_KNIGHT: tempTexture = whiteKnightSprite; break;
                case Const.WHITE_PAWN: tempTexture = whitePawnSprite; break;
                case Const.ENP_WHITE_PAWN: tempTexture = whitePawnSprite; break;
                //case Const.WHITE_PAWN: tempTexture = whiteElephantSprite; break;

                //case Const.BLACK_PAWN: tempTexture = blackElephantSprite; break;
                case Const.ENP_BLACK_PAWN: tempTexture = blackPawnSprite; break;
                case Const.BLACK_PAWN: tempTexture = blackPawnSprite; break;
                case Const.BLACK_KNIGHT: tempTexture = blackKnightSprite; break;
                case Const.BLACK_BISHOP: tempTexture = blackBishopSprite; break;
                case Const.BLACK_ROOK: tempTexture = blackRookSprite; break;
                case Const.BLACK_QUEEN: tempTexture = blackQueenSprite; break;
                case Const.BLACK_KING: tempTexture = blackKingSprite; break;


                case Const.C_BLACK_KING: tempTexture = blackKingSprite; break;
                case Const.C_WHITE_KING: tempTexture = whiteKingSprite; break;
                case Const.C_BLACK_ROOK: tempTexture = blackRookSprite; break;
                case Const.C_WHITE_ROOK: tempTexture = whiteRookSprite; break;

                default: tempTexture = null; break;
            }

            return tempTexture;
        }

        /// <summary>
        /// Takes the held piece and uses the mouse location to draw it. (Uses the identify peice method).
        /// </summary>
        /// <param name="piece"></param>
        private void DrawHeldPiece(int piece)
        {
            Texture2D tempHeldPieceTexture = IdentifyPiece(piece);
            int x = mouseState.X - chessBoardWidth / 16;
            int y = mouseState.Y - chessBoardWidth / 16;

            _spriteBatch.Draw(tempHeldPieceTexture, new Rectangle(x, y, chessBoardWidth / 8, chessBoardWidth / 8), Color.White);
        }

        /// <summary>
        /// Handles all of the updating of mouse values.
        /// </summary>
        public void HandleMouseCheck()
        {
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && !mouseLeftPressed)
            {
                mouseLeftPressed = true;

                heldPiece.column = CheckMouseColumn();
                heldPiece.row = CheckMouseRow();
                if (heldPiece.column != -1 && heldPiece.row != -1)
                {
                    heldPiece.piece = myChessBoard.GetChessBoardSquare(heldPiece.column, heldPiece.row);
                }

                userMove.startColumn = heldPiece.column;
                userMove.startRow = heldPiece.row;

                //Debug.Write(CheckMouseColumn() + 1);
                //Debug.Write(", ");
                //Debug.WriteLine(CheckMouseRow() + 1);

                //Debug.WriteLine(heldPiece.piece);
            }
            else if (mouseState.LeftButton == ButtonState.Released && mouseLeftPressed)
            {
                userMove.endColumn = CheckMouseColumn();
                userMove.endRow = CheckMouseRow();

                Debug.WriteLine(userMove.startColumn + " | " + userMove.startRow);
                Debug.WriteLine(userMove.endColumn + " | " + userMove.endRow);

                if (userMove.startColumn != -1 && userMove.startRow != -1 && userMove.endColumn != -1 && userMove.endRow != -1)
                {
                    myChessBoard.AttemptMove(userMove.startColumn, userMove.startRow, userMove.endColumn, userMove.endRow);
                }

                mouseLeftPressed = false;
                heldPiece.column = 0;
                heldPiece.row = 0;
                heldPiece.piece = 0;
            }
        }
    }
}
