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

        private Vector2 _position;

        public int zIndex;

        public virtual void OnNotify(Events eventType) { }

        public void Render(SpriteBatch spriteBatch)
        {
            // TODO: Drawing call here
            spriteBatch.Draw(_texture, Vector2.One, Color.White);
        }
    }
}
