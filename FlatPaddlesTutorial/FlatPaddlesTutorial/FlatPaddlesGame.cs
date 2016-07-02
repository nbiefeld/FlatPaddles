using FlatPaddlesTutorial.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PGL2D.Game;

namespace FlatPaddlesTutorial
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class FlatPaddlesGame : BaseGame
    {
        public FlatPaddlesGame(int width, int height, string contentRootDirectory, 
            Color backgroundColor) : base(width, height, contentRootDirectory, backgroundColor)
        {
            ScreenSystem.AddScreen(new GameScreen(this, true));
        }
    }
}
