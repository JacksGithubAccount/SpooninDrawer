using SpooninDrawer.Engine.Input.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.States.Gameplay
{
    public class GameplayInputCommand : BaseInputCommand
    {
        public class GameExit : GameplayInputCommand { }
        public class PlayerMoveLeft : GameplayInputCommand { }
        public class PlayerMoveRight : GameplayInputCommand { }
        public class PlayerStopsMoving : GameplayInputCommand { }
        public class PlayerAction : GameplayInputCommand { }
    }
}
