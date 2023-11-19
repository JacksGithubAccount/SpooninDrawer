using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
namespace GameEditor
{
    public class GameControl : MonoGameControl
    {
        private Texture2D _backgroundRectangle;
        private OrthographicCamera _camera;
        private bool _cameraDrag;
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected override void Draw()
        {
            var tower = Editor.Content.Load<Texture2D>(@"Sprites\Tower");
            Editor.spriteBatch.Draw(
             tower, new Rectangle(0, 0, tower.Width, tower.Height),
            Color.White);
            base.Draw();
        }
    }
}