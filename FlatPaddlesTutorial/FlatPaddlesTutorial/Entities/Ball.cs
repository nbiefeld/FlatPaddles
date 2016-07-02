using System;
using Microsoft.Xna.Framework;
using PGL2D.Collision;
using PGL2D.GameObject;
using PGL2D.Utility;

namespace FlatPaddlesTutorial.Entities
{
    public class Ball : MoveableEntity, ICollidable<MoveableEntity>
    {
        private readonly float _initialSpeed;
        private readonly Vector2 _initialPosition;
        private readonly float _deltaSpeed;
        private readonly Random _random;

        public Ball(Color color, Vector2 position, float angle, float speed, float maxSpeed, float deltaSpeed, string textureName)
            : base(color, position, angle, maxSpeed, textureName)
        {
            _initialSpeed = speed;
            _initialPosition = position;
            _deltaSpeed = deltaSpeed;
            _random = new Random(Guid.NewGuid().GetHashCode());
        }

        public bool OutOfBounds { get; private set; }
        public RectangleCollisionPoint LastCollisionPoint { get; set; }

        public void Reflect(Vector2 normal)
        {
            var reflectedVector = Velocity.Reflect(normal);

            var reflectedAngle = (float)Math.Atan2(reflectedVector.Y, reflectedVector.X);

            ChangeDirection(reflectedAngle);
            SpeedUp(_deltaSpeed);
        }

        public bool CheckCollision(MoveableEntity entity)
        {
            Vector2 normal;
            if (!Collision.RectangularCollision(this, entity, out normal)) return false;

            Reflect(normal);
            return true;
        }

        public override void HitBounds(Vector2 change, Vector2 normal, RectangleCollisionPoint collisionPoint)
        {
            LastCollisionPoint = collisionPoint;

            if(collisionPoint == RectangleCollisionPoint.Top || collisionPoint == RectangleCollisionPoint.Bottom)
            {
                Reflect(normal);
            }
            else
            {
                Stop();
                OutOfBounds = true;
            }

            base.HitBounds(change, normal, collisionPoint);
        }

        public void Serve()
        {
            Position = _initialPosition;
            Speed = _initialSpeed;

            var multiplier = _random.NextOdd(1, 7);
            var angle = multiplier * MathHelper.PiOver4;

            ChangeDirection(angle);

            OutOfBounds = false;
        }
    }
}
