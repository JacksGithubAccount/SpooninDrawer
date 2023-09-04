using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects
{
    public class TitleImage : BaseGameObject
    {
        public TitleImage(Texture2D texture)
        {
            _texture = texture;
        }
    }
}
