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
    class CrystalGrabber : Sprite
    {
		static int seedForRandom = 0;

        public bool hasGrabbedCrystal = false;
        public enum entityTypes { Kamikaze, CrystalGrabber, none };
        public entityTypes EntityType;
        public int explodeCounter;
        public int explodeDownTime;
        public int defaultExplodeDownTime = 34;
        public float speed;
        public float defaultSpeed = 0.42f;
        public Vector2 direction;
        public AnimatedTexture explosion;
        public AnimatedTexture anamatedSprite;

		public Vector2 offsetToGrabCrystal;

        public CrystalGrabber(MainGame game, Vector2 startPosition, AnimatedTexture Explosion, AnimatedTexture textureForAnamatedSprite)
        {
            setDefaults(game);
            position = startPosition;
            explosion = Explosion;
            anamatedSprite = textureForAnamatedSprite;
        }

        public CrystalGrabber(MainGame game, Vector2 startPosition, AnimatedTexture Explosion, AnimatedTexture textureForAnamatedSprite, float speed)
        {
            setDefaults(game);
            position = startPosition;
            explosion = Explosion;
            anamatedSprite = textureForAnamatedSprite;
            this.speed = speed;
        }

        public void setDefaults(MainGame game)
        {
            speed = defaultSpeed;
            EntityType = entityTypes.CrystalGrabber;
            powerUpType = powerUpTypes.none;
            sprite = game.OctoEnemy;
            bulletCollision = false;
            playerCollision = false;
            direction = new Vector2();
            explodeCounter = 0;
            if (seedForRandom > 9000)
                seedForRandom = 1;
            Random random = new Random(seedForRandom++);
            offsetToGrabCrystal = new Vector2(random.Next(0 - sprite.Width / 2, game.powerCrystal.rectangle.Width - sprite.Width / 2), random.Next(0 - sprite.Height / 2, game.powerCrystal.rectangle.Height - sprite.Height / 2));
            explodeDownTime = defaultExplodeDownTime;
        }

        public Vector2 explosionSpritePosition
        {
            get { return new Vector2(position.X - 20, position.Y - 10); }
        }

    }
}
