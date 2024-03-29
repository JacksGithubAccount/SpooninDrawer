﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects.Animations;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine2D.PipelineExtensions;
using Microsoft.Xna.Framework.Content;

namespace SpooninDrawer.Objects
{
    public class PlayerSprite : BaseGameObject
    {
        //public Vector2 CurrentUpSpeed { get; private set; }

        public float PlayerSpeed { get; set; } //velocity in units per seconds, so 600 units per second (10.0 times 60)
        public bool mapCollided = false;

        private const int BB1PosX = 29;
        private const int BB1PosY = 2;
        private const int BB1Width = 57;
        private const int BB1Height = 147;

        private const int BB2PosX = 2;
        private const int BB2PosY = 77;
        private const int BB2Width = 111;
        private const int BB2Height = 37;

        private Animation _idleAnimation = new Animation(false);
        private Animation _turnLeftAnimation = new Animation(false);
        private Animation _turnRightAnimation = new Animation(false);
        private Animation _leftToCenterAnimation = new Animation(false);
        private Animation _rightToCenterAnimation = new Animation(false);
        private const int AnimationSpeed = 3;
        private const int AnimationCellWidth = 116;
        private const int AnimationCellHeight = 152;

        private Animation _currentAnimation;
        private Rectangle _idleRectangle;

        private bool _isIdle = false;
        private bool _movingLeft = false;
        private bool _movingRight = false;
        private bool _movingUp = false;
        private bool _movingDown = false;
        private bool _stopLeft = false;
        private bool _stopRight = false;
        private bool _stopUp = false;
        private bool _stopDown = false;

        public bool _MustStop = false;
        public Vector2 _MoveDirection;

        public override int Height => AnimationCellHeight;
        public override int Width => AnimationCellWidth;

