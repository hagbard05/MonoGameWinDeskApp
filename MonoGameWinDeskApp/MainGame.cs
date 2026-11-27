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
	/// This is the main type for your game
	/// </summary>
	public partial class MainGame : Microsoft.Xna.Framework.Game
	{
		public bool IsTrialMode { get; }
		public bool isfullscreen = true;

		SpriteFont gameFont;
		SpriteFont gameFontLarger;
		SpriteFont Comic;

		public Random random1;
        public Random random2;
        public Random random3;
        public Random random4;
        public Random random5;
        public Random random6;
        int debugRandom;
 
        public int numPlayersStarted = 0;
        
        int _level = 0;
        public int level 
        { 
            get { return _level; } 
            set 
            {
				foreach (Player player in players)
				{
					if (value == levelToAddSpeedPowerUp && _level != levelToAddSpeedPowerUp)
					{
						spawnPowerUp(100000, Sprite.powerUpTypes.Speed, new Random(randomSeed3));
					}
					if (value == 1 && _level != 1)
					{
                        spawn10CrystalGrabbersBelow();
                        spawn10CrystalGrabbersBelow();
                        spawnCrystalGrabbersLeftOfScreen(10000);
                        spawnCrystalGrabbersRightOfScreen(10000);
						spawnPowerUp(100000, Sprite.powerUpTypes.Pepper, new Random(randomSeed6));
					}
					if (value == 2 && _level != 2)
					{
						spawnCrystalGrabbersRightOfScreen(10000);
						spawnCrystalGrabbersBelowScreen(10000);
						spawnCrystalGrabbersLeftOfScreen(100000);
						spawnCrystalGrabbersLeftOfScreen(10000);
						spawnCrystalGrabbersBelowScreen(10000);
						spawnPowerUp(100000, Sprite.powerUpTypes.Shield, new Random(randomSeed));
					}
					if (value == 3 && _level != 3)
					{
						spawnCrystalGrabbersLeftOfScreen(10000);
						spawnCrystalGrabbersRightOfScreen(1000);
						spawnCrystalGrabbersBelowScreen(10000);
						spawnCrystalGrabbersBelowScreen(1000);
						spawnCrystalGrabbersLeftOfScreen(100000);
						spawnCrystalGrabbersLeftOfScreen(10000);
						spawnCrystalGrabbersLeftOfScreen(1000);
						spawnCrystalGrabbersBelowScreen(100000);
						spawnCrystalGrabbersBelowScreen(10000);
						spawnCrystalGrabbersBelowScreen(1000);
						spawnPowerUp(100000, Sprite.powerUpTypes.Pepper, new Random(randomSeed6));
						spawnPowerUp(100000, Sprite.powerUpTypes.InvincibleBullet, new Random(randomSeed));
					}
					if (value == 4 && _level != 4)
					{
						spawnCrystalGrabbersLeftOfScreen(100000);
						spawnCrystalGrabbersRightOfScreen(10000);
						spawnCrystalGrabbersBelowScreen(100000);
						spawnCrystalGrabbersBelowScreen(10000);
						spawnCrystalGrabbersLeftOfScreen(1000000);
						spawnCrystalGrabbersLeftOfScreen(100000);
						spawnCrystalGrabbersLeftOfScreen(10000);
						spawnCrystalGrabbersBelowScreen(1000000);
						spawnCrystalGrabbersBelowScreen(100000);
						spawnCrystalGrabbersBelowScreen(10000);
						spawnPowerUp(100000, Sprite.powerUpTypes.SuperBomb, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Speed, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Shield, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.ExtraLife, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Pepper, new Random(randomSeed));
					}
					if (value == 5 && _level != 5)
					{
                        spawn10CrystalGrabbersBelow();
                        spawn10CrystalGrabbersBelow();
                        spawn10CrystalGrabbersBelow();
                        spawn10CrystalGrabbersBelow();

                        spawnCrystalGrabbersLeftOfScreen(1000000);
                        spawnCrystalGrabbersLeftOfScreen(100000);
                        spawnCrystalGrabbersLeftOfScreen(10000);

						spawnPowerUp(100000, Sprite.powerUpTypes.Pepper, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.InvincibleBullet, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.SuperBomb, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Speed, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Speed, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.ExtraLife, new Random(randomSeed));
					}
					if (value == 6 && _level != 6)
					{
						spawnPowerUp(100000, Sprite.powerUpTypes.ExtraLife, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Shield, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Speed, new Random(randomSeed));
						spawnCrystalGrabbersBelowScreen(1000000);
						spawnCrystalGrabbersBelowScreen(1000000);
						spawnCrystalGrabbersBelowScreen(1000000);
						spawnCrystalGrabbersBelowScreen(1000000);
						spawnCrystalGrabbersBelowScreen(1000000);
					}
					if (value == 7 && _level != 7)
					{
						spawnPowerUp(100000, Sprite.powerUpTypes.ExtraLife, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Shield, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Speed, new Random(randomSeed));
					}
					if (value == 8 && _level != 8)
					{
						spawnCrystalGrabbersLeftOfScreen(100000);
						spawnCrystalGrabbersRightOfScreen(10000);
						spawnCrystalGrabbersBelowScreen(100000);
						spawnCrystalGrabbersLeftOfScreen(100000);
						spawnCrystalGrabbersLeftOfScreen(100000);
						spawnCrystalGrabbersBelowScreen(1000000);
						spawnCrystalGrabbersBelowScreen(100000);
						spawnCrystalGrabbersBelowScreen(10000);
						spawnCrystalGrabbersBelowScreen(1000);
						spawnPowerUp(100000, Sprite.powerUpTypes.ExtraLife, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Shield, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Speed, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Pepper, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.InvincibleBullet, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.SuperBomb, new Random(randomSeed));
					}
					if (value == 9 && _level != 9)
					{
						spawnPowerUp(100000, Sprite.powerUpTypes.ExtraLife, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Shield, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Speed, new Random(randomSeed));
					}
					if (value == 10 && _level != 10)
					{
						spawnCrystalGrabbersLeftOfScreen(100);
						spawnCrystalGrabbersRightOfScreen(100);
						spawnCrystalGrabbersBelowScreen(100000);
						spawnCrystalGrabbersBelowScreen(10000);
						spawnCrystalGrabbersBelowScreen(100000);
						spawnCrystalGrabbersBelowScreen(10000);
						spawnCrystalGrabbersBelowScreen(100000);
						spawnCrystalGrabbersBelowScreen(10000);
						spawnCrystalGrabbersBelowScreen(1000);
						spawnCrystalGrabbersBelowScreen(100);
						spawnCrystalGrabbersLeftOfScreen(1000);
						spawnCrystalGrabbersLeftOfScreen(100);
						spawnCrystalGrabbersLeftOfScreen(10);
						spawnPowerUp(100000, Sprite.powerUpTypes.ExtraLife, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Shield, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Speed, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Pepper, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.InvincibleBullet, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.SuperBomb, new Random(randomSeed));
					}
					if (value == levelToAddKama && _level != levelToAddKama)
					{
						spawnKamikazesAboveScreen(100000);
						spawnKamikazesAboveScreen(100000);
						spawnKamikazesAboveScreen(10000);
						spawnKamikazesBelowScreen(10000);
						spawnKamikazesLeftOfScreen(10000);
						spawnKamikazesLeftOfScreen(10000);
						spawnPowerUp(100000, Sprite.powerUpTypes.ExtraLife, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Speed, new Random(randomSeed));
						spawnPowerUp(100000, Sprite.powerUpTypes.Pepper, new Random(randomSeed));
					}
					if (value == levelToAddWobbleKama && _level != levelToAddWobbleKama)
					{
						spawnWobbleKamikazes(1000000);
						spawnWobbleKamikazes(100000);
						spawnWobbleKamikazes(10000);
						spawnWobbleKamikazes(10000);
						spawnPowerUp(100000, Sprite.powerUpTypes.InvincibleBullet, new Random(randomSeed));
					}
				}
                _level = value; 
            } 
        }
        double diffuculityAdder = 0;

        public static int wobbleKSpanRateDefault = 9;
        public static int crystalGrabberSpawnRateDefault = 23;
		public static int kamikazeSpawnRateDefault = 12;

        int wobbleKSpanRate = wobbleKSpanRateDefault;
        int kamikazeSpawnRate = kamikazeSpawnRateDefault;
        int crystalGrabberSpawnRate = crystalGrabberSpawnRateDefault;
        int pepperPowerUpSpawnRate = pepperPowerUpSpawnRateDefault;
        int invincibleBulletPowerUpSpawnRate = invincibleBulletPowerUpSpawnRateDefault;
        int superBombPowerUpSpawnRate = superBombPowerUpSpawnRateDefault;
        int shieldPowerUpSpawnRate = shieldPowerUpSpawnRateDefault;
        int speedPowerUpSpawnRate = speedPowerUpSpawnRateDefault;
        int extraLifePowerUpSpawnRate = extraLifePowerUpSpawnRateDefault;

        bool finalScoreMessageboxHasBeenShown = false;

        GamePadState previousGamePadState1;
        GamePadState previousGamePadState2;
        GamePadState previousGamePadState3;
        GamePadState previousGamePadState4;

		protected bool _gameOver;
		public bool gameOver 
		{
            get { return _gameOver; } 
			set 
			{
                if (value == true)
                {
                    resetGame();
                }
				_gameOver = value; 
			} 
		} 

        public enum gameTypes { defendTheCrystal, deathMatch, none };
        gameTypes gameType = gameTypes.defendTheCrystal;

        int headExplodeDownTime = 59; //Number of cycles to show the explosion. default gameTime: 60 = 1 second = 0 to 59 = 1 second
        float elapsed; // used to track elapsed time for sprite anamation

		#region  Sound Effects
		SoundEffect unstopaballShot;
		SoundEffect powerUp;
		SoundEffect fireBallShot;
        SoundEffect normalShot;
        SoundEffect Explosion;
        SoundEffect explosion6;
		#endregion 

		SpriteBatch spriteBatch;
        Color backColor = Color.Black; //background color
        Vector2 centerOfScreen;

        // Color array data for the images - used for per-pixel collision
        // ToDo: finish setting up per pixle collisions
        Color[] forestSpriteTextureData;
        Color[] projectileSpriteTextureData;

        public Player playerOne = null;
        public Player playerTwo = null;
        public Player playerThree = null;
        public Player playerFour = null;

		public GameTime gameTime1;
		public Viewport viewport;
		public GraphicsDeviceManager graphics;

        public PowerCrystal powerCrystal;
		#region powerUp lists
		public List<PowerUp> pepperPowerUps = new List<PowerUp>();
        public List<PowerUp> InvincibleBulletPowerUps = new List<PowerUp>();
        public List<PowerUp> superBombPowerUps = new List<PowerUp>();
        public List<PowerUp> shieldPowerUps = new List<PowerUp>();
        public List<PowerUp> speedPowerUps = new List<PowerUp>();
        public List<PowerUp> extraLifePowerUps = new List<PowerUp>();
		#endregion

        #region Enemys
		List<Kamikaze> kamikazes = new List<Kamikaze>();
        List<WobbleKamikaze> wobbleKamikazes = new List<WobbleKamikaze>();
        List<CrystalGrabber> crystalGrabbers = new List<CrystalGrabber>();
		#endregion
		#region The Players
		/// <summary>
		/// The Players
		/// </summary>
		public List<Player> players = new List<Player>();
		#endregion 

		/// <summary>
		/// The sub-rectangle of the drawable area which should be visible on all TVs
		/// </summary>
		public Rectangle safeBounds;

        // Percentage of the screen on every side is the safe area
        const float SafeAreaPortion = 0.06f;
        
        ///<summary>
        /// Sets the screen size
        /// </summary>
        public MainGame(bool trialMode = false)
        {
            IsTrialMode = trialMode;
            graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

            graphics.IsFullScreen = isfullscreen;
			if (isfullscreen)
            {
                graphics.PreferredBackBufferWidth = 3840;
                graphics.PreferredBackBufferHeight = 2160;
            }
            else
            {
				graphics.PreferredBackBufferWidth = 3800;
				graphics.PreferredBackBufferHeight = 2023;
			}
			_gameOver = true;
        }

		#region Random number generation
		public static int _randomSeed = 1;
        public static int randomSeed 
        { 
            get 
            {
                if (_randomSeed > 2147483641) // int32 maxValue = 2,147,483,647
                    _randomSeed = 1;
                return _randomSeed++; 
            } 
        }
        public static int _randomSeed2 = 23;
        public static int randomSeed2
        {
            get
            {
                if (_randomSeed2 > 2147483642) // int32 maxValue = 2,147,483,647
                    _randomSeed2 = 3;
                return _randomSeed2++;
            }
        }
        public static int _randomSeed3 = 42;
        public static int randomSeed3
        {
            get
            {
                if (_randomSeed3 > 2147483643) // int32 maxValue = 2,147,483,647
                    _randomSeed3 = 4;
                return _randomSeed3++;
            }
        }
        public static int _randomSeed4 = 99;
        public static int randomSeed4
        {
            get
            {
                if (_randomSeed4 > 2147483644) // int32 maxValue = 2,147,483,647
                    _randomSeed4 = 5;
                return _randomSeed4++;
            }
        }
        public static int _randomSeed5 = 1234;
        public static int randomSeed5
        {
            get
            {
                if (_randomSeed5 > 2147483645) // int32 maxValue = 2,147,483,647
                    _randomSeed5 = 4321;
                return _randomSeed5++;
            }
        }
        public static int _randomSeed6 = 963578;
        public static int randomSeed6
        {
            get
            {
                if (_randomSeed6 > 2147483646) // int32 maxValue = 2,147,483,647
                    _randomSeed6 = 6;
                return _randomSeed6++;
            }
        }
		#endregion 

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
        {
            base.Initialize();

            random1 = new Random(randomSeed + randomSeed2);
            random2 = new Random(randomSeed2 + randomSeed + 1);
            random3 = new Random(randomSeed3 + randomSeed3);
            random4 = new Random(randomSeed4 + randomSeed2);
            random5 = new Random(randomSeed5 + 1 + randomSeed3);
            random6 = new Random(randomSeed6 + 7 + randomSeed);

            //audioEngine = new AudioEngine("Content\\Sounds\\XACT Sounds.xgs");
            //soundBank = new SoundBank(audioEngine, "Content\\Sounds\\Sound Bank.xsb");
            //waveBank = new WaveBank(audioEngine, "Content\\Sounds\\Wave Bank.xwb");

            // Create player objects
            playerOne = new Player(this, gameTime1);
            playerOne.explosion.Load(Content, explosionStripP1, 12, 12);

			//playerTwo = new Player(this, new Vector2(safeBounds.Right - playerTwoTex.Width, safeBounds.Top), playerTwoTex, gameTime1);
			playerTwo = new Player(this, new Vector2(500, 500), playerTwoTex, gameTime1);
			playerTwo.explosion.Load(Content, explosionStrip2, 12, 12);

            playerThree = new Player(this, new Vector2(safeBounds.Left - playerThreeTex.Width, safeBounds.Bottom - playerThreeTex.Height), playerThreeTex, gameTime1);
            playerThree.explosion.Load(Content, explosionStrip2, 12, 12);

            playerFour = new Player(this, new Vector2(safeBounds.Right - playerFourTex.Width, safeBounds.Bottom - playerFourTex.Height), playerFourTex, gameTime1);
            playerFour.explosion.Load(Content, explosionStrip2, 12, 12);
        }

		#region Textures Def
		public Texture2D background;
        public Texture2D backgroundSat;
        public Texture2D backgroundGalixy;
        public Texture2D backgroundMilkyWay;
        public Texture2D backgroundBlueSun;
        public Texture2D backgroundMiraInSnow;
        public Texture2D level2;
        public Texture2D level3;
        public Texture2D level4;
        public Texture2D level6;
        public Texture2D level10;

		public Texture2D forestSprite2;
		public Texture2D calebSprite;
		public Texture2D headExplodeSprite;
		public Texture2D splatSprite;
		public Texture2D fireBallMetalSphere;
		public Texture2D playerStatusArea;
		public Texture2D spriteOcto;
		public Texture2D spriteOctoLarge;

		public Texture2D debugDot; // one pixle used for position debugging
        public Texture2D playerBullet;
        public Texture2D metelSpherePowerUp; // Utilised
		public Texture2D metelSpherePowerUpProjectileSprite; // Utilised
		public Texture2D playerOneTex; // Utilised
		public Texture2D playerTwoTex; // Utilised
		public Texture2D playerThreeTex; // Utilised
		public Texture2D playerFourTex; // Utilised
		public Texture2D kamakizeTex; // Utilised
									  //public Texture2D kamakizeTexSm;
		public Texture2D oneUpTex; // Utilised
		public Texture2D crystal; // Utilised
		public Texture2D habanerio; // Utilised
		public Texture2D fireball; // Utilised
		public Texture2D bomb; // Utilised
		public Texture2D explosionStripForGhost; // Utilised
		public Texture2D KamakisaiSheetSmall; // Utilised
		public Texture2D explosionStrip; // Utilised
		public Texture2D OctoEnemy; // Utilised
		public Texture2D OctoEnemyAnamation; // Utilised
        public Texture2D OctoEnemyAnamation2; // Utilised
		//public Texture2D OctoEnemyAnamationSmall;
        //public Texture2D OctoEnemySmall;
        //public Texture2D wobbleKam;
        public Texture2D wobbleKSheet; // Utilised
		public Texture2D explosionStrip2; // Utilised
		public Texture2D shieldSprite; // Utilised
		public Texture2D powerCrystalTex; // Utilised
		public Texture2D speedPowUpTex; // Utilised
		public Texture2D shieldPowUpTex; // Utilised
		public Texture2D explosionStripForWobble; // Utilised
		public Texture2D explosionStripP1; // Utilised
		#endregion 

		/// <summary>LoadContent will be called once per game and is the place to load all of your content.</summary>
		protected override void LoadContent()
        {
            // Calculate safe bounds based on current resolution
            viewport = graphics.GraphicsDevice.Viewport;
            safeBounds = new Rectangle( (int)(viewport.Width * SafeAreaPortion), (int)(viewport.Height * SafeAreaPortion), (int)(viewport.Width * (1 - 2 * SafeAreaPortion)), (int)(viewport.Height * (1 - 2 * SafeAreaPortion)));
            gameTime1 = new GameTime();
            centerOfScreen = new Vector2((viewport.Width / 2), (viewport.Height / 2));

            //Backgrounds
            backgroundSat = Content.Load<Texture2D>("Saturn");
            backgroundGalixy = Content.Load<Texture2D>("Galixy");
            backgroundMilkyWay = Content.Load<Texture2D>("Saturn");
            backgroundBlueSun = Content.Load<Texture2D>("BlueSun");
            backgroundMiraInSnow = Content.Load<Texture2D>("Saturn");
            level2 = Content.Load<Texture2D>("level2");
            level3 = Content.Load<Texture2D>("level3");
            level4 = Content.Load<Texture2D>("level4");
            level6 = Content.Load<Texture2D>("level2");
            level10 = Content.Load<Texture2D>("level10");

            // sprites that are used by the Sprite (and child classes) objs
            explosionStripP1 = Content.Load<Texture2D>("explosionStripP1");
            shieldPowUpTex = Content.Load<Texture2D>("Sphere75small");
            speedPowUpTex = Content.Load<Texture2D>("speed");
			powerCrystalTex = Content.Load<Texture2D>("quartz");
            shieldSprite = Content.Load<Texture2D>("Sphere75");
            explosionStrip2 = Content.Load<Texture2D>("explosionStrip2");
            //OctoEnemySmall = Content.Load<Texture2D>("OctoEnemy-s");
            //wobbleKam = Content.Load<Texture2D>("WKam");
            wobbleKSheet = Content.Load<Texture2D>("wobKamAna");

            OctoEnemy = Content.Load<Texture2D>("OctoEnemy");
            OctoEnemyAnamation = Content.Load<Texture2D>("OctoEnemyAnamation");
            OctoEnemyAnamation2 = Content.Load<Texture2D>("OctoEnemyAnamation2");
            explosionStrip = Content.Load<Texture2D>("explosionStrip");
            KamakisaiSheetSmall = Content.Load<Texture2D>("KamakisaiSheet - small-s");
            explosionStripForGhost = Content.Load<Texture2D>("explosionStripForGhost");
            explosionStripForWobble = Content.Load<Texture2D>("exStripWobble");
			bomb = Content.Load<Texture2D>("H-Bomb");
			habanerio = Content.Load<Texture2D>("habanerio-small");
			fireball = Content.Load<Texture2D>("fireBall");
			crystal = Content.Load<Texture2D>("quartz");
            oneUpTex = Content.Load<Texture2D>("1up");
            kamakizeTex = Content.Load<Texture2D>("Kspr-ExSmall");
            //kamakizeTexSm = Content.Load<Texture2D>("Kspr-s");
            playerOneTex = Content.Load<Texture2D>("playerOne");
            playerTwoTex = Content.Load<Texture2D>("playerTwo");
            playerThreeTex = Content.Load<Texture2D>("player3");
            playerFourTex = Content.Load<Texture2D>("player4");

            playerBullet = Content.Load<Texture2D>("smileSmallStainG");
            metelSpherePowerUp = Content.Load<Texture2D>("metelSphere");
            metelSpherePowerUpProjectileSprite = Content.Load<Texture2D>("metelSphereSmall");

            gameFont = Content.Load<SpriteFont>("gameFont");
			gameFontLarger = Content.Load<SpriteFont>("gameFontLarger");
			Comic = Content.Load<SpriteFont>("Comic");
            background = Content.Load<Texture2D>("DeeperDive");
            calebSprite = Content.Load<Texture2D>("CalebSprite");
            forestSprite2 = Content.Load<Texture2D>("ForestSprite2");
			spriteOcto = Content.Load<Texture2D>("OctoEnemy");
            fireBallMetalSphere = Content.Load<Texture2D>("fireBallMetelSphere");  //used as a combo sprite for when player has invincibleBullet and Pepper powerups
            //projectileSprite = Content.Load<Texture2D>("flameBall");
            //headExplodeSprite = Content.Load<Texture2D>("headExplode");
            //splatSprite = Content.Load<Texture2D>("splat");
            playerStatusArea = Content.Load<Texture2D>("PlayerStatusArea");

            debugDot = Content.Load<Texture2D>("debugDot");// one pixle used for position debugging

			#region SoundEffects
			unstopaballShot = Content.Load<SoundEffect>("unstopaballShot");
			powerUp = Content.Load<SoundEffect>("reload");
			fireBallShot = Content.Load<SoundEffect>("FireBallShot");
            normalShot = Content.Load<SoundEffect>("puet");
            Explosion = Content.Load<SoundEffect>("exp");   
            explosion6 = Content.Load<SoundEffect>("explosion6");

			speedPowUpGrab = Content.Load<SoundEffect>("reload");
            lifePowUpGrab = Content.Load<SoundEffect>("ExLifeAhh");
            pepperPowUpGrab = Content.Load<SoundEffect>("FireballAhh");
            shieldPowUpGrab = Content.Load<SoundEffect>("reload");
            

            daBombPowUpGrab = Content.Load<SoundEffect>("reload");
            unstopPowUpGrab = Content.Load<SoundEffect>("reload");
			#endregion 

			// Extract collision data - used for per pixle collisions - not yet implemented
			forestSpriteTextureData = new Color[forestSprite2.Width * forestSprite2.Height];
            forestSprite2.GetData(forestSpriteTextureData);
            //projectileSpriteTextureData = new Color[projectileSprite.Width * projectileSprite.Height];

            // Create a new SpriteBatch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            powerCrystal = new PowerCrystal(this, centerOfScreen);
        }

        /// <summary>
        /// Checks the score for a deathmatch type game (not implemented fully)
        /// </summary>
        void CheckTheScore()
        {
            int winningScore = 10;
            if ((playerOne.score >= winningScore || playerTwo.score >= winningScore || playerThree.score >= winningScore || playerFour.score >= winningScore ) && finalScoreMessageboxHasBeenShown == false)
            {
                //System.Windows.Forms.MessageBox.Show("Game over. Final score: player one: " + playerOne.score + " player two: " + playerTwo.score
                //    + " player three: " + playerThree.score + " player four: " + playerFour.score);
                //finalScoreMessageboxHasBeenShown = true;
            }
        }
                
        /// <summary>UnloadContent will be called once per game and is the place to unload all content.</summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

		bool livesSetForTrialMode = false;

        /// <summary>Allows the game to run logic such as updating the world, checking for collisions, gathering input, and playing audio.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            gameTime1 = gameTime;
            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (Guide.IsVisible == false)
            {
                // Move the sprites around.
                UpdateInputAndMovement(gameTime);
                //CheckTheScore();
                if (gameOver == false)
                {
                    if (level > 5)
                        diffuculityAdder += 0.00060;
                    else
					{
						diffuculityAdder += 0.00069;
					}
					level = (int)diffuculityAdder;
                }
            }
            
            foreach (Player p in players)
            {
                if (p.lives <= 0)
                {
                    p.position = new Vector2(100000, 100000);
                    players.Remove(p);
                    numPlayersStarted--;
                    break;
                }
            }
            //audioEngine.Update();

            base.Update(gameTime);
        }

		Vector2 centerForGameOverText;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(backColor);
            spriteBatch.Begin();

            // Draw the background
            if (level == 0)
                spriteBatch.Draw(backgroundBlueSun, new Vector2(), Color.White);
            if (level == 1)
                spriteBatch.Draw(background, new Vector2(), Color.White);
            else if (level == 2)
                spriteBatch.Draw(level2, new Vector2(), Color.White);
            else if (level == 3)
                spriteBatch.Draw(level3, new Vector2(), Color.White);
            else if (level > 3 && level <= 5)
                spriteBatch.Draw(level4, new Vector2(), Color.White);
            else if (level > 5 && level <= 9)
                spriteBatch.Draw(level6, new Vector2(), Color.White);
            else if (level > 9 && level <= 10)
                spriteBatch.Draw(level10, new Vector2(), Color.White);
            else if (level > 10 && level <= 15)
                spriteBatch.Draw(backgroundMilkyWay, new Vector2(), Color.White);
            else if (level > 15 && level <= 20)
                spriteBatch.Draw(backgroundSat, new Vector2(), Color.White);
            else if (level > 20 && level <= 25)
                spriteBatch.Draw(backgroundMiraInSnow, new Vector2(), Color.White);

            //Check for game over
            if (gameOver == true)
            {
                if (IsTrialMode)
                {
                    spriteBatch.DrawString(gameFontLarger, "Upgrade to Full Version for: " + System.Environment.NewLine +
                        "*  4 Players at a time!" + System.Environment.NewLine +
                        "*  A happy happy joy joy best luck good time fun!", new Vector2(safeBounds.Left + 100, safeBounds.Top + 250), Color.White);
                }
                else
                {
                    centerForGameOverText = new Vector2(centerOfScreen.X - 146, centerOfScreen.Y - 12);
                    spriteBatch.DrawString(gameFont, "Game Over.  Press Start!", centerForGameOverText, Color.White);
                    spriteBatch.End();
                }
                base.Draw(gameTime);
                return; // draw nothing else
            }
            else if (gameOver == false)
            {
#if DEBUG
			spriteBatch.DrawString(gameFont, "diff adder: " + diffuculityAdder, new Vector2(safeBounds.Left, safeBounds.Top + 180), Color.Blue);
			spriteBatch.DrawString(gameFont,
                @"PlayerOne: (" + playerOne.position.X + ", " + playerOne.position.Y + ") " + System.Environment.NewLine + "CenterPos: (" + playerOne.CenterPosition.X + ", " + playerOne.CenterPosition.Y + ") " + System.Environment.NewLine
                 + "PowerCrystal: (" + this.powerCrystal.position.X + ", " + powerCrystal.position.Y + ") " + System.Environment.NewLine + "CenterPos: (" + powerCrystal.CenterPosition.X + ", " + powerCrystal.CenterPosition.Y + ") "
                , new Vector2(safeBounds.Left + 50, safeBounds.Height - 320), Color.OrangeRed); 
#endif
                // Draw the Player Status Areas
                if (playerOne.hasStarted)
                {
                    spriteBatch.Draw(playerStatusArea, new Vector2(safeBounds.Left - 5, safeBounds.Top - 5), new Color(255, 255, 255, 40));
                    spriteBatch.DrawString(gameFont, "Player One", new Vector2(safeBounds.Left, safeBounds.Top), Color.Blue);
                    spriteBatch.DrawString(gameFont, "Lives: " + playerOne.lives, new Vector2(safeBounds.Left, safeBounds.Top + 40), Color.Blue);
                    spriteBatch.DrawString(gameFont, "Super Bombs: " + playerOne.superBombs, new Vector2(safeBounds.Left, safeBounds.Top + 80), Color.Blue);
                    spriteBatch.DrawString(gameFont, "Score: " + playerOne.score, new Vector2(safeBounds.Left, safeBounds.Top + 120), Color.Blue);
                }
                if (playerTwo.hasStarted)
                {
                    spriteBatch.Draw(playerStatusArea, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Top), new Color(255, 255, 255, 40));
                    spriteBatch.DrawString(gameFont, "Player Two", new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Top), Color.Blue);
                    spriteBatch.DrawString(gameFont, "Lives: " + playerTwo.lives, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Top + 40), Color.Blue);
                    spriteBatch.DrawString(gameFont, "Super Bombs: " + playerTwo.superBombs, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Top + 80), Color.Blue);
                    spriteBatch.DrawString(gameFont, "Score: " + playerTwo.score, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Top + 120), Color.Blue);
                }
                if (playerThree.hasStarted)
                {
                    spriteBatch.Draw(playerStatusArea, new Vector2(safeBounds.Left, safeBounds.Bottom - playerStatusArea.Height), new Color(255, 255, 255, 40));
                    spriteBatch.DrawString(gameFont, "Player Three", new Vector2(safeBounds.Left, safeBounds.Bottom - playerStatusArea.Height), Color.Blue);
                    spriteBatch.DrawString(gameFont, "Lives: " + playerThree.lives, new Vector2(safeBounds.Left, safeBounds.Bottom - playerStatusArea.Height + 40), Color.Blue);
                    spriteBatch.DrawString(gameFont, "Super Bombs: " + playerThree.superBombs, new Vector2(safeBounds.Left, safeBounds.Bottom - playerStatusArea.Height + 80), Color.Blue);
                    spriteBatch.DrawString(gameFont, "Score: " + playerThree.score, new Vector2(safeBounds.Left, safeBounds.Bottom - playerStatusArea.Height + 120), Color.Blue);
                }
                if (playerFour.hasStarted)
                {
                    spriteBatch.Draw(playerStatusArea, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Bottom - playerStatusArea.Height), new Color(255, 255, 255, 40));
                    spriteBatch.DrawString(gameFont, "Player Four", new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Bottom - playerStatusArea.Height), Color.Blue);
                    spriteBatch.DrawString(gameFont, "Lives: " + playerFour.lives, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Bottom - playerStatusArea.Height + 40), Color.Blue);
                    spriteBatch.DrawString(gameFont, "Super Bombs: " + playerFour.superBombs, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Bottom - playerStatusArea.Height + 80), Color.Blue);
                    spriteBatch.DrawString(gameFont, "Score: " + playerFour.score, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Bottom - playerStatusArea.Height + 120), Color.Blue);
                }
                // Draw the power crystal in the center of the screen
                //spriteBatch.Draw(powerCrystal.sprite, powerCrystal.position, new Color(138,43,226,190));
                spriteBatch.Draw(powerCrystal.sprite, powerCrystal.position, 
                    new Color((int)((powerCrystal.CenterPosition.Y / 1.9) % 255), 43 + (int)((powerCrystal.CenterPosition.Y / 1.9) % 255), (int)((powerCrystal.CenterPosition.Y / 1.9) % 255) + 226, 190));

                // Draw the players
                if (playerOne.hasStarted && playerOne.lives > 0)
                {
                    if (playerOne.bulletCollision || playerOne.playerCollision)
                        playerOne.explosion.DrawFrame(spriteBatch, playerOne.explosionSpritePosition);
                    else if (!(playerOne.bulletCollision || playerOne.playerCollision))
                        spriteBatch.Draw(playerOne.sprite, playerOne.position, Color.White);
                    //draws shields - The fourth varaiable in the new color controls opacity which gives the shield a fade as it runs out

                    if (playerOne.shieldUp == true && playerOne.ShieldPowerUp == false)
                    {
                        spriteBatch.Draw(playerOne.shieldSprite, playerOne.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerOne.shieldUpCounter / (playerOne.shieldTimeAfterDeath / 185.0)) + 70)));
                    }

                    if (playerOne.shieldUp == true && playerOne.ShieldPowerUp == true)
                    {
                        spriteBatch.Draw(playerOne.shieldSprite, playerOne.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerOne.shieldUpCounter / (ShieldPowerUp.shieldTime / 185.0)) + 70)));
                    }
                    foreach (Projectile Projectile in playerOne.projectiles)
                        spriteBatch.Draw(Projectile.sprite, Projectile.position, Color.White);

                }
                if (playerTwo.hasStarted && playerTwo.lives > 0)
                {
                    if (playerTwo.bulletCollision || playerTwo.playerCollision)
                        playerTwo.explosion.DrawFrame(spriteBatch, playerTwo.explosionSpritePosition);
                    else if (!(playerTwo.bulletCollision || playerTwo.playerCollision))
                        spriteBatch.Draw(playerTwo.sprite, playerTwo.position, Color.White);
                    spriteBatch.Draw(debugDot, playerTwo.CenterPosition, Color.White);
                    if (playerTwo.shieldUp == true && playerTwo.ShieldPowerUp == false)
                        spriteBatch.Draw(playerTwo.shieldSprite, playerTwo.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerTwo.shieldUpCounter / (playerTwo.shieldTimeAfterDeath / 185.0)) + 70)));
                    if (playerTwo.shieldUp == true && playerTwo.ShieldPowerUp == true)
                        spriteBatch.Draw(playerTwo.shieldSprite, playerTwo.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerTwo.shieldUpCounter / (ShieldPowerUp.shieldTime / 185.0)) + 70)));
                    foreach (Projectile Projectile in playerTwo.projectiles)
                        spriteBatch.Draw(Projectile.sprite, Projectile.position, Color.White);
                }
                if (playerThree.hasStarted && playerThree.lives > 0)
                {
                    if (playerThree.bulletCollision || playerThree.playerCollision)
                        playerThree.explosion.DrawFrame(spriteBatch, playerThree.explosionSpritePosition);
                    else if (!(playerThree.bulletCollision || playerThree.playerCollision))
                        spriteBatch.Draw(playerThree.sprite, playerThree.position, Color.White);
                    if (playerThree.shieldUp == true && playerThree.ShieldPowerUp == false)
                        spriteBatch.Draw(playerThree.shieldSprite, playerThree.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerThree.shieldUpCounter / (playerThree.shieldTimeAfterDeath / 185.0)) + 70)));
                    if (playerThree.shieldUp == true && playerThree.ShieldPowerUp == true)
                        spriteBatch.Draw(playerThree.shieldSprite, playerThree.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerThree.shieldUpCounter / (ShieldPowerUp.shieldTime / 185.0)) + 70)));
                    foreach (Projectile Projectile in playerThree.projectiles)
                        spriteBatch.Draw(Projectile.sprite, Projectile.position, Color.White);
                }
                if (playerFour.hasStarted && playerFour.lives > 0)
                {
                    if (playerFour.bulletCollision || playerFour.playerCollision)
                        playerFour.explosion.DrawFrame(spriteBatch, playerFour.explosionSpritePosition);
                    else if (!(playerFour.bulletCollision || playerFour.playerCollision))
                        spriteBatch.Draw(playerFour.sprite, playerFour.position, Color.White);
                    if (playerFour.shieldUp == true && playerFour.ShieldPowerUp == false)
                        spriteBatch.Draw(playerFour.shieldSprite, playerFour.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerFour.shieldUpCounter / (playerFour.shieldTimeAfterDeath / 185.0)) + 70)));
                    if (playerFour.shieldUp == true && playerFour.ShieldPowerUp == true)
                        spriteBatch.Draw(playerFour.shieldSprite, playerFour.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerFour.shieldUpCounter / (ShieldPowerUp.shieldTime / 185.0)) + 70)));
                    // Draw projectiles
                    foreach (Projectile Projectile in playerFour.projectiles)
                        spriteBatch.Draw(Projectile.sprite, Projectile.position, Color.White);
                }

                // Draw powerUps
                foreach (PepperPowerUp pepperPowerUp in pepperPowerUps)
                    spriteBatch.Draw(pepperPowerUp.sprite, pepperPowerUp.position, Color.White);
                foreach (InvincibleBulletPowerUp invincibleBulletPowerUp in InvincibleBulletPowerUps)
                    spriteBatch.Draw(invincibleBulletPowerUp.sprite, invincibleBulletPowerUp.position, Color.White);
                foreach (SuperBombPowerUp superBombPowerUp in superBombPowerUps)
                    spriteBatch.Draw(superBombPowerUp.sprite, superBombPowerUp.position, Color.White);
                foreach (ShieldPowerUp shieldPowerUp in shieldPowerUps)
                    spriteBatch.Draw(shieldPowerUp.sprite, shieldPowerUp.position, Color.White);
                foreach (SpeedPowerUp speedPowerUp in speedPowerUps)
                    spriteBatch.Draw(speedPowerUp.sprite, speedPowerUp.position, Color.White);
                foreach (ExtraLifePowerUp extraLifePowerUp in extraLifePowerUps)
                    spriteBatch.Draw(extraLifePowerUp.sprite, extraLifePowerUp.position, Color.White);

                // Draw Enemys
                foreach (Kamikaze kamikazeObj in kamikazes)
                {
                    if (kamikazeObj.bulletCollision == true || kamikazeObj.playerCollision == true)
                        kamikazeObj.explosion.DrawFrame(spriteBatch, kamikazeObj.explosionSpritePosition);
                    else
                        kamikazeObj.anamatedSprite.DrawFrame(spriteBatch, kamikazeObj.position);
                    //spriteBatch.Draw(kamikazeObj.sprite, kamikazeObj.position, Color.White);
                    //spriteBatch.Draw(debugDot, kamikazeObj.centerPosition, Color.White);  // used for debugging position
                }

                foreach (WobbleKamikaze Wk in wobbleKamikazes)
                {
                    if (Wk.bulletCollision == true || Wk.playerCollision == true)
                        Wk.explosion.DrawFrame(spriteBatch, Wk.explosionSpritePosition);
                    else
                        Wk.anamatedSprite.DrawFrame(spriteBatch, Wk.position);
                }

                foreach (CrystalGrabber crystalGrabber in crystalGrabbers)
                {

                    if (crystalGrabber.bulletCollision == true || crystalGrabber.playerCollision == true)
                        crystalGrabber.explosion.DrawFrame(spriteBatch, crystalGrabber.position);
                    else
                        crystalGrabber.anamatedSprite.DrawFrame(spriteBatch, crystalGrabber.position);
                }

                // Draw level text
                spriteBatch.DrawString(Comic, "Level: " + level, new Vector2(centerOfScreen.X - 12, safeBounds.Top + 25), Color.White);

            } // end if (gameOver == false)



            spriteBatch.End();

            base.Draw(gameTime);
        }

        ///<summary>PlayerMovement handles the joystick movement for a player object</summary>
        ///<param name="playerObj">The player object to move</param>
        ///<param name="gamePadState">The gamePadState controlling the movement</param>
        void PlayerMovement(Player playerObj, GamePadState gamePadState)
        {
            if (playerObj.hasStarted && playerObj.lives > 0)
            {
                if (playerObj.playerCollision || playerObj.bulletCollision)
                {
                    playerObj.velocity = Vector2.Zero;
                    return;
                }

                Vector2 movementInput = new Vector2(
                    gamePadState.ThumbSticks.Left.X,
                    -gamePadState.ThumbSticks.Left.Y);

                if (playerObj == playerOne)
                {
                    KeyboardState keyboardState = Keyboard.GetState();
                    if (keyboardState.IsKeyDown(Keys.Right)) movementInput.X += 1;
                    if (keyboardState.IsKeyDown(Keys.Left)) movementInput.X -= 1;
                    if (keyboardState.IsKeyDown(Keys.Down)) movementInput.Y += 1;
                    if (keyboardState.IsKeyDown(Keys.Up)) movementInput.Y -= 1;
                }

                if (movementInput.LengthSquared() > 1f)
                    movementInput.Normalize();

                playerObj.velocity += movementInput * (playerObj.speed * 60f * elapsed);
                playerObj.position += playerObj.velocity * elapsed;
            }
        }

        float convertPositionToPanValue(float xPosition)
        {
            if (xPosition < 0)
                xPosition = 0;
            if (xPosition > (float)this.viewport.Width)
                xPosition = this.viewport.Width;
            float ViewportWidth = (float)this.viewport.Width;
            float ConvertedSpritePositionToPanValue = (xPosition / ViewportWidth * 2.0f) - 1.0f;
            return ConvertedSpritePositionToPanValue;
        }

        ///<summary>
        /// PlayerTiming handles the of the timing for shields and explosions
		/// </summary>
        void PlayerTiming(Player playerObj)
        {
            if (playerObj.hasStarted && playerObj.lives > 0)
            {
                if (playerObj.bulletCollision == true || playerObj.playerCollision == true)
                    playerObj.headExplodeCounter++;
                if (playerObj.shieldUp == true)
                    playerObj.shieldUpCounter--;
                if (playerObj.shieldUpCounter < 1)
                {
                    playerObj.ShieldPowerUp = false;
                    playerObj.shieldUp = false;
                    playerObj.shieldUpCounter = playerObj.shieldTimeAfterDeath;
                }
                if (headExplodeDownTime < playerObj.headExplodeCounter)
                {
                    //playerObj.explosion.Frame = 0;  //reset the explosion frame
                    playerObj.bulletCollision = false; //reset any prevoius collisions
                    playerObj.playerCollision = false; //reset any prevoius collisions
                    playerObj.headExplodeCounter = 0;
                    playerObj.position = playerObj.startPosition;
                    playerObj.velocity = Vector2.Zero;
                    playerObj.shieldUp = true;
                }
                if (playerObj.playerCollision == true || playerObj.bulletCollision == true)
                {
                    playerObj.explosion.UpdateFrame(elapsed);
                }
            }
            else if (playerObj.lives <= 0)
            {
                playerObj.position.X = -10000;
                playerObj.position.Y = -10000;
                return;
            }
        }

        bool kamikazeTiming(Kamikaze kamikazeObj)
        {
            kamikazeObj.anamatedSprite.UpdateFrame(elapsed);
            if (kamikazeObj.bulletCollision == true || kamikazeObj.playerCollision == true)
            {
                kamikazeObj.explodeCounter++;
                kamikazeObj.explosion.UpdateFrame(elapsed);
            }
            if (kamikazeObj.explodeCounter > kamikazeObj.explodeDownTime)
            {
                kamikazes.Remove(kamikazeObj);
                return true;
            }
            else return false;
            //ToDo: finish setting up timing needed to show explosion when enemy is hit for player enemy collision
        }

        bool wobbleKamikazeTiming(WobbleKamikaze wk)
        {
            wk.anamatedSprite.UpdateFrame(elapsed);
            if (wk.bulletCollision == true || wk.playerCollision == true)
            {
                wk.explodeCounter++;
                wk.explosion.UpdateFrame(elapsed);
            }
            if (wk.explodeCounter > wk.explodeDownTime)
            {
                wobbleKamikazes.Remove(wk);
                return true;
            }
            else return false;
        }
         
        bool enemyTiming(CrystalGrabber CrystalGrabberObj)
        {
            CrystalGrabberObj.anamatedSprite.UpdateFrame(elapsed);
            if (CrystalGrabberObj.bulletCollision == true || CrystalGrabberObj.playerCollision == true)
            {
                CrystalGrabberObj.explodeCounter++;
                CrystalGrabberObj.explosion.UpdateFrame(elapsed);
            }
            if (CrystalGrabberObj.explodeCounter > CrystalGrabberObj.explodeDownTime)
            {
                crystalGrabbers.Remove(CrystalGrabberObj);
                return true;
            }
            else return false;
            //ToDo: finish setting up timing needed to show explosion when enemy is hit for player enemy collision
        }

        ///<summary>
        /// checkPlayerBulletCollision handles checking weather a projectile object has hit any of the four players
        ///</summary>
        bool checkPlayerBulletCollision(Player playerObj, Projectile projectileObj)
        {
            bool result = false;
            int knockStrength = 3;
            if (playerObj.hasStarted)
            {
                if (playerOne.hasStarted)
                {
                    //if we are not player one and we hit player one
                    if (playerOne.rectangle.Intersects(projectileObj.rectangle) && playerOne.shieldUp == false && !(playerObj.Equals(playerOne)))
                    {
                        result = true;
                        if (gameType == gameTypes.deathMatch)
                        {
                            if (playerOne.bulletCollision == false)
                            {
                                playerObj.score += 25;
								explosion6.Play(0.5f, 0.0f, this.convertPositionToPanValue(playerObj.position.X));
								//soundBank.PlayCue("explosion6");
							}
                            playerOne.bulletCollision = true;
                        }
                        if (gameType == gameTypes.defendTheCrystal)
                        {
                            playerOne.velocity.X += projectileObj.direction.X * knockStrength * 60f;
                            playerOne.velocity.Y -= projectileObj.direction.Y * knockStrength * 60f;
                        }
                    }
                }
                if (playerTwo.hasStarted)
                {
                    //if we are not player two and we hit player two
                    if (playerTwo.rectangle.Intersects(projectileObj.rectangle) && playerTwo.shieldUp == false && !(playerObj.Equals(playerTwo)))
                    {
                        result = true;
                        if (gameType == gameTypes.deathMatch)
                        {
                            if (playerTwo.bulletCollision == false)
                            {
                                playerObj.score += 25;
								explosion6.Play(0.5f, 0.0f, this.convertPositionToPanValue(playerObj.position.X));
								//soundBank.PlayCue("explosion6");
							}
                            playerTwo.bulletCollision = true;
                        }
                        if (gameType == gameTypes.defendTheCrystal)
                        {
                            playerTwo.velocity.X += projectileObj.direction.X * knockStrength * 60f;
                            playerTwo.velocity.Y -= projectileObj.direction.Y * knockStrength * 60f;
                        }

                    }
                }
                if (playerThree.hasStarted)
                {
                    //if we are not player three and we hit player three
                    if (playerThree.rectangle.Intersects(projectileObj.rectangle) && playerThree.shieldUp == false && !(playerObj.Equals(playerThree)))
                    {
                        result = true;
                        if (gameType == gameTypes.deathMatch)
                        {
                            if (playerThree.bulletCollision == false)
                            {
                                playerObj.score += 25;
								explosion6.Play(0.5f, 0.0f, this.convertPositionToPanValue(playerObj.position.X));
								//soundBank.PlayCue("explosion6");
                            }
                            playerThree.bulletCollision = true;
                        }
                        if (gameType == gameTypes.defendTheCrystal)
                        {
                            playerThree.velocity.X += projectileObj.direction.X * knockStrength * 60f;
                            playerThree.velocity.Y -= projectileObj.direction.Y * knockStrength * 60f;
                        }

                    }
                }
                if (playerFour.hasStarted)
                {
                    //if we are not player four and we hit player four
                    if (playerFour.rectangle.Intersects(projectileObj.rectangle) && playerFour.shieldUp == false && !(playerObj.Equals(playerFour)))
                    {
                        result = true;
                        if (gameType == gameTypes.deathMatch)
                        {
                            if (playerFour.bulletCollision == false)
                            {
                                playerObj.score += 25;
								explosion6.Play(0.5f, 0.0f, this.convertPositionToPanValue(playerObj.position.X));
								//soundBank.PlayCue("explosion6");
                            }
                            playerFour.bulletCollision = true;
                        }
                        if (gameType == gameTypes.defendTheCrystal)
                        {
                            playerFour.velocity.X += projectileObj.direction.X * knockStrength * 60f;
                            playerFour.velocity.Y -= projectileObj.direction.Y * knockStrength * 60f;
                        }
                    }
                }
            }
            return result;
        }

		/// <summary>
		/// collision detection between one players bullet and anothers shield
		/// </summary>
		/// <param name="projectileObj"></param>
		/// <param name="playerObj"></param>
		/// <returns></returns>
        bool checkPlayerShieldBulletCollision(Projectile projectileObj, Player playerObj)
        {
            if (playerOne.hasStarted)
            {
                //if we are not player one and we hit player one with shield up
                if (playerOne.shieldRectangle.Intersects(projectileObj.rectangle) && playerOne.shieldUp == true && !(playerObj.Equals(playerOne)))
                {
                    // Check for per pixle collision
                    if (perPixleCollisionDetection(projectileObj.rectangle, projectileObj.spriteTextureData, playerOne.shieldRectangle, playerOne.shieldTextureData))
                    {
                        projectileObj.direction = findDirection(projectileObj.position, playerOne.shieldCenterPosition);
                        projectileObj.direction.Y *= -1;
                        return true;
                    }
                }
            }
            if (playerTwo.hasStarted)
            {
                //if we are not player two and we hit player one with shield up
                if (playerTwo.shieldRectangle.Intersects(projectileObj.rectangle) && playerTwo.shieldUp == true && !(playerObj.Equals(playerTwo)))
                {
                    // Check for per pixle collision
                    if (perPixleCollisionDetection(projectileObj.rectangle, projectileObj.spriteTextureData, playerTwo.shieldRectangle, playerTwo.shieldTextureData))
                    {
                        //Vector2 outFromCenterVector = findDirection(projectileObj.position, playerTwo.shieldCenterPosition);
                        //Vector2 reflectVec = new Vector2(outFromCenterVector.Y * -1, outFromCenterVector.X);
                        //projectileObj.direction = Vector2.Reflect(projectileObj.direction, reflectVec);
                        //projectileObj.hasHitShield = true;
                        projectileObj.direction = findDirection(projectileObj.position, playerTwo.shieldCenterPosition);
                        projectileObj.direction.Y *= -1;
                        return true;
                    }
                }
            }

            //if we are not player three and we hit player one with shield up
            if (playerThree.hasStarted)
            {
                if (playerThree.shieldRectangle.Intersects(projectileObj.rectangle) && playerThree.shieldUp == true && !(playerObj.Equals(playerThree)))
                {
                    // Check for per pixle collision
                    if (perPixleCollisionDetection(projectileObj.rectangle, projectileObj.spriteTextureData, playerThree.shieldRectangle, playerThree.shieldTextureData))
                    {
                        projectileObj.direction = findDirection(projectileObj.position, playerThree.shieldCenterPosition);
                        projectileObj.direction.Y *= -1;
                        return true;
                    }
                }
            }

            if (playerFour.hasStarted)
            {
                //if we are not player four and we hit player one with shield up
                if (playerFour.shieldRectangle.Intersects(projectileObj.rectangle) && playerFour.shieldUp == true && !(playerObj.Equals(playerFour)))
                {
                    // Check for per pixle collision
                    if (perPixleCollisionDetection(projectileObj.rectangle, projectileObj.spriteTextureData, playerFour.shieldRectangle, playerFour.shieldTextureData))
                    {
                        projectileObj.direction = findDirection(projectileObj.position, playerFour.shieldCenterPosition);
                        projectileObj.direction.Y *= -1;
                        return true;
                    }
                }
            }
            return false;
        }

        bool checkEnemyBulletCollision( Projectile projectileObj)
        {
            bool result = false;
            foreach (Kamikaze kamikazeObj in kamikazes)
            {
                if (kamikazeObj.rectangle.Intersects(projectileObj.rectangle) && (!(kamikazeObj.bulletCollision || kamikazeObj.playerCollision)))
                {
                    result = true;
                    if (kamikazeObj.bulletCollision == false) // play the sound only once
                        Explosion.Play(0.5f, 0.0f, this.convertPositionToPanValue(kamikazeObj.position.X));
                    kamikazeObj.bulletCollision = true;    
                    break;
                }
            }
            foreach (CrystalGrabber crystalGrabber in crystalGrabbers)
            {
                if (crystalGrabber.rectangle.Intersects(projectileObj.rectangle) && (!(crystalGrabber.bulletCollision || crystalGrabber.playerCollision)))
                {
                    result = true;
                    if (crystalGrabber.bulletCollision == false) // play the sound only once
                        Explosion.Play(0.5f, 0.0f, this.convertPositionToPanValue(crystalGrabber.position.X));
                    crystalGrabber.bulletCollision = true;
                    break;
                }
            }
            foreach (WobbleKamikaze wk in wobbleKamikazes)
            {
                if (wk.rectangle.Intersects(projectileObj.rectangle) && (!(wk.bulletCollision || wk.playerCollision)))
                {
                    result = true;
                    if (wk.bulletCollision == false) // play the sound only once
                        Explosion.Play(0.5f, 0.0f, this.convertPositionToPanValue(wk.position.X));
                    wk.bulletCollision = true;
                    break;
                }
            }
            return result;
        }

        ///<summary>
        /// checkPlayerPlayerCollision handles checking weather a player object has hit any other player
        ///</summary>
        void checkPlayerPlayerCollision( Player playerObjOne, Player playerObjTwo )
        {
            if (!(playerObjOne == null) && (!(playerObjTwo == null)))
            {
                if (playerObjOne.shieldUp == false && playerObjTwo.shieldUp == false)
                {
                    if (playerObjOne.rectangle.Intersects(playerObjTwo.rectangle))
                    {
                        //if (perPixleCollisionDetection(playerObjOne.rectangle,
                        playerObjOne.playerCollision = true;
                        playerObjOne.lives--;
                        playerObjTwo.playerCollision = true;
                        playerObjTwo.lives--;
                        if (playerObjOne.headExplodeCounter < 1)
                        {
							explosion6.Play(0.5f, 0.0f, this.convertPositionToPanValue(playerObjOne.position.X));
							//soundBank.PlayCue("explosion6");
                        }
                    }
                }
            }
        }

        void checkPlayerSpriteCollision(Player playerObj, Sprite spriteObj)
        {
            if (!(playerObj == null))
            {
                if (playerObj.shieldUp == false)
                {
                    if (playerObj.rectangle.Intersects(spriteObj.rectangle) && (!(spriteObj.playerCollision || spriteObj.bulletCollision)) && playerObj.playerCollision == false)
                    {
                        playerObj.playerCollision = true;
                        playerObj.lives--;
                        if (playerObj.headExplodeCounter < 1)
							explosion6.Play(0.5f, 0.0f, this.convertPositionToPanValue(playerObj.position.X));
						spriteObj.playerCollision = true;
                    }
                }
                if (playerObj.shieldUp == true)
                {
                    if (playerObj.rectangle.Intersects(spriteObj.rectangle) && (!(spriteObj.playerCollision || spriteObj.bulletCollision)))
                    {
						explosion6.Play(0.5f, 0.0f, this.convertPositionToPanValue(playerObj.position.X));
						spriteObj.playerCollision = true;
                    }
                }
            }
        }

        /// <summary>
        /// This handles removing any sprites that haved moved off the screen
        /// </summary>
        void destroyOffScreenSprites(List<Projectile> projectiles)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                // Remove any projectiles that have moved out of the visible area
                if (projectiles[i].position.X > graphics.GraphicsDevice.Viewport.Width + 300)
                {
                    projectiles.RemoveAt(i);
                    break;
                }
                else if (projectiles[i].position.Y > graphics.GraphicsDevice.Viewport.Height + 300)
                {
                    projectiles.RemoveAt(i);
                    break;
                }
                else if (projectiles[i].position.X < -300)
                {
                    projectiles.RemoveAt(i);
                    break;
                }
                else if (projectiles[i].position.Y < -300)
                {
                    projectiles.RemoveAt(i);
                    break;
                }
            }
        }

        void destroyOffScreenPowerUps(List<PowerUp> powerUps)
        {
            for (int i = 0; i < powerUps.Count; i++)
            {
                // Remove any powerUps that have moved out of the visible area
                if (powerUps[i].position.X > graphics.GraphicsDevice.Viewport.Width + 200)
                {
                    powerUps.RemoveAt(i);
                    break;
                }
                else if (powerUps[i].position.Y > graphics.GraphicsDevice.Viewport.Height + 200)
                {
                    powerUps.RemoveAt(i);
                    break;
                }
                else if (powerUps[i].position.X < -200)
                {
                    powerUps.RemoveAt(i);
                    break;
                }
                else if (powerUps[i].position.Y < -200)
                {
                    powerUps.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// This handles removing powerUp settings after they timeout
        /// </summary>
        void powerUpTiming(Player playerObj)
        {
            if (playerObj != null)
            {
                // handle the timeout for the pepperpowerup
                if (playerObj.PepperPowerup == true)
                {
                    if (!(playerObj.pepperPowerUpTimeoutTimer == null)) // check for null(uninstantiated) timer object 
                    {
                        if (playerObj.pepperPowerUpTimeoutTimer.expired(gameTime1) == true)
                        {
                            playerObj.PepperPowerup = false;

                            // subtract powerup modifiers from player settings 
                            playerObj.projectileFrequencyTimer.timerDuration -= PepperPowerUp.projectileFrequencyModifier;
                            playerObj.speed -= PepperPowerUp.playerSpeedModifier;
                            playerObj.projectileSpeed -= PepperPowerUp.projectileSpeedModifier;
                        }
                    }
                }

                // Handle the timeout for the speed power up
                if (playerObj.SpeedPowerUp == true)
                {
                    if (!(playerObj.speedPowerUpTimeoutTimer == null)) // check for null(uninstantiated) timer object 
                    {
                        if (playerObj.speedPowerUpTimeoutTimer.expired(gameTime1) == true)
                        {
                            playerObj.SpeedPowerUp = false;

                            // subtract powerup modifiers from player settings 
                            playerObj.speed -= SpeedPowerUp.playerSpeedModifier;
                        }
                    }
                }

                // handle the timeout for the InvincibleBullet powerup
                if (playerObj.InvincBulletPowerUp == true)
                {
                    if (!(playerObj.invincibleBulletPowerUpTimeoutTimer == null)) // check for null(uninstantiated) timer object 
                    {
                        if (playerObj.invincibleBulletPowerUpTimeoutTimer.expired(gameTime1) == true)
                        {
                            playerObj.InvincBulletPowerUp = false;

                            // subtract powerup modifiers from player settings 
                            playerObj.projectileFrequencyTimer.timerDuration -= InvincibleBulletPowerUp.projectileFrequencyModifier;
                            playerObj.speed -= InvincibleBulletPowerUp.playerSpeedModifier;
                            playerObj.projectileSpeed -= InvincibleBulletPowerUp.projectileSpeedModifier;
                        }
                    }
                }
            }
        }

        void dropTheSuperBomb()
        {
            // destroy all 
            foreach (Kamikaze kamikaze in kamikazes)
                kamikaze.bulletCollision = true;
            foreach (CrystalGrabber crystalGrabber in crystalGrabbers)
                crystalGrabber.bulletCollision = true;
            foreach (WobbleKamikaze w in wobbleKamikazes)
                w.bulletCollision = true;
        }

        void KamikazeMovement()
        {
            foreach(Kamikaze kamikaze in kamikazes)
            {
                Player playerObj = FindNearistPlayer(kamikaze);
                kamikaze.direction = findDirection(playerObj.position, kamikaze.position);
                kamikaze.position.X += kamikaze.direction.X * kamikaze.speed;    
                kamikaze.position.Y += kamikaze.direction.Y * kamikaze.speed;
            }
        }

        void wobbleKamikazeMovement()
        {
            foreach (WobbleKamikaze wKamikaze in wobbleKamikazes)
            {
                Player playerObj = FindNearistPlayer(wKamikaze);
                wKamikaze.direction = findDirection(playerObj.position, wKamikaze.position);
                wKamikaze.position.X += wKamikaze.direction.X * wKamikaze.speed;
                wKamikaze.position.Y += wKamikaze.direction.Y * wKamikaze.speed;
                // do wobble
                wKamikaze.position.X += ((float)Math.Cos(wKamikaze.wobbleRad) * wKamikaze.speed * 1.5f);
                wKamikaze.position.Y += ((float)Math.Sin(wKamikaze.wobbleRad) * wKamikaze.speed * 1.5f);
                wKamikaze.wobbleRad += 0.2;
                wKamikaze.wobbleRad = wKamikaze.wobbleRad % (2 * Math.PI);
            }
        }

        void CrystalGrabberMovement()
        {
            foreach (CrystalGrabber crystalGrabber in crystalGrabbers)
            {

                crystalGrabber.direction = findDirection(powerCrystal.position + crystalGrabber.offsetToGrabCrystal, crystalGrabber.position);
                if (crystalGrabber.hasGrabbedCrystal == false)
                {
                    crystalGrabber.position.X += crystalGrabber.direction.X * crystalGrabber.speed;
                    crystalGrabber.position.Y += crystalGrabber.direction.Y * crystalGrabber.speed;
					//if ((int)crystalGrabber.position.Y == (int)powerCrystal.position.Y && (int)crystalGrabber.position.X == (int)powerCrystal.position.X)
					//    crystalGrabber.hasGrabbedCrystal = true;
					if ((int)crystalGrabber.position.Y >= (int)powerCrystal.position.Y - 10 + crystalGrabber.offsetToGrabCrystal.Y 
                        && (int)crystalGrabber.position.Y <= (int)powerCrystal.position.Y + 10 + crystalGrabber.offsetToGrabCrystal.Y 
                        && (int)crystalGrabber.position.X >= (int)powerCrystal.position.X - 10 + crystalGrabber.offsetToGrabCrystal.X 
                        && (int)crystalGrabber.position.X <= (int)powerCrystal.position.X + 10 + crystalGrabber.offsetToGrabCrystal.X)
					{
						crystalGrabber.hasGrabbedCrystal = true;
					}

                }
                if (crystalGrabber.hasGrabbedCrystal == true)
                {
                    crystalGrabber.position = powerCrystal.position + crystalGrabber.offsetToGrabCrystal;
                    //Drag the crystal away through the top of the screen
                    powerCrystal.position.Y -= powerCrystal.speed;
                }
                
            }
        }

        void CheckForCrystalOffScreen()
        {
            if (powerCrystal.position.Y <= 0 - powerCrystal.sprite.Height)
                gameOver = true;
        }

        Player FindNearistPlayer( Sprite spriteObj)
        {
            float distanceToPlayerOne;
            float distanceToPlayerTwo;
            float distanceToPlayerThree;
            float distanceToPlayerFour;

            if (playerOne.hasStarted)
                distanceToPlayerOne = System.Math.Abs(spriteObj.position.X - playerOne.position.X) + System.Math.Abs(spriteObj.position.Y - playerOne.position.Y);
            else 
                distanceToPlayerOne = 90000;
            if (playerTwo.hasStarted)
                distanceToPlayerTwo = System.Math.Abs(spriteObj.position.X - playerTwo.position.X) + System.Math.Abs(spriteObj.position.Y - playerTwo.position.Y);
            else 
                distanceToPlayerTwo = 90000;
            if (playerThree.hasStarted)
                distanceToPlayerThree = System.Math.Abs(spriteObj.position.X - playerThree.position.X) + System.Math.Abs(spriteObj.position.Y - playerThree.position.Y);
            else 
                distanceToPlayerThree = 90000;
            if (playerFour.hasStarted)
                distanceToPlayerFour = System.Math.Abs(spriteObj.position.X - playerFour.position.X) + System.Math.Abs(spriteObj.position.Y - playerFour.position.Y);
            else 
                distanceToPlayerFour = 90000;

            if (distanceToPlayerOne < distanceToPlayerTwo && distanceToPlayerOne < distanceToPlayerThree && distanceToPlayerOne < distanceToPlayerFour)
                return playerOne;
            if (distanceToPlayerTwo < distanceToPlayerOne && distanceToPlayerTwo < distanceToPlayerThree && distanceToPlayerTwo < distanceToPlayerFour)
                return playerTwo;
            if (distanceToPlayerThree < distanceToPlayerTwo && distanceToPlayerThree < distanceToPlayerTwo && distanceToPlayerThree < distanceToPlayerFour)
                return playerThree;
            if (distanceToPlayerFour < distanceToPlayerTwo && distanceToPlayerFour < distanceToPlayerThree && distanceToPlayerFour < distanceToPlayerOne)
                return playerFour;
            else return playerOne;

        }

        void updatePowerUpPositions()
        {
            foreach (PepperPowerUp pepperPowerU in pepperPowerUps)
            {
                pepperPowerU.position.X += pepperPowerU.direction.X * pepperPowerU.speed;
                pepperPowerU.position.Y += pepperPowerU.direction.Y * pepperPowerU.speed;
            }

            foreach (InvincibleBulletPowerUp invincibleBulletPowerUp in InvincibleBulletPowerUps)
            {
                invincibleBulletPowerUp.position.X += invincibleBulletPowerUp.direction.X * invincibleBulletPowerUp.speed;
                invincibleBulletPowerUp.position.Y += invincibleBulletPowerUp.direction.Y * invincibleBulletPowerUp.speed;
            }

            foreach (SuperBombPowerUp superBombPowerUp in superBombPowerUps)
            {
                superBombPowerUp.position.X += superBombPowerUp.direction.X * superBombPowerUp.speed;
                superBombPowerUp.position.Y += superBombPowerUp.direction.Y * superBombPowerUp.speed;
            }

            foreach (ShieldPowerUp shieldPowerUp in shieldPowerUps)
            {
                shieldPowerUp.position.X += shieldPowerUp.direction.X * shieldPowerUp.speed;
                shieldPowerUp.position.Y += shieldPowerUp.direction.Y * shieldPowerUp.speed;
            }
            foreach (SpeedPowerUp speedPowerUp in speedPowerUps)
            {
                speedPowerUp.position.X += speedPowerUp.direction.X * speedPowerUp.speed;
                speedPowerUp.position.Y += speedPowerUp.direction.Y * speedPowerUp.speed;
            }
            foreach (ExtraLifePowerUp extraLifePowerUp in extraLifePowerUps)
            {
                extraLifePowerUp.position.X += extraLifePowerUp.direction.X * extraLifePowerUp.speed;
                extraLifePowerUp.position.Y += extraLifePowerUp.direction.Y * extraLifePowerUp.speed;
            }
        }

        /// <summary>
        /// This handles restraining sprites to viewport area
        /// </summary>
        void HandleSpriteBoundries(Player playerObj)
        {
            if (!(playerObj == null))
            {
                int MaxX = graphics.GraphicsDevice.Viewport.Width - playerObj.sprite.Width;
                int MinX = 0;
                int MaxY = graphics.GraphicsDevice.Viewport.Height - playerObj.sprite.Height;
                int MinY = 0;

                // Stop outward momentum when the player reaches a screen edge.
                if (playerObj.position.X < MinX)
                {
                    playerObj.position.X = MinX;
                    if (playerObj.velocity.X < 0) playerObj.velocity.X = 0;
                }
                else if (playerObj.position.X > MaxX)
                {
                    playerObj.position.X = MaxX;
                    if (playerObj.velocity.X > 0) playerObj.velocity.X = 0;
                }

                if (playerObj.position.Y < MinY)
                {
                    playerObj.position.Y = MinY;
                    if (playerObj.velocity.Y < 0) playerObj.velocity.Y = 0;
                }
                else if (playerObj.position.Y > MaxY)
                {
                    playerObj.position.Y = MaxY;
                    if (playerObj.velocity.Y > 0) playerObj.velocity.Y = 0;
                }
            }
        }

        /// <summary>
        /// This handles finding the slope bewtween two points and the direction to travel along that slope to get to the 
        /// destanation point
        /// The slope is returned as a normalised vector
        /// </summary>
        /// <param name="destinatoinPoint">sense</param>
        /// <param name="originPoint">inital point</param>
        /// <returns>A normalised vector indicating direction</returns>
        Vector2 findDirection(Vector2 destinatoinPoint, Vector2 originPoint)
        {
            Vector2 direction = new Vector2();
            float directionY = destinatoinPoint.Y - originPoint.Y;
            float directionX = destinatoinPoint.X - originPoint.X;

            direction = new Vector2(directionX, directionY);

            // Turn vector into a unit circle type vector with a length of 1 - this allows for keeping track of direction only
            direction.Normalize();
            return direction;
        }

        ///// <summary>
        ///// This handles finding the slope bewtween two points and the direction to travel along that slope to get to the 
        ///// destanation point
        ///// </summary>
        //Vector2 findDirection(Vector2 destinatoinPoint, Vector2 originPoint)
        //{
        //    Vector2 direction = new Vector2();
        //    float directionY = destinatoinPoint.Y - originPoint.Y;
        //    float directionX = destinatoinPoint.X - originPoint.X;
        //    float reducedDirectionY = 0;
        //    float reducedDirectionX = 0;

        //    if (directionY == 0 && directionX > 0)
        //        return new Vector2(0, 1);
        //    if (directionY == 0 && directionX < 0)
        //        return new Vector2(0, -1);
        //    if (directionX == 0 && directionY < 0)
        //        return new Vector2(-1, 0);
        //    if (directionX == 0 && directionY > 0)
        //        return new Vector2(1, 0);

        //    if (System.Math.Abs(directionY) > System.Math.Abs(directionX))
        //    {
        //        reducedDirectionY = 1;
        //        reducedDirectionX = directionX / directionY;
        //    }

        //    if (System.Math.Abs(directionY) < System.Math.Abs(directionX))
        //    {
        //        reducedDirectionX = 1;
        //        reducedDirectionY = directionY / directionX;
        //    }

        //    if (destinatoinPoint.X > originPoint.X)
        //        direction.X = System.Math.Abs(reducedDirectionX);
        //    if (destinatoinPoint.X < originPoint.X)
        //        direction.X = (System.Math.Abs(reducedDirectionX)) * -1;

        //    if (destinatoinPoint.Y > originPoint.Y)
        //        direction.Y = System.Math.Abs(reducedDirectionY);
        //    if (destinatoinPoint.Y < originPoint.Y)
        //        direction.Y = (System.Math.Abs(reducedDirectionY)) * -1;

        //    return direction;
        //}

        bool perPixleCollisionDetection(Rectangle rectangleA, Color[] dataA, Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }
            return false;
        } // end perPixleCollisionDetection()

        public void resetGame()
        {
            diffuculityAdder = 0;
            finalScoreMessageboxHasBeenShown = false;

            kamikazes.Clear();
            crystalGrabbers.Clear();
            wobbleKamikazes.Clear();
			pepperPowerUps.Clear();
			InvincibleBulletPowerUps.Clear();
			superBombPowerUps.Clear();
			shieldPowerUps.Clear();
			speedPowerUps.Clear();
			extraLifePowerUps.Clear();
			kamikazes.Clear();
			crystalGrabbers.Clear();
			players.Clear();

			playerOne.hasStarted = false;
			playerTwo.hasStarted = false;
			playerThree.hasStarted = false;
			playerFour.hasStarted = false;

			playerOne.resetToIniValues();
			playerTwo.resetToIniValues();
			playerThree.resetToIniValues();
			playerFour.resetToIniValues();

			wobbleKSpanRate = wobbleKSpanRateDefault;
			kamikazeSpawnRate = kamikazeSpawnRateDefault;
			crystalGrabberSpawnRate = crystalGrabberSpawnRateDefault;
			pepperPowerUpSpawnRate = pepperPowerUpSpawnRateDefault;
			invincibleBulletPowerUpSpawnRate = invincibleBulletPowerUpSpawnRateDefault;
			superBombPowerUpSpawnRate = superBombPowerUpSpawnRateDefault;
			shieldPowerUpSpawnRate = shieldPowerUpSpawnRateDefault;
			speedPowerUpSpawnRate = speedPowerUpSpawnRateDefault;
			extraLifePowerUpSpawnRate = extraLifePowerUpSpawnRateDefault;

			powerCrystal.CenterPosition = this.centerOfScreen;
        }

    } // end class
}
