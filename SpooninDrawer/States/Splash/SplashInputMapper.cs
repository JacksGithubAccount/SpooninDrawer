using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.Engine.Input.Base;

namespace SpooninDrawer.States.Splash
{
    public class SplashInputMapper : BaseInputMapper
    {
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = state;
            var commands = new List<SplashInputCommand>();

            if (state.IsKeyDown(Keys.Enter))
            {
                commands.Add(new SplashInputCommand.GameSelect());
            }
            if (state.IsKeyDown(Keys.Up) && HasBeenPressed(Keys.Up))
            {
                commands.Add(new SplashInputCommand.MenuMoveUp());
            }
            if (state.IsKeyDown(Keys.Down) && HasBeenPressed(Keys.Down))
            {
                commands.Add(new SplashInputCommand.MenuMoveDown());
            }
            return commands;
        }   
        private bool isKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }
        private bool IsKeyTriggered(Keys key)
        {
            return (currentKeyboardState.IsKeyDown(key)) &&
                (!previousKeyboardState.IsKeyDown(key));
        }
        private  bool HasBeenPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }
    }
}
