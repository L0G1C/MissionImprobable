using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MissionImprobable.Entities;
using MissionImprobable.Managers;

namespace MissionImprobable.Managers
{
    public class CollisionManager
    {
        private readonly Player _player;
        private readonly LaserManager _laserManager;
        private readonly SoundManager _SoundManager;

        public CollisionManager(Player player, LaserManager laser, SoundManager soundManager)
        {
            _player = player;
            _laserManager = laser;
            _SoundManager = soundManager;
        }

        public void Update(GameTime gameTime)
        {
            if (!_player.IsJumping)
            {
                CheckForCollisions();
            }
            else
            {                
                CheckForCollisionsInAir();
            }
        }

        private void CheckForCollisions()
        {
            for (int i = 0; i < _laserManager.Lasers.Count(); i++)
            {
                var laser = _laserManager.Lasers[i];
                if (!_player.IsDead)
                {
                     if (_player.BoundingBox.Intersects(laser.BoundingBox))
                    {
                          _player.IsDead = true;
                         _SoundManager.LaserHit();
                    }
                }
            }
        }

        private void CheckForCollisionsInAir()
        {
            for (int i = 0; i < _laserManager.Lasers.Count(); i++)
            {
                var laser = _laserManager.Lasers[i];
                if (!_player.IsDead)
                {
                    var inflatedBounds = _player.BoundingBox;
                    inflatedBounds.Inflate(0, -24);

                    if (inflatedBounds.Intersects(laser.BoundingBox))
                    {
                        _player.IsDead = true;
                        _SoundManager.LaserHit();
                    }
                }
            }
        }  
    }
}
