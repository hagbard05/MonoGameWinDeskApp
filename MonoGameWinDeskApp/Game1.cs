using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Net;
//using Microsoft.Xna.Framework.Storage;
using AnimatedSprite;

namespace MyFirstGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        bool finalScoreMessageboxHasBeenShown = false;
        double shieldTimeAfterDeath = 120;
        int forestScore = 0;
        int calebScore = 0;
        //int projectileSpeed = 15;
        int headExplodeDownTime = 59; //Number of cycles to show the explosion. default gameTime: 60 = 1 second = 0 to 59 = 1 second
        float elapsed; // used to track elapsed time for sprite anamation

        GraphicsDeviceManager graphics;
        AudioEngine audioEngine;
        SoundBank soundBank;
        WaveBank waveBank;
        SpriteBatch spriteBatch;
        Color backColor = Color.Black; //background color

        // Color array data for the images - used for per-pixel collision
        Color[] forestSpriteTextureData;
        Color[] projectileSpriteTextureData;


        Player playerOne;
        Player playerTwo;
        Player playerThree;
        Player playerFour;

        List<Projectile> playerOneProjectiles = new List<Projectile>();
        List<Projectile> playerTwoProjectiles = new List<Projectile>();

        PepperPowerUp pepperPowerUp;

        // The sub-rectangle of the drawable area which should be visible on all TVs
        Rectangle safeBounds;
        // Percentage of the screen on every side is the safe area
        const float SafeAreaPortion = 0.05f;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            //graphics.PreferredBackBufferWidth = 1920;
            //graphics.PreferredBackBufferHeight = 1080;

            //graphics.PreferredBackBufferWidth = 1550;
            //graphics.PreferredBackBufferHeight = 1150;


            Content.RootDirectory = "Content";
            //TargetElapsedTime = new TimeSpan(20000);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            // Calculate safe bounds based on current resolution
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            safeBounds = new Rectangle(
                (int)(viewport.Width * SafeAreaPortion),
                (int)(viewport.Height * SafeAreaPortion),
                (int)(viewport.Width * (1 - 2 * SafeAreaPortion)),
                (int)(viewport.Height * (1 - 2 * SafeAreaPortion)));

            audioEngine = new AudioEngine("Content\\Sounds\\XACT Sounds.xgs");
            soundBank = new SoundBank(audioEngine, "Content\\Sounds\\Sound Bank.xsb");
            waveBank = new WaveBank(audioEngine, "Content\\Sounds\\Wave Bank.xwb");

        }

        // This is a texture we can render.
        Texture2D forestSprite;
        Texture2D forestSprite2;
        Texture2D calebSprite;
        Texture2D headExplodeSprite;
        Texture2D shieldSprite;
        Texture2D splatSprite;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            calebSprite = Content.Load<Texture2D>("CalebSprite");
            forestSprite2 = Content.Load<Texture2D>("ForestSprite2");
            forestSprite = Content.Load<Texture2D>("ForestSprite");
            //projectileSprite = Content.Load<Texture2D>("flameBall");
            headExplodeSprite = Content.Load<Texture2D>("headExplode");
            shieldSprite = Content.Load<Texture2D>("Sphere75");
            splatSprite = Content.Load<Texture2D>("splat");

            // Extract collision data - used for per pixle collisions - not yet implemented
            forestSpriteTextureData = new Color[forestSprite2.Width * forestSprite2.Height];
            forestSprite2.GetData(forestSpriteTextureData);
            //projectileSpriteTextureData = new Color[projectileSprite.Width * projectileSprite.Height];

            // Create a new SpriteBatch
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Create player objects
            playerOne = new Player(this);
            playerOne.explosion.Load(Content, "explosionStrip2", 12, 12);

            playerTwo = new Player(this, new Vector2(0, graphics.GraphicsDevice.Viewport.Height - calebSprite.Height), forestSprite2);
            playerTwo.explosion.Load(Content, "explosionStrip2", 12, 12);

            playerThree = new Player(this, new Vector2(graphics.GraphicsDevice.Viewport.Width - forestSprite.Width, 0));
            playerThree.explosion.Load(Content, "explosionStrip2", 12, 12);

            playerFour = new Player(this, new Vector2(graphics.GraphicsDevice.Viewport.Width - forestSprite2.Width,
                                    graphics.GraphicsDevice.Viewport.Height - forestSprite2.Height), forestSprite2);
            playerFour.explosion.Load(Content, "explosionStrip2", 12, 12);

            pepperPowerUp = new PepperPowerUp(this, new Vector2( 300, 300 ));
        }


        void CheckTheScore()
        {
            int winningScore = 10;
            if ((playerOne.score >= winningScore || playerTwo.score >= winningScore || playerThree.score >= winningScore || playerFour.score >= winningScore ) && finalScoreMessageboxHasBeenShown == false)
            {
                System.Windows.Forms.MessageBox.Show("Game over. Final score: player one: " + playerOne.score + " player two: " + playerTwo.score
                    + " player three: " + playerThree.score + " player four: " + playerFour.score);
                finalScoreMessageboxHasBeenShown = true;
            }
        }
                
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Move the sprites around.
            UpdateInputAndMovement(gameTime);
            CheckTheScore();

            audioEngine.Update();

            base.Update(gameTime);
        }

        private void UpdateInputAndMovement(GameTime gameTime)
        {
            GamePadState gamePadStateOne = GamePad.GetState(PlayerIndex.One);
            GamePadState gamePadStateTwo = GamePad.GetState(PlayerIndex.Two);
            GamePadState gamePadStateThree = GamePad.GetState(PlayerIndex.Three);
            GamePadState gamePadStateFour = GamePad.GetState(PlayerIndex.Four);

            PlayerMovement(playerOne, gamePadStateOne);
            PlayerMovement(playerTwo, gamePadStateTwo);
            PlayerMovement(playerThree, gamePadStateThree);
            PlayerMovement(playerFour, gamePadStateFour);

            PlayerShooting(playerOne, gamePadStateOne);
            PlayerShooting(playerTwo, gamePadStateTwo);
            PlayerShooting(playerThree, gamePadStateThree);
            PlayerShooting(playerFour, gamePadStateFour);

            PlayerTiming(playerOne);
            PlayerTiming(playerTwo);
            PlayerTiming(playerThree);
            PlayerTiming(playerFour);

            destroyOffScreenSprites(playerOne.projectiles);
            destroyOffScreenSprites(playerTwo.projectiles);
            destroyOffScreenSprites(playerThree.projectiles);
            destroyOffScreenSprites(playerFour.projectiles);

            checkPlayerPlayerCollision(playerOne, playerTwo);
            checkPlayerPlayerCollision(playerOne, playerThree);
            checkPlayerPlayerCollision(playerOne, playerFour);
            checkPlayerPlayerCollision(playerTwo, playerThree);
            checkPlayerPlayerCollision(playerTwo, playerFour);
            checkPlayerPlayerCollision(playerThree, playerFour);

            HandleSpriteBoundries(playerOne);
            HandleSpriteBoundries(playerTwo);
            HandleSpriteBoundries(playerThree);
            HandleSpriteBoundries(playerFour);

            powerUpHandleing(playerOne, pepperPowerUp);
            powerUpHandleing(playerTwo, pepperPowerUp);
            powerUpHandleing(playerThree, pepperPowerUp);
            powerUpHandleing(playerFour, pepperPowerUp);


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
            // TODO: Create Keyboard input for firing

            // PlayerTwo Keyboard Input
            // Caleb movement
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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(backColor);

            // Draw the sprites.
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            if (playerOne.bulletCollision || playerOne.playerCollision)
                playerOne.explosion.DrawFrame(spriteBatch, playerOne.explosionSpritePosition);
            else if (!(playerOne.bulletCollision || playerOne.playerCollision))
                spriteBatch.Draw(playerOne.sprite, playerOne.position, Color.White);

            if (playerTwo.bulletCollision || playerTwo.playerCollision)
                playerTwo.explosion.DrawFrame(spriteBatch, playerTwo.explosionSpritePosition);
            else if (!(playerTwo.bulletCollision || playerTwo.playerCollision))
                spriteBatch.Draw(playerTwo.sprite, playerTwo.position, Color.White);

            if (playerThree.bulletCollision || playerThree.playerCollision)
                playerThree.explosion.DrawFrame(spriteBatch, playerThree.explosionSpritePosition);
            else if (!(playerThree.bulletCollision || playerThree.playerCollision))
                spriteBatch.Draw(playerThree.sprite, playerThree.position, Color.White);

            if (playerFour.bulletCollision || playerFour.playerCollision)
                playerFour.explosion.DrawFrame(spriteBatch, playerFour.explosionSpritePosition);
            else if (!(playerFour.bulletCollision || playerFour.playerCollision))
                spriteBatch.Draw(playerFour.sprite, playerFour.position, Color.White);

            //draws shields - The fourth varaiable in the new color controls opacity which gives the shield a fade as it runs out
            if (playerOne.shieldUp == true)
                spriteBatch.Draw(shieldSprite, playerOne.shieldSpritePosition, new Color(255, 255, 255, (byte)((playerOne.shieldUpCounter / (shieldTimeAfterDeath / 185.0)) + 70)));
            if (playerTwo.shieldUp == true)
                spriteBatch.Draw(shieldSprite, playerTwo.shieldSpritePosition, new Color(255, 255, 255, (byte)((playerTwo.shieldUpCounter / (shieldTimeAfterDeath / 185.0)) + 70)));
            if (playerThree.shieldUp == true)
                spriteBatch.Draw(shieldSprite, playerThree.shieldSpritePosition, new Color(255, 255, 255, (byte)((playerThree.shieldUpCounter / (shieldTimeAfterDeath / 185.0)) + 70)));
            if (playerFour.shieldUp == true)
                spriteBatch.Draw(shieldSprite, playerFour.shieldSpritePosition, new Color(255, 255, 255, (byte)((playerFour.shieldUpCounter / (shieldTimeAfterDeath / 185.0)) + 70)));

            foreach (Projectile Projectile in playerOne.projectiles)
                spriteBatch.Draw(Projectile.sprite, Projectile.position, Color.White);
            foreach (Projectile Projectile in playerTwo.projectiles)
                spriteBatch.Draw(Projectile.sprite, Projectile.position, Color.White);
            foreach (Projectile Projectile in playerThree.projectiles)
                spriteBatch.Draw(Projectile.sprite, Projectile.position, Color.White);
            foreach (Projectile Projectile in playerFour.projectiles)
                spriteBatch.Draw(Projectile.sprite, Projectile.position, Color.White);

            spriteBatch.Draw(pepperPowerUp.sprite, pepperPowerUp.position, Color.White);

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
            // if the player is currently blowing up then skip movement
            if (!(playerObj.playerCollision == true || playerObj.bulletCollision == true))
            {
                playerObj.position.X += gamePadState.ThumbSticks.Left.X * playerObj.speed;
                playerObj.position.Y += gamePadState.ThumbSticks.Left.Y * -playerObj.speed;
            }
        }

        ///<summary>
        /// PlayerShooting handles the joystick shooting for the player object
        ///</summary>
        ///<param name="playerObj">The player object to move</param>
        ///<param name="gamePadState">The gamePadState controlling the movement</param>
        void PlayerShooting(Player playerObj, GamePadState gamePadState)
        {

            // This loop adds new projectiles when the player is shooting
            if (!(playerObj.playerCollision == true || playerObj.bulletCollision == true))
            {
                if (playerObj.projectileFrequencyCounter > playerObj.projectileFrequency && gamePadState.IsConnected)
                {
                    playerObj.projectileFrequencyCounter = 0;
                    if ((gamePadState.ThumbSticks.Right.X < 0.09 && gamePadState.ThumbSticks.Right.X > -0.09) 
                        && (gamePadState.ThumbSticks.Right.Y < 0.09 && gamePadState.ThumbSticks.Right.Y > -0.09))
                        return;

                    // The following changes the projectile texture depending on the power up
                    if (playerObj.powerUpType == Sprite.powerUpTypes.none)
                    {
                        playerObj.projectiles.Add(new Projectile(this,
                            new Vector2(playerObj.position.X + 30, playerObj.position.Y + 55),
                            new GamePadState(gamePadState.ThumbSticks, gamePadState.Triggers, gamePadState.Buttons, gamePadState.DPad)));
                        soundBank.PlayCue("splat");
                    }
                    if (playerObj.powerUpType == Sprite.powerUpTypes.Pepper)
                    {
                        playerObj.projectiles.Add(new Projectile(this,
                            new Vector2(playerObj.position.X + 30, playerObj.position.Y + 55),
                            new GamePadState(gamePadState.ThumbSticks, gamePadState.Triggers, gamePadState.Buttons, gamePadState.DPad),
                            pepperPowerUp.projectileSprite, pepperPowerUp.projectileSpeed));
                        soundBank.PlayCue("splat");
                    }
                }
            }

            // this loop checks for collisions and updates the projectile movement as well as sets the texture for the projectile
            for (int i = 0; i < playerObj.projectiles.Count; i++)
            {
                playerObj.projectiles[i].position =
                    new Vector2(playerObj.projectiles[i].position.X + playerObj.projectiles[i].gamePadState.ThumbSticks.Right.X * playerObj.projectiles[i].speed,
                    playerObj.projectiles[i].position.Y + playerObj.projectiles[i].gamePadState.ThumbSticks.Right.Y * -playerObj.projectiles[i].speed);

                //Check for collisions 
                checkPlayerBulletCollision(playerObj, playerObj.projectiles[i]);
            }
            playerObj.projectileFrequencyCounter++;
        }

        ///<summary>
        /// PlayerTiming handles the of the timing for shields and explosions
        ///</summary>
        void PlayerTiming(Player playerObj)
        {
            if (playerObj.bulletCollision == true || playerObj.playerCollision == true)
            {
                playerObj.headExplodeCounter++;
            }
            if (playerObj.shieldUp == true)
                playerObj.shieldUpCounter--;
            if (playerObj.shieldUpCounter < 1)
            {
                playerObj.shieldUp = false;
                playerObj.shieldUpCounter = shieldTimeAfterDeath;
            }
            if (headExplodeDownTime < playerObj.headExplodeCounter)
            {
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

        ///<summary>
        /// checkPlayerBulletCollision handles checking weather a projectile object has hit any of the four players
        ///</summary>
        void checkPlayerBulletCollision(Player playerObj, Projectile projectileObj)
        {
            //if we are not player one and we hit player one
            if (playerOne.rectangle.Intersects(projectileObj.rectangle) && playerOne.shieldUp == false && !(playerObj.Equals(playerOne))) 
            {
                if (playerOne.bulletCollision == false)
                {
                    playerObj.score++;
                    soundBank.PlayCue("explosion6");
                }
                playerOne.bulletCollision = true;

            }
            //if we are not player two and we hit player two
            if (playerTwo.rectangle.Intersects(projectileObj.rectangle) && playerTwo.shieldUp == false && !(playerObj.Equals(playerTwo))) 
            {
                if (playerTwo.bulletCollision == false)
                {
                    playerObj.score++;
                    soundBank.PlayCue("explosion6");
                }
                playerTwo.bulletCollision = true;

            }
            //if we are not player three and we hit player three
            if (playerThree.rectangle.Intersects(projectileObj.rectangle) && playerThree.shieldUp == false && !(playerObj.Equals(playerThree))) 
            {
                if (playerThree.bulletCollision == false)
                {
                    playerObj.score++;
                    soundBank.PlayCue("explosion6");
                }
                playerThree.bulletCollision = true;

            }
            //if we are not player four and we hit player four
            if (playerFour.rectangle.Intersects(projectileObj.rectangle) && playerFour.shieldUp == false && !(playerObj.Equals(playerFour))) 
            {
                if (playerFour.bulletCollision == false)
                {
                    playerObj.score++;
                    soundBank.PlayCue("explosion6");
                }
                playerFour.bulletCollision = true;

            }
        }

        ///<summary>
        /// checkPlayerPlayerCollision handles checking weather a player object has hit any other player
        ///</summary>
        void checkPlayerPlayerCollision( Player playerObjOne, Player playerObjTwo )
        {
            if (playerObjOne.shieldUp == false && playerObjTwo.shieldUp == false)
            {
                if (playerObjOne.rectangle.Intersects(playerObjTwo.rectangle))
                {
                    playerObjOne.playerCollision = true;
                    playerObjTwo.playerCollision = true;
                    if (playerObjOne.headExplodeCounter < 1)
                        soundBank.PlayCue("explosion6");
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
                // Remove any of player one's projectiles that have moved out of the visible area
                if (projectiles[i].position.X > graphics.GraphicsDevice.Viewport.Width + 100)
                {
                    projectiles.RemoveAt(i);
                    break;
                }
                else if (projectiles[i].position.Y > graphics.GraphicsDevice.Viewport.Height + 100)
                {
                    projectiles.RemoveAt(i);
                    break;
                }
                else if (projectiles[i].position.X < -100)
                {
                    projectiles.RemoveAt(i);
                    break;
                }
                else if (projectiles[i].position.Y < -100)
                {
                    projectiles.RemoveAt(i);
                    break;
                }
            }
        }

        void powerUpHandleing( Player playerObj, PowerUp powerUpObj )
        {
            if (playerObj.rectangle.Intersects(powerUpObj.rectangle))
            {
                // ToDO: create powerup types that can be matched to the player
                playerObj.powerUpType = powerUpObj.powerUpType;
                if (playerObj.powerUpType == Sprite.powerUpTypes.Pepper)
                {
                    playerObj.projectileFrequency = pepperPowerUp.projectileFrequency;
                    playerObj.speed = pepperPowerUp.playerSpeed;
                    playerObj.projectileSpeed = pepperPowerUp.projectileSpeed;
                }
            }
        }
        
        /// <summary>
        /// This handles restraining sprites to viewport area
        /// </summary>
        void HandleSpriteBoundries(Player playerObj)
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
}
