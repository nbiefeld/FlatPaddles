using System;
using Microsoft.Xna.Framework;
using PGL2D.GameObject;

namespace FlatPaddlesTutorial.Entities
{
    public class EnemyPaddle : MoveableEntity, IPaddle
    {
        private readonly Ball _ball;
        private float _ballTolerance;

        public EnemyPaddle(Color color, Vector2 position, float angle, float speed, string textureName, float rotation, float scale, Ball ball, Rectangle bounds)
            : base(color, position, angle, speed, textureName, rotation, scale, bounds)
        {
            _ball = ball;
            _ballTolerance = 0;

            _ball.ContentLoaded += _ball_ContentLoaded;

            PaddleControl = PaddleControl.Computer;
        }

        public PaddleControl PaddleControl { get; private set; }

        private void _ball_ContentLoaded(object sender, EventArgs e)
        {
            _ballTolerance = _ball.Texture.Width;
        }

        protected override void UpdateEntity(GameTime gameTime)
        {
            if (Math.Abs(_ball.Position.Y - Position.Y) < _ballTolerance) return;

            var angle = _ball.Position.Y - Position.Y > 0 ? MathHelper.PiOver2 : 3.0f * MathHelper.PiOver2;

            Speed = MaxSpeed;
            ChangeDirection(angle);

            base.UpdateEntity(gameTime);
        }
    }
}
