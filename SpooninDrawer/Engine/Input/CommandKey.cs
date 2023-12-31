using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.States.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SpooninDrawer.Engine.Input
{
     class CommandKey
    {
        public Keys key;
        public BaseInputCommand command;
        public InputType type;

        public CommandKey(Keys inputKey, BaseInputCommand command)
        {
            key = inputKey;
            this.command = command;
        }
        public CommandKey(CommandKey commandKey)
        {
            key = commandKey.key;
            command = commandKey.command;
            type = InputType.NoInput;
        }
        public CommandKey(CommandKey commandKey, InputType inputType)
        {
            key = commandKey.key;
            command = commandKey.command;
            type = inputType;
        }
        public CommandKey(Keys inputKey, BaseInputCommand command, InputType inputType)
        {
            key = inputKey;
            this.command = command;
            type = inputType;
        }
        public void SetKeyAction(Keys inputKey, BaseInputCommand command)
        {
            key = inputKey;
            this.command = command;
        }
        public void SetKey(Keys inputKey)
        {
            key = inputKey;
        }
    }
}
