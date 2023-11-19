﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;
namespace GameEditor
{
    public class GameControl : MonoGameControl
    {
        private Texture2D _backgroundRectangle;
        private OrthographicCamera _camera;
        private bool _cameraDrag;

        private int _mouseX;
        private int _mouseY;
        protected override void Initialize()
        {
            base.Initialize();
            _backgroundRectangle = new Texture2D(GraphicsDevice, 1, 1);
            _backgroundRectangle.SetData(new[] { Color.CadetBlue });
            var viewportAdapter = new DefaultViewportAdapter(Editor.graphics);
            _camera = new OrthographicCamera(viewportAdapter);
            ResetCameraPosition();
            _draggedTile = null;
            OnInitialized(this, EventArgs.Empty);
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

        private void ResetCameraPosition()
        {
            _camera.Position = new Vector2(0, Level.LEVEL_LENGTH * TILE_SIZE - ClientSize.Height);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Middle)
            {
                _cameraDrag = false;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_cameraDrag)
            {
                _camera.Move(new Vector2(_mouseX - e.X, _mouseY - e.Y));
            }
            _mouseX = e.X;
            _mouseY = e.Y;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Middle)
            {
                _cameraDrag = true;
            }
        }

    }