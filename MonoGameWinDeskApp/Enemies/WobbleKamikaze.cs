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
    class WobbleKamikaze : Sprite
    {
        public float speed;
        public float defaultSpeed = 0.55f;
        public Vector2 direction;
		public double wobbleRad = 0;
        public AnimatedTexture explosion;
        public AnimatedTexture anamatedSprite;
        public int explodeCounter;
        public int explodeDownTime;
		public int defaultExplodeDownTime = 34;

		public WobbleKamikaze(MainGame game, Vector2 startPosition, AnimatedTexture Explosion, AnimatedTexture textureForAnamatedSprite)
        {
			wobbleRad = (double)new Random().Next(6);
            sprite = game.kamakizeTex;
            powerUpType = powerUpTypes.none;
            speed = defaultSpeed;
            anamatedSprite = textureForAnamatedSprite;
            position = startPosition;
            bulletCollision = false;
            playerCollision = false;
            direction = new Vector2();
            explosion = Explosion;
            explodeCounter = 0;
			explodeDownTime = defaultExplodeDownTime;
        }

        public Vector2 explosionSpritePosition
        {
			get 
            {
				//Vector2 explosPos = new Vector2(position.X + (sprite.Width / 2) - (explosion.FrameWidth / 2), position.Y - (sprite.Height / 2) + (explosion.FrameHeight / 2)); 
                return new Vector2(position.X , position.Y  ); 
            } 
        }
    }
}
