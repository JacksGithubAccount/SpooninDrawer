﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.Objects;
using SpooninDrawer.Engine.States;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Audio;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Text;
using SpooninDrawer.States.Gameplay;
using System.Reflection.Metadata;

using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using TiledSharp;


namespace SpooninDrawer.Engine.States.Gameplay
{
    public class GameplayState : BaseGameState
    {
        private const float CAMERA_SPEED = 10.0f;

        private const string BackgroundTexture = "Sprites/Barren";
        private const string PlayerFighter = "Sprites/Animations/PlayerSpriteSheet";
        private const string PlayerAnimationTurnLeft = "Animations/Player/left_walk";
        private const string PlayerAnimationTurnRight = "Animations/Player/right_walk";
        private const string PlayerAnimationIdle = "Animations/Player/idle";
        private const string TiledMapTest = "TiledMaps/test";
        //rivate const string ExplosionTexture = "Sprites/explosion";

        private const string TextFont = "Fonts/Lives";
        private const string GameOverFont = "Fonts/GameOver";

        //private const string BulletSound = "Sounds/bulletSound";
        //private const string MissileSound = "Sounds/missileSound";

        //private const string Soundtrack1 = "Music/FutureAmbient_1";
        //private const string Soundtrack2 = "Music/FutureAmbient_2";

        private const int StartingPlayerLives = 3;
        private int _playerLives = StartingPlayerLives;

        private const int MaxExplosionAge = 600; // 10 seconds
        private const int ExplosionActiveLength = 75; // emit particles for 1.2 seconds and let them fade out for 10 seconds

        private Texture2D _missileTexture;
        private Texture2D _exhaustTexture;
        private Texture2D _bulletTexture;
        private Texture2D _explosionTexture;
        private Texture2D _chopperTexture;
        private Texture2D _screenBoxTexture;

        private PlayerSprite _playerSprite;

        private bool paused = false;
        private bool _playerDead;
        private bool _gameOver = false;
        
        private bool _isShootingBullets;
        private bool _isShootingMissile;
        private TimeSpan _lastBulletShotAt;
        private TimeSpan _lastMissileShotAt;

        private const string StatsFont = "Fonts/Stats";       
        private StatsObject _statsText;

        TmxMap _map;
        TiledMap _tiledMap;
        TiledMapTile _tiledMapTile;
        TiledMapRenderer _tiledMapRenderer;
        TiledMapLayer _tiledMapLayer;
        private List<Rectangle> colliders;

        private OrthographicCamera _camera;

