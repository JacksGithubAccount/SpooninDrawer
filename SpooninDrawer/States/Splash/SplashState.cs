using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Objects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.Engine.States;
using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Input.Base;
using SpooninDrawer.Engine.States.Gameplay;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Screens;

namespace SpooninDrawer.States.Splash
{
    public class SplashState : BaseGameState
    {

        private string screenTexture;
        private const string titleScreenArrow = "Menu/TitleScreenArroww";
        private MenuArrowSprite _menuArrow;
        private int[] menuLocationArrayX;
        private int[] menuLocationArrayY;
        private int menuNavigatorX = 0;
        private int menuNavigatorY = 0;
        private int menuNavigatorXCap;
        private int menuNavigatorYCap;
        BaseScreen currentScreen;
        BaseScreen previousScreen;

        public override void LoadContent()
        {
            ChangeScreen(new TitleScreen());
            _menuArrow = new MenuArrowSprite(LoadTexture(titleScreenArrow));
            AddGameObject(_menuArrow);

            _menuArrow.Position = new Vector2(menuLocationArrayX[0], menuLocationArrayY[0]);
        }

        public void ChangeScreen(BaseScreen screen)
        {
            previousScreen = currentScreen ?? new EmptyScreen();
            currentScreen = screen;
            this.screenTexture = screen.screenTexture;
            this.menuLocationArrayX = screen.menuLocationArrayX;
            this.menuLocationArrayY = screen.menuLocationArrayY;
            this.menuNavigatorXCap = screen.menuNavigatorXCap;
            this.menuNavigatorYCap = screen.menuNavigatorYCap;
            SplashImage currentSplash = new SplashImage(LoadTexture(screenTexture));
            BaseGameObject holder = getScreenExist(currentSplash.getTextureName());
            if (holder != null) 
            {
                currentSplash.Activate();
            }
            else
            {
                AddGameObject(currentSplash);
                currentSplash.Activate();
            }
            BaseGameObject previousholder = getScreenExist(previousScreen.screenTexture);
            //SplashImage previousSplash = new SplashImage(LoadTexture(previousScreen.screenTexture));
            //RemoveGameObject(previousSplash);
            previousholder?.Deactivate();
        }
        public override void HandleInput(Microsoft.Xna.Framework.GameTime gameTime)
        {

            _menuArrow.Position = new Vector2(menuLocationArrayX[menuNavigatorX], menuLocationArrayY[menuNavigatorY]);

            InputManager.GetCommands(cmd =>
            {

                if (cmd is SplashInputCommand.GameSelect)
                {
                    SwitchState(new GameplayState());
                }
                if (cmd is SplashInputCommand.SettingsSelect) 
                {
                    ChangeScreen(new SettingsScreen());
                }
                if (cmd is SplashInputCommand.BackSelect) 
                {
                    if (previousScreen != null)
                    {
                        ChangeScreen(previousScreen);
                    }
                    else
                    {
                        menuNavigatorY = 4;
                    }
                }
                if (cmd is SplashInputCommand.ExitSelect)
                {
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                }
                if (cmd is SplashInputCommand.MenuMoveUp)
                {
                    menuNavigatorY--;
                }
                if (cmd is SplashInputCommand.MenuMoveDown)
                {
                    menuNavigatorY++;
                }
                KeepArrowinBound(ref menuNavigatorX, menuNavigatorXCap);
                KeepArrowinBound(ref menuNavigatorY, menuNavigatorYCap);

            });
        }
        public string GetCommandState()
        {
            string holder = currentScreen.GetMenuCommand(menuNavigatorX, menuNavigatorY);
            return holder;
        }
        private void KeepArrowinBound(ref int currentArrowPosition, int maxArrowPostion)
        {
            if (currentArrowPosition > maxArrowPostion)
            {
                currentArrowPosition = 0;
            }
            else if (currentArrowPosition < 0)
            {
                currentArrowPosition = maxArrowPostion;
            }
        }

        public override void UpdateGameState(GameTime gameTime) 
        {
            _menuArrow.Update(gameTime);
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper(this));
        }
    }
}
