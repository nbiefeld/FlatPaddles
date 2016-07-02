using System;
using System.Collections.Generic;
using System.Linq;
using FlatPaddlesTutorial.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PGL2D.Collision;
using PGL2D.Game;
using PGL2D.GameObject;

namespace FlatPaddlesTutorial.Screens
{
    public class GameScreen : PGL2D.ScreenSystem.GameScreen
    {
        private Ball _ball;
        private readonly uint _awardedPoints;
        private Participant _player, _computer;
        private TextEntity _playerScore, _computerScore, _timer;

        public GameScreen(BaseGame game, bool acceptsInput, bool isFrozen = false, bool isHidden = false)
            : base(game, acceptsInput, isFrozen, isHidden)
        {
            _awardedPoints = 1;
        }

        public override void Initialize()
        {
            InitializeGameEntities();
            InitializeTextEntities();

            _ball.Serve();

            base.Initialize();
        }

        public void InitializeGameEntities()
        {
            _ball = new Ball(Color.White, Game.AbsoluteCenter, 0, 150, 650, 25, "ball");
            _ball.BindEntity(Game.Dimensions);

            var playerPaddle = new PlayerPaddle(Game.InputSystem, Color.White, new Vector2(100, 200), 0, 300, "paddle");
            playerPaddle.BindEntity(new Rectangle(0, 0, Game.Dimensions.Width, Game.Dimensions.Height));

            var enemyPaddle = new EnemyPaddle(Color.White, new Vector2(Game.Dimensions.Right - 100, 300), (float)Math.PI, 200, "paddle", MathHelper.Pi, 1.0f,
                _ball, Game.Dimensions);

            _player = new Participant(playerPaddle);
            _computer = new Participant(enemyPaddle);

            Entities.Add(_ball);
            Entities.Add(playerPaddle);
            Entities.Add(enemyPaddle);
        }

        public void InitializeTextEntities()
        {
            _playerScore = new TextEntity("gametext", _player.Points.ToString(), Color.White);
            _playerScore.ContentLoaded += AdjustPlayerTextPosition;
            _playerScore.TextUpdated += AdjustPlayerTextPosition;

            _computerScore = new TextEntity("gametext", _computer.Points.ToString(), Color.White);
            _computerScore.ContentLoaded += AdjustComputerTextPosition;
            _computerScore.TextUpdated += AdjustComputerTextPosition;

            _timer = new TextEntity("gametext", TimeSpan.Zero.ToString(), Color.White);
            _timer.ContentLoaded += AdjustTimerPosition;
            _timer.TextUpdated += AdjustTimerPosition;

            TextEntities.Add(_playerScore);
            TextEntities.Add(_computerScore);
            TextEntities.Add(_timer);
        }

        private void AdjustPlayerTextPosition(object sender, EventArgs args)
        {
            _playerScore.Move(new Vector2(_playerScore.Origin.X, _playerScore.Origin.Y));
        }

        private void AdjustComputerTextPosition(object sender, EventArgs args)
        {
            _computerScore.Move(new Vector2(Game.Dimensions.Right - _computerScore.Origin.X, _computerScore.Origin.Y));
        }

        private void AdjustTimerPosition(object sender, EventArgs args)
        {
            _timer.Move(new Vector2((Game.Dimensions.Width - _timer.Origin.X) / 2.0f, _timer.Origin.Y));
        }

        protected override void UpdateScreen(GameTime gameTime, List<GameEntity> updateableEntities, 
            List<TextEntity> updateableTextEntities)
        {
            _timer.UpdateText(gameTime.TotalGameTime.ToString("mm':'ss"));

            foreach (var entity in updateableEntities)
            {
                entity.Update(gameTime);
            }

            foreach (var entity in updateableEntities.OfType<IPaddle>().OfType<MoveableEntity>())
            {
                _ball.CheckCollision(entity);
            }

            if (!_ball.OutOfBounds) return;

            AwardPoints();
            _ball.Serve();
        }

        private void AwardPoints()
        {
            switch (_ball.LastCollisionPoint.GetCollisionSide())
            {
                case RectangleCollisionSide.Left:
                    _computer.Score(_awardedPoints);
                    _computerScore.UpdateText(_computer.Points.ToString());
                    break;
                case RectangleCollisionSide.Right:
                    _player.Score(_awardedPoints);
                    _playerScore.UpdateText(_player.Points.ToString());
                    break;
            }
        }

        protected override void DrawScreen(GameTime gameTime, SpriteBatch spriteBatch,
            List<GameEntity> drawableEntities, List<TextEntity> drawableTextEntities)
        {
            foreach (var entity in drawableEntities)
            {
                entity.Draw(gameTime, Game.SpriteBatch);
            }

            foreach (var textEntity in drawableTextEntities)
            {
                textEntity.Draw(gameTime, Game.SpriteBatch);
            }
        }
    }
}
