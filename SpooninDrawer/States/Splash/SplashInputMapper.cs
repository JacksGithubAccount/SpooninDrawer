﻿using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Input.Base;
using SpooninDrawer.Input;
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
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<SplashInputCommand>();

            if (state.IsKeyDown(Keys.Enter))
            {
                commands.Add(new SplashInputCommand.GameSelect());
            }

            return commands;
        }
    }
}
