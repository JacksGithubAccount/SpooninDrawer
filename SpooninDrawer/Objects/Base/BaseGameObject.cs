using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Base
{
    public class BaseGameObject
    {
        protected Texture2D _texture;
        protected Vector2 _position = Vector2.One;

        public int zIndex;

        public int Width { get { return _texture.Width; } }
        public int Height { get { return _texture.Height; } }
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public virtual void OnNotify(Events eventType, object argument = null) { }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
