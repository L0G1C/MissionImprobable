using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MissionImprobable.Entities;

namespace MissionImprobable.Managers
{
    public class SoundManager
    {
        private Song _backgroundMusic;
        private SoundEffect _alarm;
        private SoundEffect _laserHitGrunt;
        


        public SoundManager(ContentManager Content)
        {
            _backgroundMusic = Content.Load<Song>(@"sfx/backgroundMusic");
            _alarm = Content.Load<SoundEffect>(@"sfx/alarm");
            _laserHitGrunt = Content.Load<SoundEffect>(@"sfx/laserHitGrunt");
            
        }

        public void PlayBackgroundMusic()
        {
            if (MediaPlayer.GameHasControl)
            {
                MediaPlayer.Play(_backgroundMusic);
                MediaPlayer.IsRepeating = true;
            }
        }
        

        public void Alarm()
        {
            _alarm.Play();
        }

        public void LaserHit()
        {
            _laserHitGrunt.Play();
        }

        public void StopMusic()
        {
            MediaPlayer.Stop();
        }
    }
}
