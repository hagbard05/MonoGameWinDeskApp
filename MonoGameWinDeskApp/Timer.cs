using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyFirstGame
{
    public class Timer
    {
        float elapsedTime;
        float startTime;
        public float timerDuration;

        public Timer(float timerLength, GameTime gameTime)
        {
            timerDuration = timerLength;
            startTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
            elapsedTime = 0f;
        }

        public bool expired(GameTime gameTime)
        {
            elapsedTime = ((float)gameTime.TotalGameTime.TotalMilliseconds) - startTime;
            if (elapsedTime > timerDuration)
                return true;
            else return false;
        }
        public void reset(GameTime gameTime)
        {
            elapsedTime = 0f;
            startTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
        }
    }
}
