using Microsoft.Xna.Framework.Input;
using SpooninDrawer.States.Base;
using SpooninDrawer.Objects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.States
{
    public class TitleState : BaseGameState
    {
        public override void LoadContent()
        {
            AddGameObject(new TitleImage(LoadTexture("Menu/titleScreen")));
        }

        public override void HandleInput()
        {
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Enter))
            {
                SwitchState(new GameplayState());
            }
        }
    }
}
