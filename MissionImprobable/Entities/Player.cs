using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MissionImprobable.Entities
{
    public class Player : Sprite
    {

        #region fields/props

        private int _playerCurrentFrame = 0;
        private readonly int _runFrameWidth = 0;
        private readonly int _runFrameHeight = 0;
        private readonly Texture2D _texture;
        public  Vector2 _position;
        private static double AnimationDelay = 0.1;
        private double _lastFrame;
        private readonly ContentManager _content;

        //jump
        private bool _isPressed = false;
        private bool _jumping = false;
        private float _velocityY = 0;
        private float _offsetY = 0;
        private static float AccelerationY = 400f;
        private static float StartVelocityY = -190;

        public bool IsDead { get; set; }    
        public bool IsJumping {get { return _jumping; }}

        #endregion

        public Player(Texture2D texture, Vector2 position, Rectangle movementBounds, ContentManager content)
            :base(texture, position, movementBounds, 13)
        {
            _runFrameWidth = texture.Width / 10;  //expecting 10 frame animation
            _runFrameHeight = texture.Height;
            _texture = texture;
            _position = position;
            _content = content;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDead)
            {
                if (!_jumping)
                {
                    spriteBatch.Draw(texture, _position,
                        new Rectangle(_playerCurrentFrame*_runFrameWidth, 0, _runFrameWidth, _runFrameHeight),
                        Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    spriteBatch.Draw(texture, new Vector2(_position.X, _position.Y + _offsetY),
                        new Rectangle(_playerCurrentFrame*70, 0, 70, 64), Color.White, 0, Vector2.Zero, 1,
                        SpriteEffects.FlipHorizontally, 0);
                }
            }



        }

        public override void Update(GameTime gameTime)
        {

            if (_jumping)
            {
                _offsetY += _velocityY*(float) gameTime.ElapsedGameTime.TotalSeconds;
                if (_offsetY > 0)
                {
                    _offsetY = 0;
                    _jumping = false;
                }
                _velocityY += AccelerationY*(float) gameTime.ElapsedGameTime.TotalSeconds;
            }

            KeyboardState keyboard = Keyboard.GetState();
            _isPressed = keyboard.IsKeyDown(Keys.Space);

            if (_isPressed && !_jumping)
            {
                _jumping = true;
                _lastFrame = 0;
                _velocityY = StartVelocityY;
                texture = _content.Load<Texture2D>(@"gfx/jump");
            }

            _lastFrame += gameTime.ElapsedGameTime.TotalSeconds; 

            if (!_jumping)
            {
                texture = _content.Load<Texture2D>(@"gfx/player");
                while (_lastFrame > AnimationDelay)
                {
                    _playerCurrentFrame++;
                    _playerCurrentFrame = _playerCurrentFrame%10;
                    _lastFrame -= AnimationDelay;
                }
            }
            else
            {
                 double totalJumpTime = Math.Abs(StartVelocityY*2)/AccelerationY;
                _playerCurrentFrame = (int) MathHelper.Lerp(0, 12.999f, (float) (_lastFrame/totalJumpTime));

                if (_playerCurrentFrame > 12) _playerCurrentFrame = 12;
            }

        }

        public void SpeedUp()
        {
            AccelerationY += 1f;            
        }
    }
}
