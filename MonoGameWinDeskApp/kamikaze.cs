using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using AnimatedSprite;

namespace MonoGameWinDeskApp
{
    class Kamikaze : Sprite
    {
        public int speed;
        public int defaultSpeed = 2;
        public Vector2 direction;
        public AnimatedTexture explosion;
        public AnimatedTexture anamatedSprite;
        public int explodeCounter;
        public int explodeDownTime;
		public int defaultExplodeDownTime = 34;

        public Kamikaze(MainGame game, Vector2 startPosition, AnimatedTexture Explosion, AnimatedTexture textureForAnamatedSprite)
        {
            sprite = game.Content.Load<Texture2D>("Kspr");
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

        public override Rectangle rectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height); }
        }

        public Vector2 explosionSpritePosition
        {
			get 
            {
				Vector2 explosPos = new Vector2(position.X + (sprite.Width / 2) - (explosion.FrameWidth / 2), position.Y - (sprite.Height / 2) + (explosion.FrameHeight / 2)); 
				//Vector2 explosPos = new Vector2(position.X + (explosion.FrameWidth / 2), position.Y - (explosion.FrameHeight / 2)); 
                return new Vector2(position.X , position.Y  ); 
            } 
        }

        public Vector2 centerPosition
        {
            get 
            {
                return new Vector2(position.X + (sprite.Width / 2), position.Y - (sprite.Height / 2)); 
            }
			set
			{
				position.X = value.X + (sprite.Width / 2);
				position.Y = value.Y - (sprite.Height / 2);
			}
        }
    }
}
