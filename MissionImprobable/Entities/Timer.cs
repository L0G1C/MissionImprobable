using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MissionImprobable.Entities
{
    public class Timer 
    {         
        public double _Timer { get; private set; }
        public int Difficulty { get; set; }

        public Timer()
        {
            _Timer = 10.0;
            Difficulty = 0;
        }

        public void Countdown(GameTime gameTime)
        {
            _Timer -= Math.Round(gameTime.ElapsedGameTime.TotalSeconds, 2);

            if (_Timer <= 0.0)
            {
                _Timer = 0;
            }
        }



        internal void Restart()
        {
            _Timer = 10.0;
        }
    }
}
