using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content; //using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using AnimatedSprite;
using SharpDX.DirectWrite;

namespace MyFirstGame
{
    public partial class MainGame : Microsoft.Xna.Framework.Game
    {
        private void UpdateInputAndMovement(GameTime gameTime)
        {
            GamePadState gamePadStateOne = GamePad.GetState(PlayerIndex.One);
            GamePadState gamePadStateTwo = GamePad.GetState(PlayerIndex.Two);
            GamePadState gamePadStateThree = GamePad.GetState(PlayerIndex.Three);
            GamePadState gamePadStateFour = GamePad.GetState(PlayerIndex.Four);

            // poll for start press on controller
            #region pollForStart

            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed && previousGamePadState1.Buttons.Start == ButtonState.Released && playerOne.hasStarted == false)
            {
                //if (!(Guide.IsTrialMode && numPlayersStarted >= 2))
                //{
                    playerOne.hasStarted = true;
                    players.Add(playerOne);
                    numPlayersStarted++;
                    gameOver = false;
                //}
            }
            if (GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed && previousGamePadState2.Buttons.Start == ButtonState.Released && playerTwo.hasStarted == false)
            {
                //if (!(Guide.IsTrialMode && numPlayersStarted >= 2))
                //{
                    playerTwo.hasStarted = true;
                    players.Add(playerTwo);
                    numPlayersStarted++;
                    gameOver = false;
                //}
            }
            if (GamePad.GetState(PlayerIndex.Three).Buttons.Start == ButtonState.Pressed && previousGamePadState3.Buttons.Start == ButtonState.Released && playerThree.hasStarted == false)
            {
                //if (!(Guide.IsTrialMode && numPlayersStarted >= 2))
                //{
                    playerThree.hasStarted = true;
                    players.Add(playerThree);
                    numPlayersStarted++;
                    gameOver = false;
                //}
            }
            if (GamePad.GetState(PlayerIndex.Four).Buttons.Start == ButtonState.Pressed && previousGamePadState4.Buttons.Start == ButtonState.Released && playerFour.hasStarted == false)
            {
                //if (!(Guide.IsTrialMode && numPlayersStarted >= 2))
                {
                    playerFour.hasStarted = true;
                    players.Add(playerFour);
                    numPlayersStarted++;
                    gameOver = false;
                }
            }

