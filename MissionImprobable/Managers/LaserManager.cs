using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MissionImprobable.Entities;

namespace MissionImprobable.Managers
{
    public class LaserManager
    {
        private readonly Texture2D _texture ;
        private Laser Laser;
        private float _okToPlace = 0;
        public float _timeDelay = 1.2f;
        Random rand = new Random();
        private Rectangle _bounds;

        private readonly List<Laser> _laserList = new List<Laser>();

        public IList<Laser> Lasers {get { return _laserList; }}

        public LaserManager(Texture2D texture, Rectangle bounds)
        {
            _texture = texture;
            _bounds = bounds;
        }

        public void Update(GameTime gameTime)
        {
            _okToPlace += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timeDelay <= 0)
                _timeDelay = .3f;

            if (_okToPlace > _timeDelay)
            {

                var placeLaser = rand.Next(2);

                if (placeLaser == 0)
                {
                    Laser = new Laser(_texture, new Vector2(800, 335));
                    _laserList.Add(Laser);
                }

                _okToPlace = 0.0f;
            }

            foreach (var laser in _laserList)
            {
                laser.Update(gameTime);
            }

            for (int i = 0; i < _laserList.Count(); i++)
            {
                if (_laserList[i].Position.X < 500)
                {
                    var inflatedBounds = _bounds;
                    inflatedBounds.Inflate(50, 10);
                    if (!inflatedBounds.Contains(_laserList[i].BoundingBox))
                    {
                        _laserList.Remove(_laserList[i]);
                    }
                }
            }

        }

        public void DrawLasers(SpriteBatch spriteBatch)
        {
            foreach (var laser in _laserList)
            {
                laser.Draw(spriteBatch);
            }
        }
    }
}
