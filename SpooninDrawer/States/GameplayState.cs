using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Enum;
using SpooninDrawer.States.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.States
{
    public class GameplayState : BaseGameState
    {
        //private const string PlayerFighter = "fighter";

        //private const string BackgroundTexture = "Barren";

        public override void LoadContent()
        {
            //AddGameObject(new SplashImage(LoadTexture(BackgroundTexture)));
            //AddGameObject(new PlayerSprite(LoadTexture(PlayerFighter)));
        }

        public override void HandleInput()
        {
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape))
            {
                NotifyEvent(Events.GAME_QUIT);
            }
        }
    }
}
