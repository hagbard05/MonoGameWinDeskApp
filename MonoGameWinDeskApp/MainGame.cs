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
	/// Handles operations in the game loop 
	/// </summary>
	public class MainGame : Microsoft.Xna.Framework.Game
	{
		SpriteFont gameFont;
		Random random2, random3, random4, random5, random6;
		bool playerOneStarted = false, playerTwoStarted = false, playerThreeStarted = false, playerFourStarted = false;
		double diffuculityAdder = 0;
		int kamikazeSpawnRate = 8,
			crystalGrabberSpawnRate = 10,
			pepperPowerUpSpawnRate = 8,
			invincibleBulletPowerUpSpawnRate = 4,
			superBombPowerUpSpawnRate = 8,
			shieldPowerUpSpawnRate = 7,
			speedPowerUpSpawnRate = 6,
			extraLifePowerUpSpawnRate = 3;
		bool finalScoreMessageboxHasBeenShown = false;
		GamePadState previousGamePadState1, previousGamePadState2, previousGamePadState3, previousGamePadState4;
		protected bool _gameOver;
		public bool gameOver
		{
			get
			{
				return _gameOver;
			}
			set
			{
				if (value == true)
				{
					finalScoreMessageboxHasBeenShown = false;

					kamikazes.Clear();
					crystalGrabbers.Clear();

					playerOneStarted = false;
					playerTwoStarted = false;
					playerThreeStarted = false;
					playerFourStarted = false;

					playerOne.resetToIniValues();
					playerTwo.resetToIniValues();
					playerThree.resetToIniValues();
					playerFour.resetToIniValues();

					diffuculityAdder = 0;
					kamikazeSpawnRate = 8;
					crystalGrabberSpawnRate = 10;
					pepperPowerUpSpawnRate = 8;
					invincibleBulletPowerUpSpawnRate = 4;
					superBombPowerUpSpawnRate = 8;
					shieldPowerUpSpawnRate = 7;
					speedPowerUpSpawnRate = 6;
					extraLifePowerUpSpawnRate = 3;

					pepperPowerUps.Clear();
					InvincibleBulletPowerUps.Clear();
					superBombPowerUps.Clear();
					shieldPowerUps.Clear();
					speedPowerUps.Clear();
					extraLifePowerUps.Clear();

					kamikazes.Clear();
					crystalGrabbers.Clear();
					powerCrystal.position = centerOfScreen;
				}
				_gameOver = value;
			}
		}

		public enum gameTypes { defendTheCrystal, deathMatch, none };
		gameTypes gameType = gameTypes.defendTheCrystal;

		int headExplodeDownTime = 59; //Number of cycles to show the explosion. default gameTime: 60 = 1 second = 0 to 59 = 1 second
		float elapsed; // used to track elapsed time for sprite anamation
		GameTime gameTime1;
		Viewport viewport;
		GraphicsDeviceManager graphics;
		AudioEngine audioEngine;
		SoundBank soundBank;
		WaveBank waveBank;
		SpriteBatch spriteBatch;
		Color backColor = Color.Black; //background color
		Vector2 centerOfScreen;

		// Color array data for the images - used for per-pixel collision
		// ToDo: finish setting up per pixle collisions
		Color[] forestSpriteTextureData;
		Color[] projectileSpriteTextureData;

		Player playerOne;
		Player playerTwo;
		Player playerThree;
		Player playerFour;

		public PowerCrystal powerCrystal;

		List<PowerUp> pepperPowerUps = new List<PowerUp>();
		List<PowerUp> InvincibleBulletPowerUps = new List<PowerUp>();
		List<PowerUp> superBombPowerUps = new List<PowerUp>();
		List<PowerUp> shieldPowerUps = new List<PowerUp>();
		List<PowerUp> speedPowerUps = new List<PowerUp>();
		List<PowerUp> extraLifePowerUps = new List<PowerUp>();

		List<Kamikaze> kamikazes = new List<Kamikaze>();
		List<CrystalGrabber> crystalGrabbers = new List<CrystalGrabber>();

		// The sub-rectangle of the drawable area which should be visible on all TVs
		Rectangle safeBounds;

		// Percentage of the screen on every side is the safe area
		const float SafeAreaPortion = 0.05f;


		public MainGame()
		{
			_gameOver = true;
			graphics = new GraphicsDeviceManager(this);
			if (GraphicsAdapter.DefaultAdapter.IsWideScreen)
			{
				graphics.PreferredBackBufferWidth = 1920;
				graphics.PreferredBackBufferHeight = 1080;
			}
			else
			{
				graphics.PreferredBackBufferWidth = 1280;
				graphics.PreferredBackBufferHeight = 720;
			}

			graphics.IsFullScreen = false;
			Content.RootDirectory = "Content";
			//TargetElapsedTime = new TimeSpan(20000);
		}

		private static int _randomSeed = 0;
		public static int randomSeed
		{
			get
			{
				if (_randomSeed > 10523)
					_randomSeed = 0;
				return _randomSeed++;
			}
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic related content.  
		/// Calling base.Initialize will enumerate through any components and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
			random2 = new Random(randomSeed);
			random3 = new Random(randomSeed);
			random4 = new Random(randomSeed);
			random5 = new Random(randomSeed);
			random6 = new Random(randomSeed);

			//audioEngine = new AudioEngine("Content\\Sounds\\Win\\XACT Sounds.xgs");
			//soundBank = new SoundBank(audioEngine, "Content\\Sounds\\Win\\Sound Bank.xsb");
			//waveBank = new WaveBank(audioEngine, "Content\\Sounds\\Win\\Wave Bank.xwb");

			//soundBank.PlayCue("The Crystal Method_Bad Ass");
		}

		// This is a texture we can render.
		public Texture2D background;
		public Texture2D forestSprite;
		public Texture2D forestSprite2;
		public Texture2D calebSprite;
		public Texture2D headExplodeSprite;
		public Texture2D splatSprite;
		public Texture2D fireBallMetalSphere;
		public Texture2D playerStatusArea;
		public Texture2D spriteOcto;
		public Texture2D spriteOctoLarge;

		public Texture2D debugDot; // one pixle used for position debugging

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Calculate safe bounds based on current resolution
			viewport = graphics.GraphicsDevice.Viewport;

			safeBounds = new Rectangle((int)(viewport.Width * SafeAreaPortion), (int)(viewport.Height * SafeAreaPortion),
				(int)(viewport.Width * (1 - 2 * SafeAreaPortion)), (int)(viewport.Height * (1 - 2 * SafeAreaPortion)));

			gameTime1 = new GameTime();
			centerOfScreen = new Vector2((viewport.Width / 2), (viewport.Height / 2));

			gameFont = Content.Load<SpriteFont>("gameFont");
			//gameFont = Content.Load<SpriteFont>("fonts//MarkerFelt-16");

			background = Content.Load<Texture2D>("DeeperDive");
			calebSprite = Content.Load<Texture2D>("CalebSprite");
			forestSprite2 = Content.Load<Texture2D>("ForestSprite2");
			forestSprite = Content.Load<Texture2D>("ForestSprite");
			spriteOcto = Content.Load<Texture2D>("OctoEnemy");
			fireBallMetalSphere = Content.Load<Texture2D>("fireBallMetelSphere");  //used as a combo sprite for when player has invincibleBullet and Pepper powerups
																				   //projectileSprite = Content.Load<Texture2D>("flameBall");
																				   //headExplodeSprite = Content.Load<Texture2D>("headExplode");
			splatSprite = Content.Load<Texture2D>("splat");
			playerStatusArea = Content.Load<Texture2D>("PlayerStatusArea");

			debugDot = Content.Load<Texture2D>("debugDot");// one pixle used for position debugging

			// Extract collision data - used for per pixle collisions - not yet implemented
			forestSpriteTextureData = new Color[forestSprite2.Width * forestSprite2.Height];
			forestSprite2.GetData(forestSpriteTextureData);
			//projectileSpriteTextureData = new Color[projectileSprite.Width * projectileSprite.Height];

			// Create a new SpriteBatch
			spriteBatch = new SpriteBatch(GraphicsDevice);

			//TestCrystalGrabber = new CrystalGrabber(this, new Vector2(300, 300), new AnimatedTexture(new Vector2(0), Content, "explosionStripForGhost", 12, 12));

			powerCrystal = new PowerCrystal(this, centerOfScreen);

			// Create player objects
			playerOne = new Player(this, gameTime1);
			playerOne.explosion.Load(Content, "explosionStrip2", 12, 12);

			playerTwo = new Player(this, new Vector2(safeBounds.Right - calebSprite.Width, safeBounds.Top), calebSprite, gameTime1);
			playerTwo.explosion.Load(Content, "explosionStrip2", 12, 12);

			playerThree = new Player(this, new Vector2(safeBounds.Left - forestSprite.Width, safeBounds.Bottom - forestSprite.Height), gameTime1);
			playerThree.explosion.Load(Content, "explosionStrip2", 12, 12);

			playerFour = new Player(this, new Vector2(safeBounds.Right - forestSprite2.Width, safeBounds.Bottom - forestSprite2.Height), forestSprite2, gameTime1);
			playerFour.explosion.Load(Content, "explosionStrip2", 12, 12);


		}


		void CheckTheScore()
		{
			int winningScore = 10;
			if ((playerOne.score >= winningScore || playerTwo.score >= winningScore || playerThree.score >= winningScore || playerFour.score >= winningScore) && finalScoreMessageboxHasBeenShown == false)
			{
				//System.Windows.Forms.MessageBox.Show("Game over. Final score: player one: " + playerOne.score + " player two: " + playerTwo.score
				//    + " player three: " + playerThree.score + " player four: " + playerFour.score);
				//finalScoreMessageboxHasBeenShown = true;
			}
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
			Content.Unload();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			gameTime1 = gameTime;
			elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

			// Move the sprites around.
			UpdateInputAndMovement(gameTime);
			//CheckTheScore();

			//audioEngine.Update();
			if (gameOver == false)
			{
				diffuculityAdder += 0.01;
			}
			base.Update(gameTime);
		}

		private void UpdateInputAndMovement(GameTime gameTime)
		{
			GamePadState gamePadStateOne = GamePad.GetState(PlayerIndex.One);
			GamePadState gamePadStateTwo = GamePad.GetState(PlayerIndex.Two);
			GamePadState gamePadStateThree = GamePad.GetState(PlayerIndex.Three);
			GamePadState gamePadStateFour = GamePad.GetState(PlayerIndex.Four);

			// Create player objects
			// poll for start press on controller
			#region pollForStart

			if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed && previousGamePadState1.Buttons.Start == ButtonState.Released && playerOneStarted == false)
			{
				playerOneStarted = true;
				gameOver = false;
			}
			if (GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed && previousGamePadState2.Buttons.Start == ButtonState.Released && playerTwoStarted == false)
			{
				playerTwoStarted = true;
				gameOver = false;
			}
			if (GamePad.GetState(PlayerIndex.Three).Buttons.Start == ButtonState.Pressed && previousGamePadState3.Buttons.Start == ButtonState.Released && playerThreeStarted == false)
			{
				playerThreeStarted = true;
				gameOver = false;
			}
			if (GamePad.GetState(PlayerIndex.Four).Buttons.Start == ButtonState.Pressed && previousGamePadState4.Buttons.Start == ButtonState.Released && playerFourStarted == false)
			{
				playerFourStarted = true;
				gameOver = false;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.A))
			{
				playerOneStarted = true;
				gameOver = false;
			}

			#endregion

			if (gamePadStateOne.Buttons.Back == ButtonState.Pressed && playerOneStarted == true)
				this.Exit();
			if (gamePadStateTwo.Buttons.Back == ButtonState.Pressed && playerTwoStarted == true)
				this.Exit();
			if (gamePadStateThree.Buttons.Back == ButtonState.Pressed && playerThreeStarted == true)
				this.Exit();
			if (gamePadStateFour.Buttons.Back == ButtonState.Pressed && playerFourStarted == true)
				this.Exit();

			if (playerOne.lives < 0 && playerTwo.lives < 0 && playerThree.lives < 0 && playerFour.lives < 0)
			{
				gameOver = true;
			}

			if (playerOneStarted && playerOne.lives > 0)
			{
				PlayerMovement(playerOne, gamePadStateOne);
				PlayerShooting(playerOne, gamePadStateOne);
				PlayerTiming(playerOne);
			}
			if (playerTwoStarted && playerTwo.lives > 0)
			{
				PlayerMovement(playerTwo, gamePadStateTwo);
				PlayerShooting(playerTwo, gamePadStateTwo);
				PlayerTiming(playerTwo);
			}
			if (playerThreeStarted && playerThree.lives > 0)
			{
				PlayerMovement(playerThree, gamePadStateThree);
				PlayerShooting(playerThree, gamePadStateThree);
				PlayerTiming(playerThree);
			}
			if (playerFourStarted && playerFour.lives < 0)
			{
				PlayerMovement(playerFour, gamePadStateFour);
				PlayerShooting(playerFour, gamePadStateFour);
				PlayerTiming(playerFour);
			}

			if (!(playerOne == null))
				destroyOffScreenSprites(playerOne.projectiles);
			if (!(playerTwo == null))
				destroyOffScreenSprites(playerTwo.projectiles);
			if (!(playerThree == null))
				destroyOffScreenSprites(playerThree.projectiles);
			if (!(playerFour == null))
				destroyOffScreenSprites(playerFour.projectiles);

			if (gameOver == false)
			{

				if (gameType == gameTypes.deathMatch)
				{
					checkPlayerPlayerCollision(playerOne, playerTwo);
					checkPlayerPlayerCollision(playerOne, playerThree);
					checkPlayerPlayerCollision(playerOne, playerFour);
					checkPlayerPlayerCollision(playerTwo, playerThree);
					checkPlayerPlayerCollision(playerTwo, playerFour);
					checkPlayerPlayerCollision(playerThree, playerFour);
				}

				spawnKamikazesAboveScreen(kamikazeSpawnRate + (int)diffuculityAdder);  // spawn enemys above screen
				spawnKamikazesBelowScreen(kamikazeSpawnRate + (int)diffuculityAdder);
				spawnKamikazesLeftOfScreen(kamikazeSpawnRate + (int)diffuculityAdder);
				spawnKamikazesRightOfScreen(kamikazeSpawnRate + (int)diffuculityAdder);

				spawnCrystalGrabbersAboveScreen(crystalGrabberSpawnRate + (int)diffuculityAdder);
				spawnCrystalGrabbersBelowScreen(crystalGrabberSpawnRate + (int)diffuculityAdder);
				spawnCrystalGrabbersRightOfScreen(crystalGrabberSpawnRate + (int)diffuculityAdder);
				spawnCrystalGrabbersLeftOfScreen(crystalGrabberSpawnRate + (int)diffuculityAdder);

				CheckForCrystalOffScreen();

				KamikazeMovement();
				CrystalGrabberMovement();

				foreach (Kamikaze kamikazeObj in kamikazes)
				{
					checkPlayerSpriteCollision(playerOne, kamikazeObj);
					checkPlayerSpriteCollision(playerTwo, kamikazeObj);
					checkPlayerSpriteCollision(playerThree, kamikazeObj);
					checkPlayerSpriteCollision(playerFour, kamikazeObj);
					if (kamikazeTiming(kamikazeObj))
						break;
				}

				foreach (CrystalGrabber crystalGrabber in crystalGrabbers)
				{
					checkPlayerSpriteCollision(playerOne, crystalGrabber);
					checkPlayerSpriteCollision(playerTwo, crystalGrabber);
					checkPlayerSpriteCollision(playerThree, crystalGrabber);
					checkPlayerSpriteCollision(playerFour, crystalGrabber);
					if (enemyTiming(crystalGrabber))
						break;
				}

				HandleSpriteBoundries(playerOne);
				HandleSpriteBoundries(playerTwo);
				HandleSpriteBoundries(playerThree);
				HandleSpriteBoundries(playerFour);

				powerUpTiming(playerOne);
				powerUpTiming(playerTwo);
				powerUpTiming(playerThree);
				powerUpTiming(playerFour);

				spawnPowerUp(pepperPowerUpSpawnRate, Sprite.powerUpTypes.Pepper, random2);
				spawnPowerUp(invincibleBulletPowerUpSpawnRate, Sprite.powerUpTypes.InvincibleBullet, random3);
				spawnPowerUp(superBombPowerUpSpawnRate, Sprite.powerUpTypes.SuperBomb, random4);
				spawnPowerUp(shieldPowerUpSpawnRate, Sprite.powerUpTypes.Shield, random5);
				spawnPowerUp(speedPowerUpSpawnRate, Sprite.powerUpTypes.Speed, new Random(randomSeed));
				spawnPowerUp(extraLifePowerUpSpawnRate, Sprite.powerUpTypes.ExtraLife, new Random(randomSeed));

				destroyOffScreenPowerUps(pepperPowerUps);
				destroyOffScreenPowerUps(InvincibleBulletPowerUps);
				destroyOffScreenPowerUps(superBombPowerUps);
				destroyOffScreenPowerUps(shieldPowerUps);
				destroyOffScreenPowerUps(speedPowerUps);
				destroyOffScreenPowerUps(extraLifePowerUps);

				foreach (PepperPowerUp pepperPowerU in pepperPowerUps)
				{
					bool powerUpHasBeenGrabbedResult;
					powerUpHasBeenGrabbedResult = powerUpHandleing(playerOne, pepperPowerU);
					if (powerUpHasBeenGrabbedResult == true)
					{
						pepperPowerUps.Remove(pepperPowerU);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerTwo, pepperPowerU);
					if (powerUpHasBeenGrabbedResult == true)
					{
						pepperPowerUps.Remove(pepperPowerU);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerThree, pepperPowerU);
					if (powerUpHasBeenGrabbedResult == true)
					{
						pepperPowerUps.Remove(pepperPowerU);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerFour, pepperPowerU);
					if (powerUpHasBeenGrabbedResult == true)
					{
						pepperPowerUps.Remove(pepperPowerU);
						break;
					}
				}

				foreach (InvincibleBulletPowerUp invincibleBulletPowerUp in InvincibleBulletPowerUps)
				{
					bool powerUpHasBeenGrabbedResult;
					powerUpHasBeenGrabbedResult = powerUpHandleing(playerOne, invincibleBulletPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						InvincibleBulletPowerUps.Remove(invincibleBulletPowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerTwo, invincibleBulletPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						InvincibleBulletPowerUps.Remove(invincibleBulletPowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerThree, invincibleBulletPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						InvincibleBulletPowerUps.Remove(invincibleBulletPowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerFour, invincibleBulletPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						InvincibleBulletPowerUps.Remove(invincibleBulletPowerUp);
						break;
					}
				}

				foreach (SuperBombPowerUp superBombPowerUp in superBombPowerUps)
				{
					bool powerUpHasBeenGrabbedResult;
					powerUpHasBeenGrabbedResult = powerUpHandleing(playerOne, superBombPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						superBombPowerUps.Remove(superBombPowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerTwo, superBombPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						superBombPowerUps.Remove(superBombPowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerThree, superBombPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						superBombPowerUps.Remove(superBombPowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerFour, superBombPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						superBombPowerUps.Remove(superBombPowerUp);
						break;
					}
				}

				foreach (ShieldPowerUp shieldPowerUp in shieldPowerUps)
				{
					bool powerUpHasBeenGrabbedResult;
					powerUpHasBeenGrabbedResult = powerUpHandleing(playerOne, shieldPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						shieldPowerUps.Remove(shieldPowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerTwo, shieldPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						shieldPowerUps.Remove(shieldPowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerThree, shieldPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						shieldPowerUps.Remove(shieldPowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerFour, shieldPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						shieldPowerUps.Remove(shieldPowerUp);
						break;
					}
				}

				foreach (SpeedPowerUp speedPowerUp in speedPowerUps)
				{
					bool powerUpHasBeenGrabbedResult;
					powerUpHasBeenGrabbedResult = powerUpHandleing(playerOne, speedPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						speedPowerUps.Remove(speedPowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerTwo, speedPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						speedPowerUps.Remove(speedPowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerThree, speedPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						speedPowerUps.Remove(speedPowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerFour, speedPowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						speedPowerUps.Remove(speedPowerUp);
						break;
					}
				}

				foreach (ExtraLifePowerUp extraLifePowerUp in extraLifePowerUps)
				{
					bool powerUpHasBeenGrabbedResult;
					powerUpHasBeenGrabbedResult = powerUpHandleing(playerOne, extraLifePowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						extraLifePowerUps.Remove(extraLifePowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerTwo, extraLifePowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						extraLifePowerUps.Remove(extraLifePowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerThree, extraLifePowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						extraLifePowerUps.Remove(extraLifePowerUp);
						break;
					}

					powerUpHasBeenGrabbedResult = powerUpHandleing(playerFour, extraLifePowerUp);
					if (powerUpHasBeenGrabbedResult == true)
					{
						extraLifePowerUps.Remove(extraLifePowerUp);
						break;
					}
				}
				updatePowerUpPositions();

				// Handling Keyboard input
				// PlayerOne Keyboard Input
				KeyboardState newState = Keyboard.GetState();
				if (!(playerOne.bulletCollision == true || playerOne.playerCollision == true))
				{
					// PlayerOne Movement
					if (newState.IsKeyDown(Keys.Right))
						playerOne.position.X += playerOne.speed;
					if (newState.IsKeyDown(Keys.Left))
						playerOne.position.X -= playerOne.speed;
					if (newState.IsKeyDown(Keys.Down))
						playerOne.position.Y += playerOne.speed;
					if (newState.IsKeyDown(Keys.Up))
						playerOne.position.Y -= playerOne.speed;
				}
				// PlayerOne Firing
				//KeyboardState newState2 = Keyboard.GetState();
				//if (!(playerOne.bulletCollision == true || playerOne.playerCollision == true))
				//{
				//    // PlayerOne firing

				//}
				// TODO: Create Keyboard input for firing

				// PlayerTwo Keyboard Input
				// PlayerTwo movement
				if (!(playerTwo == null))
				{
					if (!(playerTwo.bulletCollision == true || playerTwo.playerCollision == true))
					{
						if (newState.IsKeyDown(Keys.D))
							playerTwo.position.X += playerTwo.speed;
						if (newState.IsKeyDown(Keys.A))
							playerTwo.position.X -= playerTwo.speed;
						if (newState.IsKeyDown(Keys.S))
							playerTwo.position.Y += playerTwo.speed;
						if (newState.IsKeyDown(Keys.W))
							playerTwo.position.Y -= playerTwo.speed;
					}
				}
			}

			previousGamePadState1 = GamePad.GetState(PlayerIndex.One);
			previousGamePadState2 = GamePad.GetState(PlayerIndex.Two);
			previousGamePadState3 = GamePad.GetState(PlayerIndex.Three);
			previousGamePadState4 = GamePad.GetState(PlayerIndex.Four);

		}

		Vector2 centerForGameOverText;

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(backColor);

			// Draw the sprites.
			spriteBatch.Begin();

			// Draw the background
			spriteBatch.Draw(background, new Vector2(), Color.White);

			//check for game over
			if (gameOver)
			{
				centerForGameOverText = new Vector2(centerOfScreen.X - 146, centerOfScreen.Y - 12);
				spriteBatch.DrawString(gameFont, "Game Over.  Press Start!", centerForGameOverText, Color.White);
				spriteBatch.End();
				base.Draw(gameTime);
				return; // draw nothing else
						//ToDo: Setup a real game over screen, real pause and player selection screens 
			}
			else
			{
#if debug
			spriteBatch.DrawString(gameFont, "diff adder: " + diffuculityAdder, new Vector2(safeBounds.Left, safeBounds.Top + 180), Color.Blue);
#endif

				// Draw the Player Status Areas
				if (playerOneStarted)
				{
					spriteBatch.Draw(playerStatusArea, new Vector2(safeBounds.Left - 5, safeBounds.Top - 5), new Color(255, 255, 255, 40));
					spriteBatch.DrawString(gameFont, "Player One", new Vector2(safeBounds.Left, safeBounds.Top), Color.Blue);
					spriteBatch.DrawString(gameFont, "Lives: " + playerOne.lives, new Vector2(safeBounds.Left, safeBounds.Top + 40), Color.Blue);
					spriteBatch.DrawString(gameFont, "Super Bombs: " + playerOne.superBombs, new Vector2(safeBounds.Left, safeBounds.Top + 80), Color.Blue);
					spriteBatch.DrawString(gameFont, "Score: " + playerOne.score, new Vector2(safeBounds.Left, safeBounds.Top + 120), Color.Blue);
				}
				if (playerTwoStarted)
				{
					spriteBatch.Draw(playerStatusArea, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Top), new Color(255, 255, 255, 40));
					spriteBatch.DrawString(gameFont, "Player Two", new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Top), Color.Blue);
					spriteBatch.DrawString(gameFont, "Lives: " + playerTwo.lives, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Top + 40), Color.Blue);
					spriteBatch.DrawString(gameFont, "Super Bombs: " + playerTwo.superBombs, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Top + 80), Color.Blue);
					spriteBatch.DrawString(gameFont, "Score: " + playerTwo.score, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Top + 120), Color.Blue);
				}
				if (playerThreeStarted)
				{
					spriteBatch.Draw(playerStatusArea, new Vector2(safeBounds.Left, safeBounds.Bottom - playerStatusArea.Height), new Color(255, 255, 255, 40));
					spriteBatch.DrawString(gameFont, "Player Three", new Vector2(safeBounds.Left, safeBounds.Bottom - playerStatusArea.Height), Color.Blue);
					spriteBatch.DrawString(gameFont, "Lives: " + playerThree.lives, new Vector2(safeBounds.Left, safeBounds.Bottom - playerStatusArea.Height + 40), Color.Blue);
					spriteBatch.DrawString(gameFont, "Super Bombs: " + playerThree.superBombs, new Vector2(safeBounds.Left, safeBounds.Bottom - playerStatusArea.Height + 80), Color.Blue);
					spriteBatch.DrawString(gameFont, "Score: " + playerThree.score, new Vector2(safeBounds.Left, safeBounds.Bottom - playerStatusArea.Height + 120), Color.Blue);
				}
				if (playerFourStarted)
				{
					spriteBatch.Draw(playerStatusArea, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Bottom - playerStatusArea.Height), new Color(255, 255, 255, 40));
					spriteBatch.DrawString(gameFont, "Player Four", new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Bottom - playerStatusArea.Height), Color.Blue);
					spriteBatch.DrawString(gameFont, "Lives: " + playerFour.lives, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Bottom - playerStatusArea.Height + 40), Color.Blue);
					spriteBatch.DrawString(gameFont, "Super Bombs: " + playerFour.superBombs, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Bottom - playerStatusArea.Height + 80), Color.Blue);
					spriteBatch.DrawString(gameFont, "Score: " + playerFour.score, new Vector2(safeBounds.Right - playerStatusArea.Width, safeBounds.Bottom - playerStatusArea.Height + 120), Color.Blue);
				}
				// Draw the power crystal in the center of the screen
				spriteBatch.Draw(powerCrystal.sprite, powerCrystal.position, Color.BlueViolet);

				// Draw the players
				if (playerOneStarted && playerOne.lives > 0)
				{
					if (playerOne.bulletCollision || playerOne.playerCollision)
						playerOne.explosion.DrawFrame(spriteBatch, playerOne.explosionSpritePosition);
					else if (!(playerOne.bulletCollision || playerOne.playerCollision))
						spriteBatch.Draw(playerOne.sprite, playerOne.position, Color.White);
					//draws shields - The fourth varaiable in the new color controls opacity which gives the shield a fade as it runs out
					if (playerOne.shieldUp == true && playerOne.playerSettingsHaveBeenModifiedByShieldPowerUp == false)
						spriteBatch.Draw(playerOne.shieldSprite, playerOne.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerOne.shieldUpCounter / (playerOne.shieldTimeAfterDeath / 185.0)) + 70)));
					if (playerOne.shieldUp == true && playerOne.playerSettingsHaveBeenModifiedByShieldPowerUp == true)
						spriteBatch.Draw(playerOne.shieldSprite, playerOne.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerOne.shieldUpCounter / (ShieldPowerUp.shieldTime / 185.0)) + 70)));
					foreach (Projectile Projectile in playerOne.projectiles)
						spriteBatch.Draw(Projectile.sprite, Projectile.position, Color.White);

				}
				if (playerTwoStarted && playerTwo.lives > 0)
				{
					if (playerTwo.bulletCollision || playerTwo.playerCollision)
						playerTwo.explosion.DrawFrame(spriteBatch, playerTwo.explosionSpritePosition);
					else if (!(playerTwo.bulletCollision || playerTwo.playerCollision))
						spriteBatch.Draw(playerTwo.sprite, playerTwo.position, Color.White);
					spriteBatch.Draw(debugDot, playerTwo.CenterPosition, Color.White);
					if (playerTwo.shieldUp == true && playerTwo.playerSettingsHaveBeenModifiedByShieldPowerUp == false)
						spriteBatch.Draw(playerTwo.shieldSprite, playerTwo.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerTwo.shieldUpCounter / (playerTwo.shieldTimeAfterDeath / 185.0)) + 70)));
					if (playerTwo.shieldUp == true && playerTwo.playerSettingsHaveBeenModifiedByShieldPowerUp == true)
						spriteBatch.Draw(playerTwo.shieldSprite, playerTwo.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerTwo.shieldUpCounter / (ShieldPowerUp.shieldTime / 185.0)) + 70)));
					foreach (Projectile Projectile in playerTwo.projectiles)
						spriteBatch.Draw(Projectile.sprite, Projectile.position, Color.White);
				}
				if (playerThreeStarted && playerThree.lives > 0)
				{
					if (playerThree.bulletCollision || playerThree.playerCollision)
						playerThree.explosion.DrawFrame(spriteBatch, playerThree.explosionSpritePosition);
					else if (!(playerThree.bulletCollision || playerThree.playerCollision))
						spriteBatch.Draw(playerThree.sprite, playerThree.position, Color.White);
					if (playerThree.shieldUp == true && playerThree.playerSettingsHaveBeenModifiedByShieldPowerUp == false)
						spriteBatch.Draw(playerThree.shieldSprite, playerThree.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerThree.shieldUpCounter / (playerThree.shieldTimeAfterDeath / 185.0)) + 70)));
					if (playerThree.shieldUp == true && playerThree.playerSettingsHaveBeenModifiedByShieldPowerUp == true)
						spriteBatch.Draw(playerThree.shieldSprite, playerThree.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerThree.shieldUpCounter / (ShieldPowerUp.shieldTime / 185.0)) + 70)));
					foreach (Projectile Projectile in playerThree.projectiles)
						spriteBatch.Draw(Projectile.sprite, Projectile.position, Color.White);
				}
				if (playerFourStarted && playerFour.lives > 0)
				{
					if (playerFour.bulletCollision || playerFour.playerCollision)
						playerFour.explosion.DrawFrame(spriteBatch, playerFour.explosionSpritePosition);
					else if (!(playerFour.bulletCollision || playerFour.playerCollision))
						spriteBatch.Draw(playerFour.sprite, playerFour.position, Color.White);
					if (playerFour.shieldUp == true && playerFour.playerSettingsHaveBeenModifiedByShieldPowerUp == false)
						spriteBatch.Draw(playerFour.shieldSprite, playerFour.shieldSpritePosition, new Color((byte)255, (byte)255, (byte)255, (byte)((playerFour.shieldUpCounter / (playerFour.shieldTimeAfterDeath / 185.0)) + 70)));
					if (playerFour.shieldUp == true && playerFour.playerSettingsHaveBeenModifiedByShieldPowerUp == true)
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

				foreach (CrystalGrabber crystalGrabber in crystalGrabbers)
				{

					if (crystalGrabber.bulletCollision == true || crystalGrabber.playerCollision == true)
						crystalGrabber.explosion.DrawFrame(spriteBatch, crystalGrabber.position);
					else
						crystalGrabber.anamatedSprite.DrawFrame(spriteBatch, crystalGrabber.position);
					//spriteBatch.Draw(crystalGrabber.sprite, crystalGrabber.position, Color.White);
					//spriteBatch.Draw(debugDot, crystalGrabber.CenterPosition, Color.White);  // used for debugging position
				}
			}

			spriteBatch.End();
			base.Draw(gameTime);
		}

		///<summary>
		/// PlayerMovement handles the joystick movement for a player object
		///</summary>
		///<param name="playerObj">The player object to move</param>
		///<param name="gamePadState">The gamePadState controlling the movement</param>
		void PlayerMovement(Player playerObj, GamePadState gamePadState)
		{

			if (!(playerObj == null))
			{
				// if the player is currently blowing up then skip movement
				if (!(playerObj.playerCollision == true || playerObj.bulletCollision == true))
				{
					playerObj.position.X += gamePadState.ThumbSticks.Left.X * playerObj.speed;
					playerObj.position.Y += gamePadState.ThumbSticks.Left.Y * -playerObj.speed;
				}
			}
		}

		///<summary>
		/// PlayerShooting handles the joystick shooting for the player object
		///</summary>
		///<param name="playerObj">The player object to move</param>
		///<param name="gamePadState">The gamePadState controlling the movement</param>
		void PlayerShooting(Player playerObj, GamePadState gamePadState)
		{
			if (!(playerObj == null))
			{
				// Check for super Bomb dropage
				if (gamePadState.Buttons.RightShoulder == ButtonState.Pressed && playerObj.superBombs > 0 && playerObj.superBombTimer.expired(gameTime1))
				{
					dropTheSuperBomb();
					playerObj.superBombs--;
					playerObj.score += 35;
					playerObj.superBombTimer.reset(gameTime1);
				}

				// this loop checks for collisions and updates the projectile movement 
				//for (int i = 0; i < playerObj.projectiles.Count; i++)
				foreach (Projectile projectile in playerObj.projectiles)
				{
					projectile.position =
						new Vector2(projectile.position.X + projectile.direction.X * projectile.speed,
						projectile.position.Y + projectile.direction.Y * -projectile.speed);


					//Check for collisions with other players
					if (checkPlayerBulletCollision(playerObj, projectile) && (!(projectile.invincible)))
					{
						playerObj.projectiles.Remove(projectile);
						break;
					}

					// Check for collisions with enemys
					if (checkEnemyBulletCollision(projectile))
					{
						playerObj.score += 10;
						if (!(projectile.invincible))
						{
							playerObj.projectiles.Remove(projectile);
							break;
						}
					}

					// Check for collisions with other players shileds
					if (checkPlayerShieldBulletCollision(projectile, playerObj))
					{
						//playerObj.projectiles.Remove(projectile);
						//break;

					}



					// Check for collisions with other player bullets
					// ToDo: setup checking collisions between player bullets probibly only for deathmatch games
				}

				// This loop adds new projectiles when the player is shooting
				if (!(playerObj.playerCollision == true || playerObj.bulletCollision == true))
				{
					if (playerObj.projectileFrequencyTimer.expired(gameTime1) == true && gamePadState.IsConnected)
					{
						playerObj.projectileFrequencyTimer.reset(gameTime1);
						if ((gamePadState.ThumbSticks.Right.X < 0.09 && gamePadState.ThumbSticks.Right.X > -0.09)
							&& (gamePadState.ThumbSticks.Right.Y < 0.09 && gamePadState.ThumbSticks.Right.Y > -0.09))
							return;

						// The following changes the projectile texture depending on the power up
						if (playerObj.playerSettingsHaveBeenModifiedByPepperPowerup == false && playerObj.playerSettingsHaveBeenModifiedByInvincibleBulletPowerUp == false)
						{
							playerObj.projectiles.Add(new Projectile(this,
								new Vector2(playerObj.position.X + 30, playerObj.position.Y + 55),
								new Vector2(gamePadState.ThumbSticks.Right.X, gamePadState.ThumbSticks.Right.Y)));
							//new GamePadState(gamePadState.ThumbSticks, gamePadState.Triggers, gamePadState.Buttons, gamePadState.DPad)));
							//soundBank.PlayCue("splat"); //ToDo: change this annoying sound
						}
						if (playerObj.playerSettingsHaveBeenModifiedByPepperPowerup == true && playerObj.playerSettingsHaveBeenModifiedByInvincibleBulletPowerUp == false)
						{
							playerObj.projectiles.Add(new Projectile(this,
								new Vector2(playerObj.position.X, playerObj.position.Y),
								new Vector2(gamePadState.ThumbSticks.Right.X, gamePadState.ThumbSticks.Right.Y),
								PepperPowerUp.projectileSprite, playerObj.projectileSpeed));
							//soundBank.PlayCue("ToDo: Add a sound for Pepper");
						}
						if (playerObj.playerSettingsHaveBeenModifiedByPepperPowerup == false && playerObj.playerSettingsHaveBeenModifiedByInvincibleBulletPowerUp == true)
						{
							playerObj.projectiles.Add(new Projectile(this,
								new Vector2(playerObj.position.X, playerObj.position.Y),
								new Vector2(gamePadState.ThumbSticks.Right.X, gamePadState.ThumbSticks.Right.Y),
								InvincibleBulletPowerUp.projectileSprite, playerObj.projectileSpeed, true));
							//soundBank.PlayCue(" ToDo: add a sound for InvincibleBullet");
						}
						if (playerObj.playerSettingsHaveBeenModifiedByPepperPowerup == true && playerObj.playerSettingsHaveBeenModifiedByInvincibleBulletPowerUp == true)
						{
							playerObj.projectiles.Add(new Projectile(this,
								new Vector2(playerObj.position.X, playerObj.position.Y),
								new Vector2(gamePadState.ThumbSticks.Right.X, gamePadState.ThumbSticks.Right.Y),
								fireBallMetalSphere, playerObj.projectileSpeed, true));
						}
					}
				}
			}
		}

		///<summary>
		/// PlayerTiming handles the of the timing for shields and explosions
		///</summary>
		void PlayerTiming(Player playerObj)
		{
			if (!(playerObj == null))
			{
				if (playerObj.bulletCollision == true || playerObj.playerCollision == true)
					playerObj.headExplodeCounter++;
				if (playerObj.shieldUp == true)
					playerObj.shieldUpCounter--;
				if (playerObj.shieldUpCounter < 1)
				{
					playerObj.playerSettingsHaveBeenModifiedByShieldPowerUp = false;
					playerObj.shieldUp = false;
					playerObj.shieldUpCounter = playerObj.shieldTimeAfterDeath;
				}
				if (headExplodeDownTime < playerObj.headExplodeCounter)
				{
					if (playerObj.lives < 0)
					{
						playerObj.position.X = 10000;
						playerObj.position.Y = 10000;
						playerObj = null;
						return;
					}
					//playerObj.explosion.Frame = 0;  //reset the explosion frame
					playerObj.bulletCollision = false; //reset any prevoius collisions
					playerObj.playerCollision = false; //reset any prevoius collisions
					playerObj.headExplodeCounter = 0;
					playerObj.position = playerObj.startPosition;
					playerObj.shieldUp = true;
				}
				if (playerObj.playerCollision == true || playerObj.bulletCollision == true)
				{
					playerObj.explosion.UpdateFrame(elapsed);
				}
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
			if (!(playerObj == null))
			{
				if (!(playerOne == null))
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
								//soundBank.PlayCue("explosion6");
							}
							playerOne.bulletCollision = true;
						}
						if (gameType == gameTypes.defendTheCrystal)
						{
							playerOne.position.X += projectileObj.direction.X * knockStrength;
							playerOne.position.Y -= projectileObj.direction.Y * knockStrength;
						}
					}
				}
				if (!(playerTwo == null))
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
								//soundBank.PlayCue("explosion6");
							}
							playerTwo.bulletCollision = true;
						}
						if (gameType == gameTypes.defendTheCrystal)
						{
							playerTwo.position.X += projectileObj.direction.X * knockStrength;
							playerTwo.position.Y -= projectileObj.direction.Y * knockStrength;
						}

					}
				}
				if (!(playerThree == null))
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
								//soundBank.PlayCue("explosion6");
							}
							playerThree.bulletCollision = true;
						}
						if (gameType == gameTypes.defendTheCrystal)
						{
							playerThree.position.X += projectileObj.direction.X * knockStrength;
							playerThree.position.Y -= projectileObj.direction.Y * knockStrength;
						}

					}
				}
				if (!(playerFour == null))
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
								//soundBank.PlayCue("explosion6");
							}
							playerFour.bulletCollision = true;
						}
						if (gameType == gameTypes.defendTheCrystal)
						{
							playerFour.position.X += projectileObj.direction.X * knockStrength;
							playerFour.position.Y -= projectileObj.direction.Y * knockStrength;
						}
					}
				}
			}
			return result;
		}

		bool checkPlayerShieldBulletCollision(Projectile projectileObj, Player playerObj)
		{
			if (!(playerOne == null))
			{
				//if we are not player one and we hit player one with shield up
				if (playerOne.shieldRectangle.Intersects(projectileObj.rectangle) && playerOne.shieldUp == true && !(playerObj.Equals(playerOne)))
				{
					// Check for per pixle collision
					if (perPixleCollisionDetection(projectileObj.rectangle, projectileObj.spriteTextureData, playerOne.shieldRectangle, playerOne.shieldTextureData))
					{
						projectileObj.direction = findDirection(projectileObj.centerPosition, playerOne.shieldCenterPosition);
						projectileObj.direction.Y *= -1;
						return true;
					}
				}
			}
			if (!(playerTwo == null))
			{
				//if we are not player two and we hit player one with shield up
				if (playerTwo.shieldRectangle.Intersects(projectileObj.rectangle) && playerTwo.shieldUp == true && !(playerObj.Equals(playerTwo)))
				{
					// Check for per pixle collision
					if (perPixleCollisionDetection(projectileObj.rectangle, projectileObj.spriteTextureData, playerTwo.shieldRectangle, playerTwo.shieldTextureData))
					{
						projectileObj.direction = findDirection(projectileObj.centerPosition, playerTwo.shieldCenterPosition);
						projectileObj.direction.Y *= -1; // not sure why this is needed - something wrong with finddirection?
														 //projectileObj.direction *= findDirection(projectileObj.centerPosition, playerTwo.shieldCenterPosition);
														 //projectileObj.direction.Y *= -1;
						return true;
					}
				}
			}

			//if we are not player three and we hit player one with shield up
			if (!(playerThree == null))
			{
				if (playerThree.shieldRectangle.Intersects(projectileObj.rectangle) && playerThree.shieldUp == true && !(playerObj.Equals(playerThree)))
				{
					// Check for per pixle collision
					if (perPixleCollisionDetection(projectileObj.rectangle, projectileObj.spriteTextureData, playerThree.shieldRectangle, playerThree.shieldTextureData))
					{
						return true;
					}
				}
			}

			if (!(playerFour == null))
			{
				//if we are not player four and we hit player one with shield up
				if (playerFour.shieldRectangle.Intersects(projectileObj.rectangle) && playerFour.shieldUp == true && !(playerObj.Equals(playerFour)))
				{
					// Check for per pixle collision
					if (perPixleCollisionDetection(projectileObj.rectangle, projectileObj.spriteTextureData, playerFour.shieldRectangle, playerFour.shieldTextureData))
					{
						return true;
					}
				}
			}
			return false;
		}

		bool checkEnemyBulletCollision(Projectile projectileObj)
		{
			bool result = false;
			foreach (Kamikaze kamikazeObj in kamikazes)
			{
				if (kamikazeObj.rectangle.Intersects(projectileObj.rectangle) && (!(kamikazeObj.bulletCollision || kamikazeObj.playerCollision)))
				{
					result = true;
					if (kamikazeObj.bulletCollision == false) // play the sound only once
					{
						//soundBank.PlayCue("explosion6");
					}
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
					{
						//soundBank.PlayCue("explosion6");
					}
					crystalGrabber.bulletCollision = true;
					break;
				}
			}
			return result;
		}

		///<summary>
		/// checkPlayerPlayerCollision handles checking weather a player object has hit any other player
		///</summary>
		void checkPlayerPlayerCollision(Player playerObjOne, Player playerObjTwo)
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
							soundBank.PlayCue("explosion6");
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
						{
							//soundBank.PlayCue("explosion6");
						}
						spriteObj.playerCollision = true;
					}
				}
				if (playerObj.shieldUp == true)
				{
					if (playerObj.rectangle.Intersects(spriteObj.rectangle) && (!(spriteObj.playerCollision || spriteObj.bulletCollision)))
					{
						//soundBank.PlayCue("explosion6");
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
		/// This handles modifying the player settings to powerups
		/// </summary>
		bool powerUpHandleing(Player playerObj, PowerUp powerUpObj)
		{

			bool powerUpHasBeenGrabbedResult = false;
			if (!(playerObj == null))
			{
				// ToDo: finish allowing multiple powerups to modify a player object at the same time - there may be nothing more to do then create another powerup type and test multiple powerups

				if (playerObj.rectangle.Intersects(powerUpObj.rectangle))
				{
					// ToDO: create powerup types that can be matched to the player
					playerObj.powerUpType = powerUpObj.powerUpType;
					if (powerUpObj.powerUpType == Sprite.powerUpTypes.Pepper && playerObj.playerSettingsHaveBeenModifiedByPepperPowerup == true)
						playerObj.pepperPowerUpTimeoutTimer.reset(gameTime1);
					if (powerUpObj.powerUpType == Sprite.powerUpTypes.Pepper && playerObj.playerSettingsHaveBeenModifiedByPepperPowerup == false)
					{
						playerObj.projectileFrequencyTimer.timerDuration += PepperPowerUp.projectileFrequencyModifier;
						playerObj.speed += PepperPowerUp.playerSpeedModifier;
						playerObj.projectileSpeed += PepperPowerUp.projectileSpeedModifier;
						playerObj.playerSettingsHaveBeenModifiedByPepperPowerup = true;
						playerObj.pepperPowerUpTimeoutTimer = new Timer(PepperPowerUp.timeout, gameTime1); // constructor starts the timer
					}

					if (powerUpObj.powerUpType == Sprite.powerUpTypes.InvincibleBullet && playerObj.playerSettingsHaveBeenModifiedByInvincibleBulletPowerUp == true)
						playerObj.invincibleBulletPowerUpTimeoutTimer.reset(gameTime1);
					if (powerUpObj.powerUpType == Sprite.powerUpTypes.InvincibleBullet && playerObj.playerSettingsHaveBeenModifiedByInvincibleBulletPowerUp == false)
					{
						playerObj.projectileFrequencyTimer.timerDuration += InvincibleBulletPowerUp.projectileFrequencyModifier;
						playerObj.speed += InvincibleBulletPowerUp.playerSpeedModifier;
						playerObj.projectileSpeed += InvincibleBulletPowerUp.projectileSpeedModifier;
						playerObj.playerSettingsHaveBeenModifiedByInvincibleBulletPowerUp = true;
						playerObj.invincibleBulletPowerUpTimeoutTimer = new Timer(PepperPowerUp.timeout, gameTime1); // constructor starts the timer
					}

					if (powerUpObj.powerUpType == Sprite.powerUpTypes.Speed && playerObj.playerSettingsHaveBeenModifiedBySpeedPowerUp == true)
						playerObj.speedPowerUpTimeoutTimer.reset(gameTime1);
					if (powerUpObj.powerUpType == Sprite.powerUpTypes.Speed && playerObj.playerSettingsHaveBeenModifiedBySpeedPowerUp == false)
					{
						playerObj.playerSettingsHaveBeenModifiedBySpeedPowerUp = true;
						playerObj.speedPowerUpTimeoutTimer = new Timer(SpeedPowerUp.timeout, gameTime1);
						playerObj.speed += SpeedPowerUp.playerSpeedModifier;
					}

					if (powerUpObj.powerUpType == Sprite.powerUpTypes.SuperBomb)
					{
						playerObj.superBombs++;
					}
					if (powerUpObj.powerUpType == Sprite.powerUpTypes.ExtraLife)
					{
						playerObj.lives++;
					}
					if (powerUpObj.powerUpType == Sprite.powerUpTypes.Shield)
					{
						playerObj.playerSettingsHaveBeenModifiedByShieldPowerUp = true;
						playerObj.shieldUpCounter = ShieldPowerUp.shieldTime;
						playerObj.shieldUp = true;
					}

					powerUpHasBeenGrabbedResult = true;
				}
			}
			return powerUpHasBeenGrabbedResult;
		}

		/// <summary>
		/// This handles removing powerUp settings after they timeout
		/// </summary>
		void powerUpTiming(Player playerObj)
		{
			if (playerObj != null)
			{
				// handle the timeout for the pepperpowerup
				if (playerObj.playerSettingsHaveBeenModifiedByPepperPowerup == true)
				{
					if (!(playerObj.pepperPowerUpTimeoutTimer == null)) // check for null(uninstantiated) timer object 
					{
						if (playerObj.pepperPowerUpTimeoutTimer.expired(gameTime1) == true)
						{
							playerObj.playerSettingsHaveBeenModifiedByPepperPowerup = false;

							// subtract powerup modifiers from player settings 
							playerObj.projectileFrequencyTimer.timerDuration -= PepperPowerUp.projectileFrequencyModifier;
							playerObj.speed -= PepperPowerUp.playerSpeedModifier;
							playerObj.projectileSpeed -= PepperPowerUp.projectileSpeedModifier;
						}
					}
				}

				// Handle the timeout for the speed power up
				if (playerObj.playerSettingsHaveBeenModifiedBySpeedPowerUp == true)
				{
					if (!(playerObj.speedPowerUpTimeoutTimer == null)) // check for null(uninstantiated) timer object 
					{
						if (playerObj.speedPowerUpTimeoutTimer.expired(gameTime1) == true)
						{
							playerObj.playerSettingsHaveBeenModifiedBySpeedPowerUp = false;

							// subtract powerup modifiers from player settings 
							playerObj.speed -= SpeedPowerUp.playerSpeedModifier;
						}
					}
				}

				// handle the timeout for the InvincibleBullet powerup
				if (playerObj.playerSettingsHaveBeenModifiedByInvincibleBulletPowerUp == true)
				{
					if (!(playerObj.invincibleBulletPowerUpTimeoutTimer == null)) // check for null(uninstantiated) timer object 
					{
						if (playerObj.invincibleBulletPowerUpTimeoutTimer.expired(gameTime1) == true)
						{
							playerObj.playerSettingsHaveBeenModifiedByInvincibleBulletPowerUp = false;

							// subtract powerup modifiers from player settings 
							playerObj.projectileFrequencyTimer.timerDuration -= InvincibleBulletPowerUp.projectileFrequencyModifier;
							playerObj.speed -= InvincibleBulletPowerUp.playerSpeedModifier;
							playerObj.projectileSpeed -= InvincibleBulletPowerUp.projectileSpeedModifier;
						}
					}
				}
			}
		}

		/// <summary>
		/// Handles spawning new powerups
		/// </summary>
		void spawnPowerUp(int frequency, Sprite.powerUpTypes powerUpType, Random random)
		{
			//Random random = new Random();
			//Random random2 = new Random();
			//Random random3 = new Random();

			if (random.Next(10000) < frequency)
			{
				if (powerUpType == Sprite.powerUpTypes.Pepper)
				{
					float randomFloatNumOne = random.Next(200);
					float randomFloatNumTwo = random.Next(200);


					pepperPowerUps.Add(new PepperPowerUp(this, new Vector2(random.Next((int)(viewport.Width)), random.Next((int)(viewport.Height))),
						new Vector2((randomFloatNumOne / 100) - 1, (randomFloatNumTwo / 100) - 1), random.Next(2) + 1));
				}
				if (powerUpType == Sprite.powerUpTypes.InvincibleBullet)
				{
					float randomFloatNumOne = random2.Next(200);
					float randomFloatNumTwo = random2.Next(200);
					InvincibleBulletPowerUps.Add(new InvincibleBulletPowerUp(this, new Vector2(random2.Next((int)(viewport.Width)), random2.Next((int)(viewport.Height))),
						new Vector2((randomFloatNumOne / 100) - 1, (randomFloatNumTwo / 100) - 1), random2.Next(2) + 1));
				}
				if (powerUpType == Sprite.powerUpTypes.SuperBomb)
				{
					float randomFloatNumOne = random3.Next(200);
					float randomFloatNumTwo = random3.Next(200);
					superBombPowerUps.Add(new SuperBombPowerUp(this, new Vector2(random3.Next((int)(viewport.Width)), random3.Next((int)(viewport.Height))),
						new Vector2((randomFloatNumOne / 100) - 1, (randomFloatNumTwo / 100) - 1), random3.Next(2) + 1));
				}
				if (powerUpType == Sprite.powerUpTypes.Shield)
				{
					float randomFloatNumOne = random4.Next(200);
					float randomFloatNumTwo = random4.Next(200);

					shieldPowerUps.Add(new ShieldPowerUp(this, new Vector2(random4.Next((int)(viewport.Width)), random4.Next((int)(viewport.Height))),
						new Vector2((randomFloatNumOne / 100) - 1, (randomFloatNumTwo / 100) - 1), random4.Next(2) + 1));
				}
				if (powerUpType == Sprite.powerUpTypes.Speed)
				{
					float randomFloatNumOne = random5.Next(200);
					float randomFloatNumTwo = random5.Next(200);

					speedPowerUps.Add(new SpeedPowerUp(this, new Vector2(random5.Next((int)(viewport.Width)), random5.Next((int)(viewport.Height))),
						new Vector2((randomFloatNumOne / 100) - 1, (randomFloatNumTwo / 100) - 1), random5.Next(2) + 1));
				}
				if (powerUpType == Sprite.powerUpTypes.ExtraLife)
				{
					float randomFloatNumOne = random6.Next(200);
					float randomFloatNumTwo = random6.Next(200);

					extraLifePowerUps.Add(new ExtraLifePowerUp(this, new Vector2(random6.Next((int)(viewport.Width)), random6.Next((int)(viewport.Height))),
						new Vector2((randomFloatNumOne / 100) - 1, (randomFloatNumTwo / 100) - 1), random6.Next(2) + 1));
				}
			}
		}

		void dropTheSuperBomb()
		{
			// destroy all enemys
			// destroy all kamikazes
			foreach (Kamikaze kamikaze in kamikazes)
			{
				kamikaze.bulletCollision = true;
			}
			// destroy all crystal grabbers
			foreach (CrystalGrabber crystalGrabber in crystalGrabbers)
			{
				crystalGrabber.bulletCollision = true;
			}
		}

		int numToSpawn = 20;

		/// <summary>
		/// Handles spawning new kamikazes above the viewable area
		/// </summary>
		void spawnKamikazesAboveScreen(int frequency)
		{
			Random random = new Random(randomSeed);
			if (random.Next(10000) < frequency)
			{
				for (int i = 0; i < random.Next(numToSpawn); i++)
				{
					kamikazes.Add(new Kamikaze(this, new Vector2(random.Next(viewport.Width), -350), new AnimatedTexture(new Vector2(0), Content, "explosionStripForGhost", 12, 12), new AnimatedTexture(new Vector2(0), Content, "KamakisaiSheet - small", 4, 12)));
				}
			}
		}

		/// <summary>
		/// Handles spawning new kamikazes below the viewable area
		/// </summary>
		void spawnKamikazesBelowScreen(int frequency)
		{
			Random random = new Random(randomSeed);
			if (random.Next(10000) < frequency)
			{
				for (int i = 0; i < random.Next(numToSpawn); i++)
				{
					kamikazes.Add(new Kamikaze(this, new Vector2(random.Next(viewport.Width), viewport.Height + 300), new AnimatedTexture(new Vector2(0), Content, "explosionStripForGhost", 12, 12), new AnimatedTexture(new Vector2(0), Content, "KamakisaiSheet - small", 4, 12)));
				}
			}
		}

		/// <summary>
		/// Handles spawning new kamikazes left of the viewable area
		/// </summary>
		void spawnKamikazesLeftOfScreen(int frequency)
		{
			Random random = new Random(randomSeed);
			if (random.Next(10000) < frequency)
			{
				for (int i = 0; i < random.Next(numToSpawn); i++)
				{
					kamikazes.Add(new Kamikaze(this, new Vector2(-300, random.Next(viewport.Height)), new AnimatedTexture(new Vector2(0), Content, "explosionStripForGhost", 12, 12), new AnimatedTexture(new Vector2(0), Content, "KamakisaiSheet - small", 4, 12)));
				}
			}
		}

		/// <summary>
		/// Handles spawning new kamikazes right of the viewable area
		/// </summary>
		void spawnKamikazesRightOfScreen(int frequency)
		{
			Random random = new Random(randomSeed);
			if (random.Next(10000) < frequency)
			{
				for (int i = 0; i < random.Next(numToSpawn); i++)
				{
					kamikazes.Add(new Kamikaze(this, new Vector2(viewport.Width + 300, random.Next(viewport.Height)), new AnimatedTexture(new Vector2(0), Content, "explosionStripForGhost", 12, 12), new AnimatedTexture(new Vector2(0), Content, "KamakisaiSheet - small", 4, 12)));
				}
			}
		}

		/// <summary>
		/// Handles spawning new crystalGrabbewrs  above the viewable area
		/// </summary>
		void spawnCrystalGrabbersAboveScreen(int frequency)
		{
			Random random = new Random(randomSeed);
			if (random.Next(10000) < frequency)
			{
				for (int i = 0; i < random.Next(numToSpawn); i++)
				{
					crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(random.Next(viewport.Width), -300), new AnimatedTexture(new Vector2(0), Content, "explosionStrip", 7, 12), new AnimatedTexture(new Vector2(0), Content, "OctoEnemyAnamation2", 14, 12)));
					crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(random.Next(viewport.Width), -300), new AnimatedTexture(new Vector2(0), Content, "explosionStrip", 7, 12), new AnimatedTexture(new Vector2(0), Content, "OctoEnemyAnamation", 9, 12)));
				}

			}
		}

		/// <summary>
		/// Handles spawning new crystalGrabbewrs  above the viewable area
		/// </summary>
		void spawnCrystalGrabbersBelowScreen(int frequency)
		{
			Random random = new Random(randomSeed);
			if (random.Next(10000) < frequency)
			{
				for (int i = 0; i < random.Next(numToSpawn); i++)
				{
					crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(random.Next(viewport.Width), viewport.Height + 300), new AnimatedTexture(new Vector2(0), Content, "explosionStrip", 7, 12), new AnimatedTexture(new Vector2(0), Content, "OctoEnemyAnamation2", 14, 24)));
					crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(random.Next(viewport.Width), viewport.Height + 300), new AnimatedTexture(new Vector2(0), Content, "explosionStrip", 7, 12), new AnimatedTexture(new Vector2(0), Content, "OctoEnemyAnamation", 9, 12)));
				}

			}
		}

		/// <summary>
		/// Handles spawning new crystalGrabbewrs right of the viewable area
		/// </summary>
		void spawnCrystalGrabbersRightOfScreen(int frequency)
		{
			Random random = new Random(randomSeed);
			if (random.Next(10000) < frequency)
			{
				for (int i = 0; i < random.Next(numToSpawn); i++)
				{
					crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(viewport.Width + 300, random.Next(viewport.Height)), new AnimatedTexture(new Vector2(0), Content, "explosionStrip", 7, 12), new AnimatedTexture(new Vector2(0), Content, "OctoEnemyAnamation2", 14, 20)));
					crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(viewport.Width + 300, random.Next(viewport.Height)), new AnimatedTexture(new Vector2(0), Content, "explosionStrip", 7, 12), new AnimatedTexture(new Vector2(0), Content, "OctoEnemyAnamation", 9, 12)));
				}

			}
		}

		/// <summary>
		/// Handles spawning new crystalGrabbewrs left of the viewable area
		/// </summary>
		void spawnCrystalGrabbersLeftOfScreen(int frequency)
		{
			Random random = new Random(randomSeed);
			if (random.Next(10000) < frequency)
			{
				for (int i = 0; i < random.Next(numToSpawn); i++)
				{
					crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(-300, random.Next(viewport.Height)), new AnimatedTexture(new Vector2(0), Content, "explosionStrip", 7, 12), new AnimatedTexture(new Vector2(0), Content, "OctoEnemyAnamation", 9, 12)));
				}
			}
		}

		void KamikazeMovement()
		{
			foreach (Kamikaze kamikaze in kamikazes)
			{
				Player playerObj = FindNearistPlayer(kamikaze);
				kamikaze.direction = findDirection(playerObj.position, kamikaze.position);
				kamikaze.position.X += kamikaze.direction.X * kamikaze.speed;
				kamikaze.position.Y += kamikaze.direction.Y * kamikaze.speed;
			}
		}

		void CrystalGrabberMovement()
		{
			foreach (CrystalGrabber crystalGrabber in crystalGrabbers)
			{

				crystalGrabber.direction = findDirection(powerCrystal.position + crystalGrabber.offsetToGrabCrystal, crystalGrabber.position);
				crystalGrabber.position.X += crystalGrabber.direction.X * crystalGrabber.speed;
				crystalGrabber.position.Y += crystalGrabber.direction.Y * crystalGrabber.speed;
				if (crystalGrabber.position.Equals(powerCrystal.position + crystalGrabber.offsetToGrabCrystal) && crystalGrabber.hasGrabbedCrystal == false)
					crystalGrabber.hasGrabbedCrystal = true;
				if (crystalGrabber.hasGrabbedCrystal == true)
				{
					//Drag the crystal away through the top of the screen
					//powerCrystal.speed += 
					powerCrystal.position.Y -= powerCrystal.speed;
				}

			}
		}

		void CheckForCrystalOffScreen()
		{
			if (powerCrystal.position.Y <= 0 - powerCrystal.sprite.Height)
				gameOver = true;
		}


		Player FindNearistPlayer(Sprite spriteObj)
		{
			float distanceToPlayerOne;
			float distanceToPlayerTwo;
			float distanceToPlayerThree;
			float distanceToPlayerFour;

			if (playerOneStarted)
				distanceToPlayerOne = System.Math.Abs(spriteObj.position.X - playerOne.position.X) + System.Math.Abs(spriteObj.position.Y - playerOne.position.Y);
			else
				distanceToPlayerOne = 90000;
			if (playerTwoStarted)
				distanceToPlayerTwo = System.Math.Abs(spriteObj.position.X - playerTwo.position.X) + System.Math.Abs(spriteObj.position.Y - playerTwo.position.Y);
			else
				distanceToPlayerTwo = 90000;
			if (playerThreeStarted)
				distanceToPlayerThree = System.Math.Abs(spriteObj.position.X - playerThree.position.X) + System.Math.Abs(spriteObj.position.Y - playerThree.position.Y);
			else
				distanceToPlayerThree = 90000;
			if (playerFourStarted)
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

				// Check for bounds.
				playerObj.position.X = MathHelper.Clamp(playerObj.position.X, MinX, MaxX);
				playerObj.position.Y = MathHelper.Clamp(playerObj.position.Y, MinY, MaxY);
			}
		}

		/// <summary>
		/// This handles finding the slope bewtween two points and the direction to travel along that slope to get to the 
		/// destanation point
		/// </summary>
		Vector2 findDirection(Vector2 destinatoinPoint, Vector2 originPoint)
		{
			Vector2 direction = new Vector2();
			float directionY = destinatoinPoint.Y - originPoint.Y;
			float directionX = destinatoinPoint.X - originPoint.X;
			float reducedDirectionY = 0;
			float reducedDirectionX = 0;

			if (directionY == 0 && directionX > 0)
				return new Vector2(0, 1);
			if (directionY == 0 && directionX < 0)
				return new Vector2(0, -1);
			if (directionX == 0 && directionY < 0)
				return new Vector2(-1, 0);
			if (directionX == 0 && directionY > 0)
				return new Vector2(1, 0);

			if (System.Math.Abs(directionY) > System.Math.Abs(directionX))
			{
				reducedDirectionY = 1;
				reducedDirectionX = directionX / directionY;
			}

			if (System.Math.Abs(directionY) < System.Math.Abs(directionX))
			{
				reducedDirectionX = 1;
				reducedDirectionY = directionY / directionX;
			}

			if (destinatoinPoint.X > originPoint.X)
				direction.X = System.Math.Abs(reducedDirectionX);
			if (destinatoinPoint.X < originPoint.X)
				direction.X = (System.Math.Abs(reducedDirectionX)) * -1;

			if (destinatoinPoint.Y > originPoint.Y)
				direction.Y = System.Math.Abs(reducedDirectionY);
			if (destinatoinPoint.Y < originPoint.Y)
				direction.Y = (System.Math.Abs(reducedDirectionY)) * -1;

			return direction;
		}

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
			kamikazeSpawnRate = 8;
			crystalGrabberSpawnRate = 10;
			pepperPowerUpSpawnRate = 8;
			invincibleBulletPowerUpSpawnRate = 4;
			superBombPowerUpSpawnRate = 8;
			shieldPowerUpSpawnRate = 7;
			speedPowerUpSpawnRate = 6;
			extraLifePowerUpSpawnRate = 10;

			finalScoreMessageboxHasBeenShown = false;
			//gameOver = true;

			kamikazes.Clear();
			crystalGrabbers.Clear();
		}

	} // end class
}
