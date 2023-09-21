using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input.Base;
using SpooninDrawer.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.States.Dev;

namespace SpooninDrawer.States.Gameplay
{
    public class GameplayInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<GameplayInputCommand>();

            if (state.IsKeyDown(Keys.Escape))
            {
                commands.Add(new GameplayInputCommand.GameExit());
            }

            if (state.IsKeyDown(Keys.Space))
            {
                commands.Add(new GameplayInputCommand.PlayerAction());
            }

            if (state.IsKeyDown(Keys.Right))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveRight());
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveLeft());
            }
            if (state.IsKeyDown(Keys.Up))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveUp());
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveDown());
            }
            if (state.IsKeyUp(Keys.Right) && state.IsKeyUp(Keys.Left) && state.IsKeyUp(Keys.Up) && state.IsKeyUp(Keys.Down))
            {
                commands.Add(new GameplayInputCommand.PlayerStopsMoving());
            }

            return commands;
        }
    }
}
