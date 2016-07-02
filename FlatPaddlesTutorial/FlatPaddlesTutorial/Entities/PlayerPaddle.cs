using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PGL2D.GameObject;
using PGL2D.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FlatPaddlesTutorial.Entities
{
    public class PlayerPaddle : ControllableGameEntity, IPaddle
    {
        private const string MoveUp = "Move Up";
        private const string MoveDown = "Move Down";

        public PlayerPaddle(InputSystem inputSystem, Color color, Vector2 position, float angle, float maxSpeed, string textureName)
            : base(inputSystem, color, position, angle, maxSpeed, textureName)
        {
            InputMap.NewAction(MoveUp, Keys.Up);
            InputMap.NewAction(MoveDown, Keys.Down);

            PaddleControl = PaddleControl.Player;

            Speed = maxSpeed;
        }

        public PaddleControl PaddleControl { get; private set; }

        public override void HandleInput(List<Keybind> availableKeybinds, GameTime gameTime)
        {
            var moveUp = availableKeybinds.FirstOrDefault(k => k.Action.Equals(MoveUp));
            var moveDown = availableKeybinds.FirstOrDefault(k => k.Action.Equals(MoveDown));

            var canMoveUp = moveUp != null && moveUp.CheckMode(InputMode.Held);
            var canMoveDown = moveDown != null && moveDown.CheckMode(InputMode.Held);

            if((!canMoveUp && !canMoveDown) || (canMoveUp && canMoveDown))
            {
                Stop();
                return;
            }

            Start();
            ChangeDirection(canMoveUp ? 3.0f * MathHelper.PiOver2 : MathHelper.PiOver2);
        }
    }
}
