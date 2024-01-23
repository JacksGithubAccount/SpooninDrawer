using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects
{
    public class MapTileCollider : BaseGameObject
    {
        private int BBPosX;
        private int BBPosY;
        private int BBWidth;
        private int BBHeight;

        private Rectangle _rectangle;

        public MapTileCollider(Rectangle rect)
        {
            _rectangle = rect;
            BBPosX = rect.X; BBPosY = rect.Y;
            BBWidth = rect.Width;
            BBHeight = rect.Height;
        }
    }
}
