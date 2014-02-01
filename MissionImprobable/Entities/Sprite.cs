using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MissionImprobable.Entities
{
    public class Sprite
    {
        protected Texture2D texture;
        protected Rectangle movementBounds;
        protected Vector2 position;
        protected float _GameSpeed = 128;
        private int _frames;

        public float Width { get { return texture.Width / _frames; } }
        public float Height { get { return texture.Height; } }

        public float GameSpeed
        {
            get { return _GameSpeed; }
            set { _GameSpeed = value; }
        }

        public Vector2 Position { get { return position; } }

        public Rectangle BoundingBox
        {
            get { return CreateBoundingBoxFromPosition(position); }
        }




        protected Sprite(Texture2D texture, Vector2 position)
            : this(texture, position, new Rectangle(0, 0, 0, 0), 1)
        {

        }

        public Sprite(Texture2D texture, Vector2 position, Rectangle playerBounds)
            : this(texture, position, playerBounds, 1)
        {
            
        }

        public Sprite(Texture2D texture, Vector2 position, Rectangle movementBounds, int frames)
        {
            this.texture = texture;
            this.position = position;
            this.movementBounds = movementBounds;
            _frames = frames;

        }




        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        private Rectangle CreateBoundingBoxFromPosition(Vector2 boundingposition)
        {
            return new Rectangle((int)boundingposition.X, 
                (int)boundingposition.Y, (int)Width - 16, 59);
        }
    }
}
