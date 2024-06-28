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
    /// <summary>
    /// player class 
    /// </summary>
    public class Player : Sprite
    {
        public int iniLives = 4;
        public int lives;

        public bool shieldUp;
        public bool hasStarted = false;
        public bool PepperPowerup;
        public bool InvincBulletPowerUp = false;
        public bool ShieldPowerUp = false;
        public bool SpeedPowerUp = false;
        public double shieldUpCounter;
        public float speed = defaultSpeed;
        public int score = 0;
        public Timer projectileFrequencyTimer;
        public Timer pepperPowerUpTimeoutTimer;
        public Timer invincibleBulletPowerUpTimeoutTimer;
        public Timer speedPowerUpTimeoutTimer;
        public Timer superBombTimer;
        public static float defaultSuperBombFrequency = 500; // half a second
        public static float defaultProjectileFrequency = 55; //in miliseconds
        public int projectileSpeed;
        public static int defaultProjectileSpeed = 30;
        public int headExplodeCounter;
        public static int defaultSpeed = 7;
        public int superBombs = 0;

		// Power up times
        public double shieldTimeAfterDeath = 230;
        public double shieldTimeFromShieldPowerUp = 1200;

		// Textures
        public Texture2D shieldSprite;
        public AnimatedTexture explosion = new AnimatedTexture(Vector2.Zero);
        public Color[] shieldTextureData;

		// position vectors
        public Vector2 startPosition;
        public Vector2 shieldOffset;

        public List<Projectile> projectiles = new List<Projectile>();

        public void setToIniValues()
        {
			lives = iniLives;
            speed = defaultSpeed;
            projectileSpeed = defaultProjectileSpeed;
            superBombs = 0;
            score = 0;
            bulletCollision = false;
            playerCollision = false;
            shieldUp = false;
            shieldUpCounter = 0;
            speed = defaultSpeed;
            projectileSpeed = defaultProjectileSpeed; // ToDo: Implment the projector constructor in the main game to customise player projectile speed
            headExplodeCounter = 0;
            position = startPosition;
            powerUpType = powerUpTypes.none;
            PepperPowerup = false;
            //if (Guide.IsTrialMode == true)
            //{
            //    iniLives = 3;
            //    shieldTimeFromShieldPowerUp = 600;
            //}
            lives = iniLives;
        }

        public void resetToIniValues()
        {
            setToIniValues();
            //projectiles.Clear();
        }

        public Player(MainGame game, GameTime gameTime)
        {
            projectiles = new List<Projectile>();
            explosion = new AnimatedTexture(Vector2.Zero);
			resetToIniValues();
            sprite = game.playerOneTex;
            shieldSprite = game.shieldSprite;
            shieldOffset.X = -40;
            shieldOffset.Y = -30;
            startPosition.X = 0;
            startPosition.Y = 0;

            projectileFrequencyTimer = new Timer(defaultProjectileFrequency, gameTime);
            superBombTimer = new Timer(defaultSuperBombFrequency, gameTime);

            // Extract texture data for per pixle collision detection
            shieldTextureData = new Color[shieldSprite.Width * shieldSprite.Height];
            shieldSprite.GetData(shieldTextureData);

        }

        public Player(MainGame game, Vector2 startPos, Texture2D Sprite, GameTime gameTime)
        {
            projectiles = new List<Projectile>();
            explosion = new AnimatedTexture(Vector2.Zero);
            sprite = Sprite;
            shieldSprite = game.shieldSprite;
            position = startPos;
            startPosition = startPos;
            shieldOffset.X = Sprite.Width;
            shieldOffset.Y = Sprite.Height;
            powerUpType = powerUpTypes.none;
            projectileFrequencyTimer = new Timer(defaultProjectileFrequency, gameTime);
            superBombTimer = new Timer(defaultSuperBombFrequency, gameTime);
            PepperPowerup = false;
            projectiles = new List<Projectile>();
            explosion = new AnimatedTexture(Vector2.Zero);

            // Extract texture data for per pixle collision detection
            shieldTextureData = new Color[shieldSprite.Width * shieldSprite.Height];
            shieldSprite.GetData(shieldTextureData);

			resetToIniValues();
        }

        public Player(MainGame game, Vector2 startPos, GameTime gameTime)
        {
            projectiles = new List<Projectile>();
            explosion = new AnimatedTexture(Vector2.Zero);
            sprite = game.playerOneTex;
            shieldSprite = game.shieldSprite;
            position = startPos;
            startPosition = startPos;
            shieldOffset.X = -40;
            shieldOffset.Y = -30;
            powerUpType = powerUpTypes.none;
            projectileFrequencyTimer = new Timer(defaultProjectileFrequency, gameTime);
            superBombTimer = new Timer(defaultSuperBombFrequency, gameTime);
            PepperPowerup = false;

            // Extract texture data for per pixle collision detection
            shieldTextureData = new Color[shieldSprite.Width * shieldSprite.Height];
            shieldSprite.GetData(shieldTextureData);

			resetToIniValues();
        }

        public Rectangle rectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height); }
        }

        public Rectangle shieldRectangle
        {
            get { return new Rectangle((int)shieldSpritePosition.X, (int)shieldSpritePosition.Y, shieldSprite.Width, shieldSprite.Height); }
        }

        public Vector2 shieldSpritePosition
        {
            get 
            {
                float x = CenterPosition.X + ((float)this.shieldSprite.Width / 2 - shieldSprite.Width);
                float y = CenterPosition.Y + ((float)this.shieldSprite.Height / 2 - shieldSprite.Height);
                return new Vector2(x,y);
            }
        }

        public Vector2 explosionSpritePosition
        {
            get { return new Vector2(position.X - 20, position.Y - 10); }
        }

        public Vector2 shieldCenterPosition
        {
            get { return new Vector2(shieldSpritePosition.X + shieldSprite.Width / 2, shieldSpritePosition.Y + shieldSprite.Height / 2); }
        }
    }
}
