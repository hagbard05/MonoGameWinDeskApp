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
    /// <summary>
    /// player class 
    /// </summary>
    public class Player : Sprite
    {
        public static int iniLives = 10;
        public int lives = iniLives;

        public bool shieldUp;
        public bool playerSettingsHaveBeenModifiedByPepperPowerup;
        public bool playerSettingsHaveBeenModifiedByInvincibleBulletPowerUp = false;
        public bool playerSettingsHaveBeenModifiedByShieldPowerUp = false;
        public bool playerSettingsHaveBeenModifiedBySpeedPowerUp = false;
        public double shieldUpCounter;
        public int speed = defaultSpeed;
        public int score = 0;
        public Timer projectileFrequencyTimer;
        public Timer pepperPowerUpTimeoutTimer;
        public Timer invincibleBulletPowerUpTimeoutTimer;
        public Timer speedPowerUpTimeoutTimer;
        public Timer superBombTimer;
        public static float defaultSuperBombFrequency = 500; // half a second
        public static float defaultProjectileFrequency = 150; //in miliseconds
        public int projectileSpeed;
        public static int defaultProjectileSpeed = 26;
        public int headExplodeCounter;
        public static int defaultSpeed = 7;
        public int superBombs = 0;
        public double shieldTimeAfterDeath = 960;
        public double shieldTimeFromShieldPowerUp = 500;

        public Texture2D shieldSprite;
        public AnimatedTexture explosion;
        public Color[] shieldTextureData;

        public Vector2 startPosition;
        public Vector2 shieldOffset;

        public List<Projectile> projectiles;

        public void resetToIniValues()
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
            projectiles.Clear();
            powerUpType = powerUpTypes.none;
            playerSettingsHaveBeenModifiedByPepperPowerup = false;
        }

        public Player(MainGame game, GameTime gameTime)
        {
            bulletCollision = false;
            playerCollision = false;
            shieldUp = false;
            shieldUpCounter = 0;
            speed = defaultSpeed;
            score = 0;
            projectileSpeed = defaultProjectileSpeed; // ToDo: Implment the projector constructor in the main game to customise player projectile speed
            headExplodeCounter = 0;
            sprite = game.Content.Load<Texture2D>("smile");
            shieldSprite = game.Content.Load<Texture2D>("Sphere75");
            position.X = 0;
            position.Y = 0;
            shieldOffset.X = -40;
            shieldOffset.Y = -30;
            startPosition.X = 0;
            startPosition.Y = 0;
            projectiles = new List<Projectile>();
            explosion = new AnimatedTexture(Vector2.Zero);
            powerUpType = powerUpTypes.none;
            projectileFrequencyTimer = new Timer(defaultProjectileFrequency, gameTime);
            superBombTimer = new Timer(defaultSuperBombFrequency, gameTime);
            playerSettingsHaveBeenModifiedByPepperPowerup = false;

            // Extract texture data for per pixle collision detection
            shieldTextureData = new Color[shieldSprite.Width * shieldSprite.Height];
            shieldSprite.GetData(shieldTextureData);

        }

        public Player(MainGame game, Vector2 startPos, Texture2D Sprite, GameTime gameTime)
        {
            bulletCollision = false;
            playerCollision = false;
            shieldUp = false;
            shieldUpCounter = 0;
            speed = defaultSpeed;
            score = 0;
            projectileSpeed = defaultProjectileSpeed;
            headExplodeCounter = 0;
            sprite = Sprite;
            shieldSprite = game.Content.Load<Texture2D>("Sphere75");
            position = startPos;
            startPosition = startPos;
            shieldOffset.X = -40;
            shieldOffset.Y = -30;
            projectiles = new List<Projectile>();
            explosion = new AnimatedTexture(Vector2.Zero);
            powerUpType = powerUpTypes.none;
            projectileFrequencyTimer = new Timer(defaultProjectileFrequency, gameTime);
            superBombTimer = new Timer(defaultSuperBombFrequency, gameTime);
            playerSettingsHaveBeenModifiedByPepperPowerup = false;

            // Extract texture data for per pixle collision detection
            shieldTextureData = new Color[shieldSprite.Width * shieldSprite.Height];
            shieldSprite.GetData(shieldTextureData);


        }

        public Player(MainGame game, Vector2 startPos, GameTime gameTime)
        {
            bulletCollision = false;
            playerCollision = false;
            shieldUp = false;
            shieldUpCounter = 0;
            speed = defaultSpeed;
            score = 0;
            projectileSpeed = defaultProjectileSpeed;
            headExplodeCounter = 0;
            sprite = game.Content.Load<Texture2D>("smile");
            shieldSprite = game.Content.Load<Texture2D>("Sphere75");
            position = startPos;
            startPosition = startPos;
            shieldOffset.X = -40;
            shieldOffset.Y = -30;
            projectiles = new List<Projectile>();
            explosion = new AnimatedTexture(Vector2.Zero);
            powerUpType = powerUpTypes.none;
            projectileFrequencyTimer = new Timer(defaultProjectileFrequency, gameTime);
            superBombTimer = new Timer(defaultSuperBombFrequency, gameTime);
            playerSettingsHaveBeenModifiedByPepperPowerup = false;

            // Extract texture data for per pixle collision detection
            shieldTextureData = new Color[shieldSprite.Width * shieldSprite.Height];
            shieldSprite.GetData(shieldTextureData);
        }

        public override Rectangle rectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height); }
        }

        public Rectangle shieldRectangle
        {
            get { return new Rectangle((int)shieldSpritePosition.X, (int)shieldSpritePosition.Y, shieldSprite.Width, shieldSprite.Height); }
        }

        public Vector2 shieldSpritePosition
        {
            get { return new Vector2(position.X + shieldOffset.X, position.Y + shieldOffset.Y); }
        }

        public Vector2 explosionSpritePosition
        {
            get { return new Vector2(position.X - 20, position.Y - 10); }
        }

        public Vector2 CenterPosition
        {
            get { return new Vector2(position.X + sprite.Width / 2, position.Y + sprite.Height / 2); }
        }

        public Vector2 shieldCenterPosition
        {
            get { return new Vector2(shieldSpritePosition.X + shieldSprite.Width / 2, shieldSpritePosition.Y + shieldSprite.Height / 2); }
        }
    }
}
