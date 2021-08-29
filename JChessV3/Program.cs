using System;

namespace JChessV3
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new JChess())
                game.Run();
        }
    }
}
