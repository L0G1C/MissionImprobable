using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MissionImprobable.Entities
{
    class Platform : Sprite
    {

        #region fields/props

        private readonly Texture2D _texture;
        private readonly Vector2 _position;
        private float _platformOffset = 0;
        //public const float PlatformScrollSpeed = 128;

        #endregion

        public Platform(Texture2D texture, Vector2 position)
            : base(texture, position)
        {            
            _texture = texture;
            _position = position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (float f = -_platformOffset; f < 800; f += _texture.Width)
            {
                spriteBatch.Draw(_texture, new Vector2(f, position.Y), Color.White );
            }

        }

        public override void Update(GameTime gameTime)
        {
            _platformOffset += GameSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_platformOffset > _texture.Width) _platformOffset -= _texture.Width;


        }
    }
}
