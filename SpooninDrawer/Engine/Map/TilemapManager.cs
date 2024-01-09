using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace SpooninDrawer.Engine.Map
{
    public class TilemapManager
    {
        private SpriteBatch _spriteBatch;
        TmxMap _map;
        Texture2D tileset;
        int tilesetTilesWide;
        int tileWidth;
        int tileHeight;

        public TilemapManager(SpriteBatch spriteBatch, TmxMap map, Texture2D tileset, int tilesetTilesWide, int tileWidth, int tileHeight)
        {
            _spriteBatch = spriteBatch;
            _map = map;
            this.tileset = tileset;
            this.tilesetTilesWide = tilesetTilesWide;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }

        public void Draw() { 
            _spriteBatch.Begin();
            for(int i = 0; i < _map.Layers.Count; i++) 
            { 
                for(int j =0;  j < _map.Layers[i].Tiles.Count; j++)
                {
                    //int gid = _map.Layers[i].Tiles.g;
                }
            }
        }
    }
}
