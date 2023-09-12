using SpooninDrawer.Engine.Input.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.States.Dev
{
    public class DevInputCommand : BaseInputCommand
    {
        // Out of Game Commands
        public class DevQuit : DevInputCommand { }
        public class DevLeft : DevInputCommand { }
        public class DevRight : DevInputCommand { }
        public class DevNotMoving : DevInputCommand { }
    }
}
