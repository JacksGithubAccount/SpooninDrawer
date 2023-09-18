using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Input.Base;
using SpooninDrawer.Engine.States;
using SpooninDrawer.Objects.Text;
using SpooninDrawer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.States.Dev
{
    /// <summary>
    /// Used to test out new things, like particle engines
    /// </summary>
    public class DevState : BaseGameState
    {
        private const string playerSpriteSheet = "Sprites/Animations/FighterSpriteSheet";
        private PlayerSprite _player;

        public override void LoadContent()
        {
            _player = new PlayerSprite(LoadTexture(playerSpriteSheet));
            _player.Position = new Vector2(200, 400);
            AddGameObject(_player);

            var font = LoadFont("Fonts/GameOver");
            var gameOverText = new GameOverText(font);
            var textPosition = new Vector2(460, 300);

            gameOverText.Position = textPosition;
            AddGameObject(gameOverText);
        }

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is DevInputCommand.DevQuit)
                {
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                }

                if (cmd is DevInputCommand.DevLeft)
                {
                    _player.MoveLeft();
                }

                if (cmd is DevInputCommand.DevRight)
                {
                    _player.MoveRight();
                }

                if (cmd is DevInputCommand.DevUp)
                {
                    _player.MoveUp();
                }

                if (cmd is DevInputCommand.DevDown)
                {
                    _player.MoveDown();
                }

                if (cmd is DevInputCommand.DevNotMoving)
                {
                    _player.StopMoving();
                }
            });
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            _player.Update(gameTime);
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new DevInputMapper());
        }
    }
}
