using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using AnimatedSprite;

namespace MyFirstGame
{
    class ShootingEnemy : Sprite
    {
        public float speed;
        public int defaultSpeed = 2;
        public Vector2 direction;
        public AnimatedTexture explosion;
        public int explodeCounter;
        public int explodeDownTime;

        public ShootingEnemy()
        {
            
        }

        public ShootingEnemy(AnimatedTexture anamatedTexture, Texture2D texture, Vector2 position)
        {
            
        }



    }
}
