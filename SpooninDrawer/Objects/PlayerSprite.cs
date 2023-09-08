using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects
{
    public class PlayerSprite : BaseGameObject
    {
        private const float PLAYER_SPEED = 10.0f;

        public PlayerSprite(Texture2D texture)
        {
            _texture = texture;
        }

        public void MoveLeft()
        {
            Position = new Vector2(Position.X - PLAYER_SPEED, Position.Y);
        }

        public void MoveRight()
        {
            Position = new Vector2(Position.X + PLAYER_SPEED, Position.Y);
        }
    }
}