			// Keyboard input for starting
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad1) && playerOne.hasStarted == false)
            {
                //if (!(Guide.IsTrialMode && numPlayersStarted >= 2))
                {
                    playerOne.hasStarted = true;
                    numPlayersStarted++;
                    gameOver = false;
                }
            }
			if (Keyboard.GetState().IsKeyDown(Keys.NumPad2) && playerTwo.hasStarted == false)
			{
				//if (!(Guide.IsTrialMode && numPlayersStarted >= 2))
				{
					playerTwo.hasStarted = true;
					numPlayersStarted++;
					gameOver = false;
				}
			}
			if (Keyboard.GetState().IsKeyDown(Keys.NumPad3) && playerThree.hasStarted == false)
			{
				//if (!(Guide.IsTrialMode && numPlayersStarted >= 2))
				{
					playerThree.hasStarted = true;
					numPlayersStarted++;
					gameOver = false;
				}
			}
			if (Keyboard.GetState().IsKeyDown(Keys.NumPad4) && playerFour.hasStarted == false)
			{
				//if (!(Guide.IsTrialMode && numPlayersStarted >= 2))
				{
					playerFour.hasStarted = true;
					numPlayersStarted++;
					gameOver = false;
				}
			}

            #endregion

			//if (gamePadStateOne.Buttons.Back == ButtonState.Pressed && playerOne.hasStarted == true) // ToDo: enable exiting on back button press and save the game for resumeing on exit
			//    this.Exit();

			if (Keyboard.GetState().IsKeyDown(Keys.Escape) )
				this.Exit();

            if ((playerOne.lives <= 0 || playerOne.hasStarted == false) && (playerTwo.lives <= 0 || playerTwo.hasStarted == false) && (playerThree.lives <= 0 || playerThree.hasStarted == false) && (playerFour.lives <= 0 || playerFour.hasStarted == false))
            {
                gameOver = true;
            }

            if (playerOne.hasStarted && playerOne.lives > 0)
            {
                PlayerMovement(playerOne, gamePadStateOne);
                PlayerShooting(playerOne, gamePadStateOne);
                PlayerTiming(playerOne);
            }
            if (playerTwo.hasStarted && playerTwo.lives > 0)
            {
                PlayerMovement(playerTwo, gamePadStateTwo);
                PlayerShooting(playerTwo, gamePadStateTwo);
                PlayerTiming(playerTwo);
            }
            if (playerThree.hasStarted && playerThree.lives > 0)
            {
                PlayerMovement(playerThree, gamePadStateThree);
                PlayerShooting(playerThree, gamePadStateThree);
                PlayerTiming(playerThree);
            }
            if (playerFour.hasStarted && playerFour.lives > 0)
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
                if (level > levelToAddWobbleKama)
                {
                    spawnWobbleKamikazes(wobbleKSpanRate + (level * 3));
                }
                if (level > levelToAddKama)
                {
                    spawnKamikazesAboveScreen(kamikazeSpawnRate + (level * 3));  // spawn enemys above screen
                    spawnKamikazesBelowScreen(kamikazeSpawnRate + (level * 3));
                    spawnKamikazesLeftOfScreen(kamikazeSpawnRate + (level * 3));
                    spawnKamikazesRightOfScreen(kamikazeSpawnRate + (level * 3));
                }
                spawnCrystalGrabbersAboveScreen(crystalGrabberSpawnRate + (level * 1));
                spawnCrystalGrabbersBelowScreen(crystalGrabberSpawnRate + (level * 3));
                spawnCrystalGrabbersRightOfScreen(crystalGrabberSpawnRate + (level * 3));
                spawnCrystalGrabbersLeftOfScreen(crystalGrabberSpawnRate + (level * 3));

                CheckForCrystalOffScreen();

                KamikazeMovement();
                wobbleKamikazeMovement();
                CrystalGrabberMovement();

                foreach (WobbleKamikaze wk in wobbleKamikazes)
                {
                    checkPlayerSpriteCollision(playerOne, wk);
                    checkPlayerSpriteCollision(playerTwo, wk);
                    checkPlayerSpriteCollision(playerThree, wk);
                    checkPlayerSpriteCollision(playerFour, wk);
                    if (wobbleKamikazeTiming(wk))
                        break;
                }

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

                spawnPowerUp(extraLifePowerUpSpawnRate, Sprite.powerUpTypes.ExtraLife, new Random(randomSeed));
                spawnPowerUp(superBombPowerUpSpawnRate, Sprite.powerUpTypes.SuperBomb, new Random(randomSeed));
                spawnPowerUp(shieldPowerUpSpawnRate, Sprite.powerUpTypes.Shield, new Random(randomSeed));
                spawnPowerUp(speedPowerUpSpawnRate, Sprite.powerUpTypes.Speed, new Random(randomSeed));
                spawnPowerUp(pepperPowerUpSpawnRate, Sprite.powerUpTypes.Pepper, new Random(randomSeed));
                spawnPowerUp(invincibleBulletPowerUpSpawnRate, Sprite.powerUpTypes.InvincibleBullet, new Random(randomSeed));

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
                KeyboardState newState2 = Keyboard.GetState();
                if (!(playerOne.bulletCollision == true || playerOne.playerCollision == true))
                {
                    if (newState.IsKeyDown(Keys.A) || newState.IsKeyDown(Keys.S) || newState.IsKeyDown(Keys.D) || newState.IsKeyDown(Keys.W))
                    {
                        double spread = 0.2;
                        Vector2 shotVec = new Vector2(0,0);
                        Vector2 leftShotVec;
                        Vector2 rightShotVec;
						float panPos = convertPositionToPanValue(playerOne.position.X);

                        if (newState.IsKeyDown(Keys.A))
                        {
                            shotVec.X = -1;
						}
						if (newState.IsKeyDown(Keys.D))
						{
							shotVec.X = 1;
						}
						if (newState.IsKeyDown(Keys.S))
						{
                            shotVec.Y = -1;
						}
						if (newState.IsKeyDown(Keys.W))
						{
                            shotVec.Y = 1;
						}

						// The following changes the projectile texture depending on the power up
						if (playerOne.PepperPowerup == false && playerOne.InvincBulletPowerUp == false)
                        {
                            playerOne.projectiles.Add(new Projectile(this, new Vector2(playerOne.CenterPosition.X - 10, playerOne.CenterPosition.Y - 6), shotVec));
                            normalShot.Play(0.4f, -0.4f, panPos);
                        }

                        if (playerOne.PepperPowerup == true && playerOne.InvincBulletPowerUp == false)
                        {
                            rightShotVec = VectorOps.RotateVector(shotVec, -1 * spread);
                            leftShotVec = VectorOps.RotateVector(shotVec, spread);
                            Vector2 center = new Vector2(playerOne.CenterPosition.X - 24, playerOne.CenterPosition.Y - 24);

                            playerOne.projectiles.Add(new Projectile(this, center, shotVec, PepperPowerUp.projectileSprite, playerOne.projectileSpeed));
                            playerOne.projectiles.Add(new Projectile(this, center, leftShotVec, PepperPowerUp.projectileSprite, playerOne.projectileSpeed));
                            playerOne.projectiles.Add(new Projectile(this, center, rightShotVec, PepperPowerUp.projectileSprite, playerOne.projectileSpeed));
                            fireBallShot.Play(0.8f, -0.8f, panPos);
                        }
                        if (playerOne.PepperPowerup == false && playerOne.InvincBulletPowerUp == true)
                        {
                            playerOne.projectiles.Add(new Projectile(this, new Vector2(playerOne.CenterPosition.X - 14, playerOne.CenterPosition.Y - 14), shotVec, InvincibleBulletPowerUp.projectileSprite, playerOne.projectileSpeed, true));
                            unstopaballShot.Play(1.0f, -0.5f, panPos);
                        }
                        if (playerOne.PepperPowerup == true && playerOne.InvincBulletPowerUp == true)
                        {
                            Vector2 center = new Vector2(playerOne.CenterPosition.X - 24, playerOne.CenterPosition.Y - 24);
                            double spread2 = 0.1;
                            Vector2 leftShotVec2 = VectorOps.RotateVector(shotVec, spread2);
                            Vector2 rightShotVec2 = VectorOps.RotateVector(shotVec, -1 * spread2);
                            //playerOne.projectiles.Add(new Projectile(this, center, shotVec, fireBallMetalSphere, playerOne.projectileSpeed, true));
                            playerOne.projectiles.Add(new Projectile(this, center, leftShotVec2, fireBallMetalSphere, playerOne.projectileSpeed, true));
                            playerOne.projectiles.Add(new Projectile(this, center, rightShotVec2, fireBallMetalSphere, playerOne.projectileSpeed, true));

                            unstopaballShot.Play(0.7f, -0.9f, panPos);
                            fireBallShot.Play(0.4f, -0.7f, panPos);
                        }
                    }
				}
            }

            previousGamePadState1 = GamePad.GetState(PlayerIndex.One);
            previousGamePadState2 = GamePad.GetState(PlayerIndex.Two);
            previousGamePadState3 = GamePad.GetState(PlayerIndex.Three);
            previousGamePadState4 = GamePad.GetState(PlayerIndex.Four);

        }

		///<summary>
		/// PlayerShooting handles the joystick shooting for the player object
		///</summary>
		///<param name="playerObj">The player object to move</param>
		///<param name="gamePadState">The gamePadState controlling the movement</param>
		void PlayerShooting(Player playerObj, GamePadState gamePadState)
		{
			if (playerObj.hasStarted && playerObj.lives > 0)
			{
				// Check for super Bomb dropage
				if (gamePadState.Buttons.RightShoulder == ButtonState.Pressed && playerObj.superBombs > 0 && playerObj.superBombTimer.expired(gameTime1))
				{
					dropTheSuperBomb();
					playerObj.superBombs--;
					playerObj.score += 42;
					playerObj.superBombTimer.reset(gameTime1);
					Explosion.Play(1.0f, -1.0f, 0.1f);
					Explosion.Play(1.0f, -0.4f, 0.1f);
				}

				// this loop checks for collisions and updates the projectile movement 
				//for (int i = 0; i < playerObj.projectiles.Count; i++)
				foreach (Projectile projectile in playerObj.projectiles)
				{
					projectile.position = new Vector2(projectile.position.X + projectile.direction.X * projectile.speed, projectile.position.Y + projectile.direction.Y * -projectile.speed);

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
					checkPlayerShieldBulletCollision(projectile, playerObj);  // ToDo: setup checking collisions between player bullets probibly only for deathmatch games
				}

				// This loop adds new projectiles when the player is shooting
				if (!(playerObj.playerCollision == true || playerObj.bulletCollision == true))
				{
					if (playerObj.projectileFrequencyTimer.expired(gameTime1) == true && gamePadState.IsConnected)
					{
						float panPos = convertPositionToPanValue(playerObj.position.X);
						playerObj.projectileFrequencyTimer.reset(gameTime1);
						Vector2 shotVec = new Vector2(gamePadState.ThumbSticks.Right.X, gamePadState.ThumbSticks.Right.Y);

						// --->  Dead Center for shooting stick.  (If the stick is in the middle dont shoot)
						float deadCenterRange = 0.04f;


						// Keyboard input for player one
						if (Keyboard.GetState().IsKeyDown(Keys.D))
						{
							shotVec = new Vector2(1, 0);
						}

						if ((shotVec.X < deadCenterRange && shotVec.X > -1 * deadCenterRange) && (shotVec.Y < deadCenterRange && shotVec.Y > -1 * deadCenterRange))
						{
							return;
						}
						else
						{
							double spread = 0.2;
							Vector2 leftShotVec;
							Vector2 rightShotVec;

							// The following changes the projectile texture depending on the power up
							if (playerObj.PepperPowerup == false && playerObj.InvincBulletPowerUp == false)
							{
								playerObj.projectiles.Add(new Projectile(this, new Vector2(playerObj.CenterPosition.X - 10, playerObj.CenterPosition.Y - 6), shotVec));
								normalShot.Play(0.4f, -0.4f, panPos);
							}

							if (playerObj.PepperPowerup == true && playerObj.InvincBulletPowerUp == false)
							{
								rightShotVec = VectorOps.RotateVector(shotVec, -1 * spread);
								leftShotVec = VectorOps.RotateVector(shotVec, spread);
								Vector2 center = new Vector2(playerObj.CenterPosition.X - 24, playerObj.CenterPosition.Y - 24);

								playerObj.projectiles.Add(new Projectile(this, center, shotVec, PepperPowerUp.projectileSprite, playerObj.projectileSpeed));
								playerObj.projectiles.Add(new Projectile(this, center, leftShotVec, PepperPowerUp.projectileSprite, playerObj.projectileSpeed));
								playerObj.projectiles.Add(new Projectile(this, center, rightShotVec, PepperPowerUp.projectileSprite, playerObj.projectileSpeed));
								fireBallShot.Play(0.8f, -0.8f, panPos);
							}
							if (playerObj.PepperPowerup == false && playerObj.InvincBulletPowerUp == true)
							{
								playerObj.projectiles.Add(new Projectile(this, new Vector2(playerObj.CenterPosition.X - 14, playerObj.CenterPosition.Y - 14), shotVec, InvincibleBulletPowerUp.projectileSprite, playerObj.projectileSpeed, true));
								unstopaballShot.Play(1.0f, -0.5f, panPos);
							}
							if (playerObj.PepperPowerup == true && playerObj.InvincBulletPowerUp == true)
							{
								Vector2 center = new Vector2(playerObj.CenterPosition.X - 24, playerObj.CenterPosition.Y - 24);
								double spread2 = 0.1;
								Vector2 leftShotVec2 = VectorOps.RotateVector(shotVec, spread2);
								Vector2 rightShotVec2 = VectorOps.RotateVector(shotVec, -1 * spread2);
								//playerObj.projectiles.Add(new Projectile(this, center, shotVec, fireBallMetalSphere, playerObj.projectileSpeed, true));
								playerObj.projectiles.Add(new Projectile(this, center, leftShotVec2, fireBallMetalSphere, playerObj.projectileSpeed, true));
								playerObj.projectiles.Add(new Projectile(this, center, rightShotVec2, fireBallMetalSphere, playerObj.projectileSpeed, true));

								unstopaballShot.Play(0.7f, -0.9f, panPos);
								fireBallShot.Play(0.4f, -0.7f, panPos);
							}

						} // end else
					} // end if
				} // end if 
			} // end if 
		} 
    }
}
