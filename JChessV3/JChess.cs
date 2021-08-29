using Microsoft.Xna.Framework;
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

        Texture2D rect;
        #endregion

        #region Other Variables
        // Creating some variables to handle important information
        //int windWidth;
        //int windHeight;

        int[] columnPositions = { 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] rowPositions = { 0, 0, 0, 0, 0, 0, 0, 0 };
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
            _graphics.IsFullScreen = false;

            _graphics.ApplyChanges();

            myConst = new Const();

            myChessBoard = new ChessBoardLogic();
            myChessBoard.chessBoardReset();

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

            rect = new Texture2D(GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Updating some important variables

            // TODO: Update to allow for nonfullscreen play.
            //windWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //windHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            // Debug


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(myConst.backgroundColor);

            _spriteBatch.Begin();

            drawChessBoardFromCenter(960, 540, 800);
            drawAllPieces();

            _spriteBatch.End();

            // DEBUG
            //Debug.WriteLine(windWidth);

            base.Draw(gameTime);
        }

        private void drawChessBoardFromCenter(int centerX, int centerY, int chessBoardDrawWidthCenter)
        {
            drawChessBoard(centerX - chessBoardDrawWidthCenter / 2, centerY - chessBoardDrawWidthCenter / 2, chessBoardDrawWidthCenter);
        }

        private void drawChessBoard(int topLeftX, int topLeftY, int chessBoardDrawWidth)
        {
            for (int column = 0; column < 8; column++)
            {
                for (int row = 0; row < 8; row++)
                {
                    Color tempColor;
                    if((column + 7 * row) % 2 == 1)
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
        }

        private void drawAllPieces()
        {
            for(int column = 0; column < 8; column++)
            {
                for(int row = 0; row < 8; row++)
                {
                    drawPiece(column, row, myChessBoard.getChessBoardArray(column, row));
                }
            }
        }

        private void drawPiece(int column, int row, int piece)
        {
            Texture2D tempTexture = rect;
            switch (piece)
            {
                case Const.WHITE_KING: tempTexture = whiteKingSprite; break; 
                case Const.WHITE_QUEEN: tempTexture = whiteQueenSprite; break;
                case Const.WHITE_ROOK: tempTexture = whiteRookSprite; break;
                case Const.WHITE_BISHOP: tempTexture = whiteBishopSprite; break;
                case Const.WHITE_KNIGHT: tempTexture = whiteKnightSprite; break;
                case Const.WHITE_PAWN: tempTexture = whitePawnSprite; break;

                case Const.BLACK_PAWN: tempTexture = blackPawnSprite; break;
                case Const.BLACK_KNIGHT: tempTexture = blackKnightSprite; break;
                case Const.BLACK_BISHOP: tempTexture = blackBishopSprite; break;
                case Const.BLACK_ROOK: tempTexture = blackRookSprite; break;
                case Const.BLACK_QUEEN: tempTexture = blackQueenSprite; break;
                case Const.BLACK_KING: tempTexture = blackKingSprite; break;

                default: tempTexture = null; break;
            }

            if (tempTexture != null)
            {
                _spriteBatch.Draw(tempTexture, new Rectangle(columnPositions[column], rowPositions[row], chessBoardWidth / 8, chessBoardWidth / 8), Color.White);
            }
        }
    }
}
