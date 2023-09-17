
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.Engine.Input.Base;

namespace SpooninDrawer.States.Splash
{
    public class SplashInputCommand : BaseInputCommand
    {
        // Out of Game Commands
        public class GameSelect : SplashInputCommand { }
    }
}