        public PlayerSprite(Texture2D texture, AnimationData turnLeftAnimation, AnimationData turnRightAnimation, AnimationData idleAnimation) : base(texture)
        {
            AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(BB1PosX, BB1PosY), BB1Width, BB1Height));
            AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(BB2PosX, BB2PosY), BB2Width, BB2Height));

            _idleRectangle = new Rectangle(348, 0, AnimationCellWidth, AnimationCellHeight);

            _idleAnimation = new Animation(idleAnimation);
            _turnLeftAnimation = new Animation(turnLeftAnimation);
            _turnRightAnimation = new Animation(turnRightAnimation);
            _leftToCenterAnimation = _turnLeftAnimation.ReverseAnimation;
            _rightToCenterAnimation = _turnRightAnimation.ReverseAnimation;

            PlayerSpeed = 10.0f;
            _MoveDirection = Position;
            //CurrentUpSpeed = _playerNormalUpSpeed;
        }
        //moved to content file
        /*public PlayerSprite(Texture2D texture)
        {
            _texture = texture;
            AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(BB1PosX, BB1PosY), BB1Width, BB1Height));
            AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(BB2PosX, BB2PosY), BB2Width, BB2Height));

            _idleRectangle = new Rectangle(348, 0, AnimationCellWidth, AnimationCellHeight);
            _idleAnimation.MakeLoop();
            _idleAnimation.AddFrame(new Rectangle(0, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _idleAnimation.AddFrame(new Rectangle(116, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _idleAnimation.AddFrame(new Rectangle(232, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _idleAnimation.AddFrame(new Rectangle(348, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _idleAnimation.AddFrame(new Rectangle(464, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);

            _turnLeftAnimation.MakeLoop();
            _turnLeftAnimation.AddFrame(new Rectangle(0, 152, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnLeftAnimation.AddFrame(new Rectangle(116, 152, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnLeftAnimation.AddFrame(new Rectangle(232, 152, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnLeftAnimation.AddFrame(new Rectangle(348, 152, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnLeftAnimation.AddFrame(new Rectangle(464, 152, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);

            _turnRightAnimation.AddFrame(new Rectangle(348, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnRightAnimation.AddFrame(new Rectangle(464, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnRightAnimation.AddFrame(new Rectangle(580, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnRightAnimation.AddFrame(new Rectangle(696, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);

            _leftToCenterAnimation = _turnLeftAnimation.ReverseAnimation;
            _rightToCenterAnimation = _turnRightAnimation.ReverseAnimation;
        }*/

        public void StopMoving()
        {
            if (_movingLeft)
            {
                _currentAnimation = _leftToCenterAnimation;
                _movingLeft = false;
            }

            if (_movingRight)
            {
                _currentAnimation = _rightToCenterAnimation;
                _movingRight = false;
            }
            if (!_movingLeft && !_movingRight && !_isIdle)
            {
                _currentAnimation = _idleAnimation;
                _isIdle = true;
                _idleAnimation.Reset();
            }
            mapCollided = false;
        }

        public void MoveLeft()
        {
            _movingLeft = true;
            _movingRight = false;
            _isIdle = false;
            _currentAnimation = _turnLeftAnimation;
            _leftToCenterAnimation.Reset();
            _turnRightAnimation.Reset();
            if (!_stopLeft)
            {
                _MoveDirection.X -= PlayerSpeed;
            }
            else
            {
                makeMoveAgainfromCollision();
            }
            
        }

        public void MoveRight()
        {
            _movingRight = true;
            _movingLeft = false;
            _isIdle = false;
            _currentAnimation = _turnRightAnimation;
            _rightToCenterAnimation.Reset();
            _turnLeftAnimation.Reset();
            if (!_stopRight)
            {
                _MoveDirection.X += PlayerSpeed;
            }
            else
            {
                makeMoveAgainfromCollision();
            }
        }

        public void MoveUp()
        {
            _movingUp = true;
            _movingDown = false;
            //_currentAnimation = _turnRightAnimation;
            //_rightToCenterAnimation.Reset();
            //_turnLeftAnimation.Reset();
            if (!_stopUp)
            {
                _MoveDirection.Y -= PlayerSpeed;
            }
            else
            {
                makeMoveAgainfromCollision();
            }
        }

        public void MoveDown()
        {
            _movingUp = false;
            _movingDown = true;
            //_currentAnimation = _turnRightAnimation;
            //_rightToCenterAnimation.Reset();
            //_turnLeftAnimation.Reset();
            if (!_stopDown)
            {
                _MoveDirection.Y += PlayerSpeed;
            }
            else
            {
                makeMoveAgainfromCollision();
            }
        }
        public void HandleMapCollision(MapTileCollider MapTile)
        {
            Engine.Objects.BoundingBox tempBB = BoundingBoxes[0];
            //PlayerSpeed = 0;
            mapCollided = true;            
            foreach(var bb in BoundingBoxes)
            {
                if (bb.CollidesWith(MapTile.BoundingBoxes[0]))
                {
                    tempBB = bb;
                }
            }
            
            Vector2 newPosition = new Vector2(Position.X, Position.Y);
            if (_movingLeft)
            {
                Rectangle LookLeftRect = new Rectangle((int)tempBB.Position.X,(int)tempBB.Position.Y,-5, (int)tempBB.Height);
                if (MapTile.IsCollide(LookLeftRect))
                {
                    _stopLeft = true;
                    if (tempBB.Position.X < MapTile.BoundingBoxes[0].Rectangle.Right)
                        newPosition.X = MapTile.BoundingBoxes[0].Rectangle.Right + (Position.X - tempBB.Position.X);
                }
            }
            else if (_movingRight)
            {
                Rectangle LookRightRect = new Rectangle((int)tempBB.Position.X + (int)tempBB.Width, (int)tempBB.Position.Y, 5, (int)tempBB.Height);
                if (MapTile.IsCollide(LookRightRect))
                {
                    _stopRight = true;
                    if (tempBB.Position.X + tempBB.Width > MapTile.BoundingBoxes[0].Rectangle.Left)
                        newPosition.X = MapTile.BoundingBoxes[0].Rectangle.Left -  tempBB.Width;
                }
            }
            if (_movingUp)
            {
                if (MapTile.IsPointinTile(Position.X, Position.Y - (MapTile.BoundingBoxes[0].Height / 5)))
                {
                    _stopUp = true;
                    //if (Position.Y < MapTileBoundingBox.Rectangle.Bottom)
                    //newPosition.Y = MapTileBoundingBox.Rectangle.Bottom;
                }
            }
            else if (_movingDown)
            {
                //_stopDown = true;
                //if (Position.Y + Height > MapTileBoundingBox.Rectangle.Top)
                    //newPosition.Y = MapTileBoundingBox.Rectangle.Top - Height;
            }
            Position = newPosition;

        }
        public void makeMoveAgainfromCollision()
        {
            _stopLeft = false;
            _stopRight = false;
            _stopDown = false;
            _stopUp = false;          
        }

        public void Update(GameTime gametime)
        {
            PlayerSpeed = (float)(600 * gametime.ElapsedGameTime.TotalSeconds);
            Position = _MoveDirection;
            if (_currentAnimation != null)
            {
                _currentAnimation.Update(gametime);
            }
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            var destinationRectangle = new Rectangle((int)_position.X, (int)_position.Y, AnimationCellWidth, AnimationCellHeight);
            var sourceRectangle = _idleRectangle;
            if (_currentAnimation != null)
            {
                var currentFrame = _currentAnimation.CurrentFrame;
                if (currentFrame != null)
                {
                    sourceRectangle = currentFrame.SourceRectangle;
                }
            }
            spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.Draw(_texture, BoundingBoxes[0].Position, BoundingBoxes[0].Rectangle, Color.Red);
            spriteBatch.Draw(_texture, BoundingBoxes[1].Position, BoundingBoxes[1].Rectangle, Color.Red);
            //spriteBatch.Draw(_texture, _MoveDirection, new Rectangle((int)Position.X, (int)Position.Y, (int)_MoveDirection.X, (int)_MoveDirection.Y), Color.Red);
        }
    }
}
