using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input.Base;
using SpooninDrawer.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.States.Dev;
using SpooninDrawer.Engine.States.Gameplay;
using SharpDX.XInput;

namespace SpooninDrawer.States.Gameplay
{
    public class GameplayInputMapper : BaseInputMapper
    {
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;


        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState keyState)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = keyState;

            var commands = new List<GameplayInputCommand>();

            if (keyState.IsKeyDown(Keys.Escape))
            {
                commands.Add(new GameplayInputCommand.GameExit());
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                commands.Add(new GameplayInputCommand.PlayerAction());
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveRight());
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveLeft());
            }
            if (keyState.IsKeyDown(Keys.Up))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveUp());
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveDown());
            }
            if (keyState.IsKeyUp(Keys.Right) && keyState.IsKeyUp(Keys.Left) && keyState.IsKeyUp(Keys.Up) && keyState.IsKeyUp(Keys.Down))
            {
                commands.Add(new GameplayInputCommand.PlayerStopsMoving());
            }
            if (keyState.IsKeyDown(Keys.P) && IsKeyPressedOnce(Keys.P))
            {
                commands.Add(new GameplayInputCommand.Pause());
            }
            return commands;
        }
        private bool isKeyHold(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key);
        }
        private bool IsKeyPressedOnce(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }
        private bool IsKeyReleased(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
        }
    }
}
