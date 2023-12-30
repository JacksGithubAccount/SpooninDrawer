using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Input.Base
{
    public class InputManager
    {
        private readonly BaseInputMapper _inputMapper;
        private KeyboardState oldKeyboardState;
        public InputManager(BaseInputMapper inputMapper)
        {
            _inputMapper = inputMapper;
            oldKeyboardState = Keyboard.GetState();
        }
        public void setOldKeyboardState(KeyboardState keyboardState) 
        {
            oldKeyboardState = keyboardState;
        }
        public void GetCommands(Action<BaseInputCommand> actOnState)
        {
            var keyboardState = Keyboard.GetState();
            foreach (var state in _inputMapper.GetKeyboardState(keyboardState))
            {
                actOnState(state);
            }

            var mouseState = Mouse.GetState();
            foreach (var state in _inputMapper.GetMouseState(mouseState))
            {
                actOnState(state);
            }

            // we're going to assume only 1 gamepad is being used
            var gamePadState = GamePad.GetState(0);
            foreach (var state in _inputMapper.GetGamePadState(gamePadState))
            {
                actOnState(state);
            }
        }
    }
}
