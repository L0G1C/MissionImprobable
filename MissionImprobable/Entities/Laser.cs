using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MissionImprobable.Entities
{
    public class Laser : Sprite
    {
        private readonly Texture2D _texture;
        private readonly Vector2 _position;        

        public Laser(Texture2D texture, Vector2 position)
            :base(texture, position)
        {
            _texture = texture;
            _position = position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, position, new Rectangle(0, 0, 39, 20),
    Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            position.X -= GameSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