        public override void LoadContent()
        {
            _debug = true;
            //_explosionTexture = LoadTexture(ExplosionTexture);
            _tiledMap = LoadTiledMap(TiledMapTest);
            _tiledMapRenderer = GetTiledMapRenderer(_tiledMap);
            colliders = new List<Rectangle>();
            //foreach (var o in _tiledMap.ObjectLayers["Collisions"].Objects)
            //{
            //    colliders.Add(new Rectangle(((int)o.X,(int)o.Y)));
            //}

            var turnLeftAnimation = LoadAnimation(PlayerAnimationTurnLeft);
            var turnRightAnimation = LoadAnimation(PlayerAnimationTurnRight);
            var idelAnimation = LoadAnimation(PlayerAnimationIdle);
            _playerSprite = new PlayerSprite(LoadTexture(PlayerFighter), turnLeftAnimation, turnRightAnimation, idelAnimation);
            // load sound effects and register in the sound manager
            //var bulletSound = LoadSound(BulletSound);
            //var missileSound = LoadSound(MissileSound);

            var viewportAdapter = new DefaultViewportAdapter(_graphicsDevice);
            _camera = new OrthographicCamera(viewportAdapter);

            _statsText = new StatsObject(LoadFont(StatsFont));
            _statsText.Position = new Vector2(10, 10);
            if (_debug)
            {
                AddGameObject(_statsText);
            }
            // load soundtracks into sound manager
            //var track1 = LoadSound(Soundtrack1).CreateInstance();
            //var track2 = LoadSound(Soundtrack2).CreateInstance();
            //_soundManager.SetSoundtrack(new List<SoundEffectInstance>() { track1, track2 });

            ResetGame();
        }
        //it's handleinput method but specifically for pause so this doesn't pause when you pause the game
        private void HandlePauseInput()
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GameplayInputCommand.Pause)
                {
                    //paused ^= true;
                    paused = !paused;
                }
            });
        }
        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GameplayInputCommand.GameExit)
                {
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                }

                if (cmd is GameplayInputCommand.PlayerMoveLeft && !_playerDead)
                {
                    _playerSprite.MoveLeft();
                    KeepPlayerInBounds();
                }

                if (cmd is GameplayInputCommand.PlayerMoveRight && !_playerDead)
                {
                    _playerSprite.MoveRight();
                    KeepPlayerInBounds();
                }

                if (cmd is GameplayInputCommand.PlayerMoveUp && !_playerDead)
                {
                    _playerSprite.MoveUp();
                    KeepPlayerInBounds();
                }

                if (cmd is GameplayInputCommand.PlayerMoveDown && !_playerDead)
                {
                    _playerSprite.MoveDown();
                    KeepPlayerInBounds();
                }

                if (cmd is GameplayInputCommand.PlayerStopsMoving && !_playerDead)
                {
                    _playerSprite.StopMoving();
                }

                if (cmd is GameplayInputCommand.PlayerAction && !_playerDead)
                {
                    //mc action
                }                
            });
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            
            HandlePauseInput();
            if (!paused)
            {
                HandleInput(gameTime);
                _tiledMapRenderer.Update(gameTime);
                _playerSprite.Update(gameTime);
                _camera.Position = _playerSprite.Position - _camera.Origin;

                DetectCollisions();
            }
            if (_debug)
            {
                _statsText.Update(gameTime);
            }
            // get rid of bullets and missiles that have gone out of view
        }
        public Matrix getCameraViewMatrix()
        {
            var transformMatrix = _camera.GetViewMatrix();
            return transformMatrix;
        }
        public override void Render(SpriteBatch spriteBatch)
        {            
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());

            base.Render(spriteBatch);
            var transformMatrix = getCameraViewMatrix();

            //spriteBatch.Begin(transformMatrix: transformMatrix);
            _playerSprite.Render(spriteBatch);
            //spriteBatch.End();
            if (_gameOver)
            {
                // draw black rectangle at 30% transparency
                var screenBoxTexture = GetScreenBoxTexture(spriteBatch.GraphicsDevice);
                var viewportRectangle = new Rectangle(0, 0, _viewportWidth, _viewportHeight);
                spriteBatch.Draw(screenBoxTexture, viewportRectangle, Color.Black * 0.3f);
            }
           
        }

        private Texture2D GetScreenBoxTexture(GraphicsDevice graphicsDevice)
        {
            if (_screenBoxTexture == null)
            {
                _screenBoxTexture = new Texture2D(graphicsDevice, 1, 1);
                _screenBoxTexture.SetData<Color>(new Color[] { Color.White });
            }

            return _screenBoxTexture;
        }


        private void DetectCollisions()
        {
            
        }

        private void ResetGame()
        {


            AddGameObject(_playerSprite);

            // position the player in the middle of the screen, at the bottom, leaving a slight gap at the bottom
            var playerXPos = _viewportWidth / 2 - _playerSprite.Width / 2;
            var playerYPos = _viewportHeight - _playerSprite.Height - 30;
            _playerSprite.Position = new Vector2(playerXPos, playerYPos);

            _playerDead = false;
        }


        private void GameOver()
        {
            var font = LoadFont(GameOverFont);
            var gameOverText = new GameOverText(font);
            var textPositionOnScreen = new Vector2(460, 300);

            gameOverText.Position = textPositionOnScreen;
            AddGameObject(gameOverText);
            _gameOver = true;
        }

        private List<T> CleanObjects<T>(List<T> objectList) where T : BaseGameObject
        {
            List<T> listOfItemsToKeep = new List<T>();
            foreach (T item in objectList)
            {
                var offScreen = item.Position.Y < -50;

                if (offScreen || item.Destroyed)
                {
                    RemoveGameObject(item);
                }
                else
                {
                    listOfItemsToKeep.Add(item);
                }
            }

            return listOfItemsToKeep;
        }



        private void KeepPlayerInBounds()
        {
            if (_playerSprite.Position.X < _camera.BoundingRectangle.Left)
            {
                _playerSprite.Position = new Vector2(0, _playerSprite.
               Position.Y);
            }
            if (_playerSprite.Position.X + _playerSprite.Width >
            _camera.BoundingRectangle.Right)
            {
                _playerSprite.Position = new Vector2(
                _camera.BoundingRectangle.Right - _playerSprite.
               Width,
                _playerSprite.Position.Y);
            }
            if (_playerSprite.Position.Y < _camera.BoundingRectangle.Top)
            {
                _playerSprite.Position = new Vector2(
                _playerSprite.Position.X,
                _camera.BoundingRectangle.Top);
            }
            if (_playerSprite.Position.Y + _playerSprite.Height > _camera.BoundingRectangle.Bottom)
            {
                _playerSprite.Position = new Vector2(
                _playerSprite.Position.X,
                _camera.BoundingRectangle.Bottom - _playerSprite.
               Height);
            }
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());

        }
    }
}
