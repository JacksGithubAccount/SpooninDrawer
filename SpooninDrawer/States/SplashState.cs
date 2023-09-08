using Microsoft.Xna.Framework.Input;
using SpooninDrawer.States.Base;
using SpooninDrawer.Objects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.Input.Base;
using SpooninDrawer.Input;

namespace SpooninDrawer.States
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

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper());
        }
    }
}
