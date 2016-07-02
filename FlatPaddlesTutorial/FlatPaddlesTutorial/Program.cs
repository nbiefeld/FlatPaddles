using System;
using Microsoft.Xna.Framework;

namespace FlatPaddlesTutorial
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new FlatPaddlesGame(1280, 720, "Content", new Color(0, 100, 100)))
                game.Run();
        }
    }
#endif
}
