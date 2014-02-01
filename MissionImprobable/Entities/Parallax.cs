using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MissionImprobable.Entities
{
    public class Parallax : Sprite
    {

        #region fields/props

        private readonly Texture2D _texture;
        private readonly Vector2 _position;
        private float _offset = 0;
        public const float ParallaxScrollSpeed = 50;

        #endregion

        public Parallax(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            _texture = texture;
            _position = position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (float f = -_offset; f < 800; f += _texture.Width)
            {
                spriteBatch.Draw(_texture, new Vector2(f, position.Y ), new Rectangle(0, 0, 800, 480), Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, 0);
            }

        }

        public override void Update(GameTime gameTime)
        {
            _offset += ParallaxScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_offset > _texture.Width) _offset -= _texture.Width;


        }
    }
}
