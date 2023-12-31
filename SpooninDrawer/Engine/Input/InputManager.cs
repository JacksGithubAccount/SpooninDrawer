using Microsoft.Xna.Framework.Input;
using SpooninDrawer.States.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Input
{
    public enum InputType
    {
        NoInput,
        Press,
        Release,
        Hold
    }
    public class InputManager
    {
        private readonly BaseInputMapper _inputMapper;
        private KeyboardState oldKeyboardState;
        private List<CommandKey> controls;
        private List<CommandKey> commandKeys;
        private static List<BaseInputCommand> commands;
        public InputManager(BaseInputMapper inputMapper)
        {
            _inputMapper = inputMapper;
            oldKeyboardState = Keyboard.GetState();
            //contains the controls for  player input
            controls = new List<CommandKey>();
            //player input is added to this list which is then checked when an action is called and removed when player is no longer inputting that input
            commandKeys = new List<CommandKey>();
            commands = new List<BaseInputCommand>();

            commands.Add(new GameplayInputCommand.PlayerMoveLeft());
            commands.Add(new GameplayInputCommand.PlayerMoveRight());
            commands.Add(new GameplayInputCommand.PlayerMoveUp());
            commands.Add(new GameplayInputCommand.PlayerMoveDown());
            commands.Add(new GameplayInputCommand.PlayerAction());
            commands.Add(new GameplayInputCommand.PlayerConfirm());
            commands.Add(new GameplayInputCommand.PlayerCancel());
            commands.Add(new GameplayInputCommand.PlayerInteract());
            commands.Add(new GameplayInputCommand.PlayerOpenMenu());
            commands.Add(new GameplayInputCommand.Pause());

            //instantiate default control scheme
            controls.Add(new CommandKey(Keys.Left, commands[0]));
            controls.Add(new CommandKey(Keys.Right, commands[1]));
            controls.Add(new CommandKey(Keys.Up, commands[2]));
            controls.Add(new CommandKey(Keys.Down, commands[3]));
            controls.Add(new CommandKey(Keys.Z, commands[4]));
            controls.Add(new CommandKey(Keys.Z, commands[5]));
            controls.Add(new CommandKey(Keys.X, commands[6]));
            controls.Add(new CommandKey(Keys.Z, commands[7]));
            controls.Add(new CommandKey(Keys.C, commands[8]));
            controls.Add(new CommandKey(Keys.P, commands[9]));
        }

        public BaseInputCommand DoesKeyExistinControls(Keys keyToCheck, BaseInputCommand actionToRemap)
        {
            BaseInputCommand crossAction = new GameplayInputCommand.PlayerNoInput();
            if (controls.Exists(x => x.key == keyToCheck))
            {
                List<CommandKey> checkAKey = controls.FindAll(x => x.key == keyToCheck);
                if (checkAKey.Count >= 1)
                {
                    if (checkAKey.Exists(x => x.command == commands[6]) && actionToRemap == commands[7])
                    {
                        return commands[6];
                    }
                    else if (checkAKey.Exists(x => x.command == commands[7]) && actionToRemap == commands[6])
                    {
                        return commands[7];
                    }
                    else
                    {
                        checkAKey.RemoveAll(x => x.command == commands[7] && x.command == commands[6]);
                        if (checkAKey.Count >= 1)
                        {
                            crossAction = checkAKey[0].command;
                        }
                    }
                }
                return crossAction;
            }
            else
                return actionToRemap;
        }
        public bool IsAnyButtonInputTyped(InputType inputType)
        {
            return commandKeys.Exists(x => x.type == inputType);
        }
        public void Remap(Keys remappedKey, BaseInputCommand BaseInputCommand)
        {
            controls.Find(x => x.command == BaseInputCommand).SetKey(remappedKey);
        }
        public Keys GetKeyforCommand(BaseInputCommand selectedCommand)
        {
            return controls.Find(x => x.command == selectedCommand).key;
        }
        public bool IsActionPressed(BaseInputCommand selectedCommand)
        {
            if (commandKeys.Exists(x => x.command == selectedCommand))
            {
                return true;
            }
            else
                return false;
        }
        public bool isCommandinputtedbyType(BaseInputCommand selectedCommand, InputType inputType)
        {
            CommandKey commandCheck = new CommandKey(controls.Find(x => x.command == selectedCommand));
            commandCheck.type = inputType;
            if (commandKeys.Exists(x => x.command == selectedCommand && x.type == inputType))
            {
                return true;
            }
            else
                return false;
        }

        public void resetKeystoDefault()
        {
            Remap(Keys.Left, commands[0]);
            Remap(Keys.Right, commands[1]);
            Remap(Keys.Up, commands[3]);
            Remap(Keys.Down, commands[4]);
            Remap(Keys.Z, commands[5]);
            Remap(Keys.Z, commands[6]);
            Remap(Keys.X, commands[7]);
            Remap(Keys.Z, commands[8]);
            Remap(Keys.C, commands[9]);
            Remap(Keys.P, commands[10]);
        }

        public void PressButton(KeyboardState keyState, BaseInputCommand command)
        {
            Keys checkKey = controls.Find(x => x.command == command).key;
            CommandKey tempCommandKey = new CommandKey(controls.Find(x => x.command == command));

            if (keyState.IsKeyDown(checkKey) && oldKeyboardState.IsKeyDown(checkKey))
            {
                tempCommandKey.type = InputType.Hold;
                if (!commandKeys.Exists(x => x.command == command && x.type == InputType.Hold))
                {
                    commandKeys.Add(tempCommandKey);
                }
            }
            else
            {
                if (commandKeys.Exists(x => x.command == command && x.type == InputType.Hold))
                {
                    commandKeys.RemoveAll(x => x.command == command && x.type == InputType.Hold);
                }
            }
            if (keyState.IsKeyDown(checkKey) && oldKeyboardState.IsKeyUp(checkKey))
            {
                tempCommandKey.type = InputType.Press;
                if (!commandKeys.Contains(tempCommandKey))
                {
                    commandKeys.Add(tempCommandKey);
                }
            }
            else
            {
                if (commandKeys.Exists(x => x.command == command && x.type == InputType.Press))
                {
                    commandKeys.RemoveAll(x => x.command == command && x.type == InputType.Press);
                }
            }
            if (keyState.IsKeyUp(checkKey) && oldKeyboardState.IsKeyDown(checkKey))
            {
                tempCommandKey.type = InputType.Release;
                if (!commandKeys.Exists(x => x.command == command && x.type == InputType.Release))
                {
                    commandKeys.Add(tempCommandKey);
                }
            }
            else
            {
                if (commandKeys.Exists(x => x.command == command && x.type == InputType.Release))
                {
                    commandKeys.RemoveAll(x => x.command == command && x.type == InputType.Release);
                }
            }

        }
        public void SetOldKeyboardState(KeyboardState keyboardState) 
        {
            oldKeyboardState = keyboardState;
        }
        
        public void GetCommands(Action<BaseInputCommand> actOnState)
        {
            var keyboardState = Keyboard.GetState();
            foreach (var state in _inputMapper.GetKeyboardState(keyboardState))
            {
                actOnState(state);
            }

            var mouseState = Mouse.GetState();
            foreach (var state in _inputMapper.GetMouseState(mouseState))
            {
                actOnState(state);
            }

            // we're going to assume only 1 gamepad is being used
            var gamePadState = GamePad.GetState(0);
            foreach (var state in _inputMapper.GetGamePadState(gamePadState))
            {
                actOnState(state);
            }
        }

        public void update(KeyboardState keyState)
        {
            for (int c = 0; c < controls.Count; c++)
            {
                PressButton(keyState, controls[c].command);
            }

            oldKeyboardState = keyState;
        }
    }
}
