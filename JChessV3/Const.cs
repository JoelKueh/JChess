using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JChessV3
{
    class Const
    {
        public Const() { }

        #region Colors
        // Defining some colors

        public Color backgroundColor = new Color(33, 33, 33);
        public Color lightSquare = new Color(150, 110, 80);
        public Color darkSquare = new Color(100, 70, 40);
        public Color freeSquare = new Color(91, 146, 229);
        public Color takeSquare = new Color(220, 20, 60);
        #endregion

        #region Piece Shortcut
        // Constant ints for shortcuts to pieces

        public const int WHITE_KING = 6;
        public const int WHITE_QUEEN = 5;
        public const int WHITE_ROOK = 4;
        public const int WHITE_BISHOP = 3;
        public const int WHITE_KNIGHT = 2;
        public const int WHITE_PAWN = 1;
        public const int BLACK_PAWN = -1;
        public const int BLACK_KNIGHT = -2;
        public const int BLACK_BISHOP = -3;
        public const int BLACK_ROOK = -4;
        public const int BLACK_QUEEN = -5;
        public const int BLACK_KING = -6;
        #endregion
    }
}
