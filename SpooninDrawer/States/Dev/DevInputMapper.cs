using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.States.Dev
{
    public class DevInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<DevInputCommand>();

            if (state.IsKeyDown(Keys.Escape))
            {
                commands.Add(new DevInputCommand.DevQuit());
            }
            if (state.IsKeyDown(Keys.Right))
            {
                commands.Add(new DevInputCommand.DevRight());
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                commands.Add(new DevInputCommand.DevLeft());
            }
            if (state.IsKeyDown(Keys.Up))
            {
                commands.Add(new DevInputCommand.DevUp());
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                commands.Add(new DevInputCommand.DevDown());
            }
            if (state.IsKeyUp(Keys.Right) && state.IsKeyUp(Keys.Left) && state.IsKeyUp(Keys.Up) && state.IsKeyUp(Keys.Down))
            { 
                commands.Add(new DevInputCommand.DevNotMoving());
            }


            return commands;
        }
    }
}
