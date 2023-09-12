using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Objects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.Engine.States;
using SpooninDrawer.Input;
using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Input.Base;
using SpooninDrawer.Engine.States.Gameplay;



namespace SpooninDrawer.States.Splash
{
    public class SplashState : BaseGameState
    {
        public override void LoadContent()
        {
            AddGameObject(new SplashImage(LoadTexture("Menu/TitleScreen")));
            AddGameObject(new SplashImage(LoadTexture("Menu/TitleScreenArroww")));
        }

        public override void HandleInput(Microsoft.Xna.Framework.GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is SplashInputCommand.GameSelect)
                {
                    SwitchState(new GameplayState());
                }
            });
        }

        public override void UpdateGameState(GameTime _) { }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper());
        }
    }
}
