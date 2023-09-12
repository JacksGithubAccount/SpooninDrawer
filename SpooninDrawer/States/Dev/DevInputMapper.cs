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
            else
            { 
                commands.Add(new DevInputCommand.DevNotMoving());
            }


            return commands;
        }
    }
}
