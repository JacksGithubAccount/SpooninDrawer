using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input.Base;
using SpooninDrawer.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            else
            {
                commands.Add(new GameplayInputCommand.PlayerStopsMoving());
            }

            return commands;
        }
    }
}
