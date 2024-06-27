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
    public partial class MainGame : Microsoft.Xna.Framework.Game
    {

        // Power up grab sounds
        SoundEffect speedPowUpGrab;
        SoundEffect lifePowUpGrab;
        SoundEffect pepperPowUpGrab;
        SoundEffect unstopPowUpGrab;
        SoundEffect shieldPowUpGrab;
        SoundEffect daBombPowUpGrab;

#if DEBUG
	    public int levelToAddExLifePowerUp = 0;
		public int levelToAddPepPowerUp = 0;
		public int levelToAddUnstopaBallPowerUp = 0;
		public int levelToAddShieldPowerUp = 0;
		public int levelToAddSpeedPowerUp = 0;
		public int levelToAddSuperBombPowerUp = 0;

		public static int pepperPowerUpSpawnRateDefault = 10;
		public static int invincibleBulletPowerUpSpawnRateDefault = 10;
		public static int superBombPowerUpSpawnRateDefault = 10;
		public static int shieldPowerUpSpawnRateDefault = 10;
		public static int speedPowerUpSpawnRateDefault = 10;
		public static int extraLifePowerUpSpawnRateDefault = 10;
#else
		public int levelToAddExLifePowerUp = 4; // will not show in trial mode
		public int levelToAddPepPowerUp = 1;
		public int levelToAddUnstopaBallPowerUp = 3;
		public int levelToAddShieldPowerUp = 2;
		public int levelToAddSpeedPowerUp = 0;
		public int levelToAddSuperBombPowerUp = 4; // will not show in trial mode

		public static int pepperPowerUpSpawnRateDefault = 2;
		public static int invincibleBulletPowerUpSpawnRateDefault = 2;
		public static int superBombPowerUpSpawnRateDefault = 2;
		public static int shieldPowerUpSpawnRateDefault = 2;
		public static int speedPowerUpSpawnRateDefault = 2;
		public static int extraLifePowerUpSpawnRateDefault = 2;
#endif

        /// <summary>
        /// This handles modifying the player settings to powerups
        /// </summary>
        bool powerUpHandleing(Player playerObj, PowerUp powerUpObj)
        {
            bool powerUpHasBeenGrabbedResult = false;

            if (!(playerObj == null))
            {
                if (playerObj.rectangle.Intersects(powerUpObj.rectangle))
                {
                    // ToDo: Create powerup types that can be matched to the player (Maybe)
                    playerObj.powerUpType = powerUpObj.powerUpType;

                    if (powerUpObj.powerUpType == Sprite.powerUpTypes.Pepper && playerObj.PepperPowerup == true)
                    {
                        playerObj.pepperPowerUpTimeoutTimer.reset(gameTime1);
                        pepperPowUpGrab.Play();
                    }
                    if (powerUpObj.powerUpType == Sprite.powerUpTypes.Pepper && playerObj.PepperPowerup == false)
                    {
                        playerObj.projectileFrequencyTimer.timerDuration += PepperPowerUp.projectileFrequencyModifier;
                        playerObj.speed += PepperPowerUp.playerSpeedModifier;
                        playerObj.projectileSpeed += PepperPowerUp.projectileSpeedModifier;
                        playerObj.PepperPowerup = true;
                        playerObj.pepperPowerUpTimeoutTimer = new Timer(PepperPowerUp.timeout, gameTime1); // constructor starts the timer
                        pepperPowUpGrab.Play();
                    }

                    if (powerUpObj.powerUpType == Sprite.powerUpTypes.InvincibleBullet && playerObj.InvincBulletPowerUp == true)
                    {
                        unstopPowUpGrab.Play();
                        playerObj.invincibleBulletPowerUpTimeoutTimer.reset(gameTime1);
                    }
                    if (powerUpObj.powerUpType == Sprite.powerUpTypes.InvincibleBullet && playerObj.InvincBulletPowerUp == false)
                    {
                        playerObj.projectileFrequencyTimer.timerDuration += InvincibleBulletPowerUp.projectileFrequencyModifier;
                        playerObj.speed += InvincibleBulletPowerUp.playerSpeedModifier;
                        playerObj.projectileSpeed += InvincibleBulletPowerUp.projectileSpeedModifier;
                        playerObj.InvincBulletPowerUp = true;
                        playerObj.invincibleBulletPowerUpTimeoutTimer = new Timer(PepperPowerUp.timeout, gameTime1); // constructor starts the timer
                        unstopPowUpGrab.Play();
                    }

                    if (powerUpObj.powerUpType == Sprite.powerUpTypes.Speed && playerObj.SpeedPowerUp == true)
                    {
                        playerObj.speedPowerUpTimeoutTimer.reset(gameTime1);
                        speedPowUpGrab.Play();
                    }
                    if (powerUpObj.powerUpType == Sprite.powerUpTypes.Speed && playerObj.SpeedPowerUp == false)
                    {
                        playerObj.SpeedPowerUp = true;
                        playerObj.speedPowerUpTimeoutTimer = new Timer(SpeedPowerUp.timeout, gameTime1);
                        playerObj.speed += SpeedPowerUp.playerSpeedModifier;
                        speedPowUpGrab.Play();
                    }

                    if (powerUpObj.powerUpType == Sprite.powerUpTypes.SuperBomb)
                    {
                        playerObj.superBombs++;
                        daBombPowUpGrab.Play();
                    }
                    if (powerUpObj.powerUpType == Sprite.powerUpTypes.ExtraLife)
                    {
                        playerObj.lives++;
                        lifePowUpGrab.Play();
                    }
                    if (powerUpObj.powerUpType == Sprite.powerUpTypes.Shield)
                    {
                        playerObj.ShieldPowerUp = true;
                        playerObj.shieldUpCounter = ShieldPowerUp.shieldTime;
                        playerObj.shieldUp = true;
                        unstopPowUpGrab.Play();
                    }

                    powerUpHasBeenGrabbedResult = true;
                }
            }
            return powerUpHasBeenGrabbedResult;
        }

        /// <summary>
        /// Handles spawning new powerups
        /// </summary>
        void spawnPowerUp(int frequency, Sprite.powerUpTypes powerUpType, Random random)
        {
            debugRandom = random.Next(20000);
            //debugRandom = random1.Next(10000);

            System.Diagnostics.Debug.WriteLine("frequency: " + frequency + "powerUpType: " + powerUpType);
            if (debugRandom * numPlayersStarted < frequency)
            {
                
                if (powerUpType == Sprite.powerUpTypes.Pepper && level >= levelToAddPepPowerUp)
                {
                    powerUp.Play();
                    float randomFloatNumOne = random.Next(200);
                    float randomFloatNumTwo = random4.Next(200);
                    pepperPowerUps.Add(new PepperPowerUp(this, new Vector2(random.Next((int)(viewport.Width)), random.Next((int)(viewport.Height))),
                        new Vector2((randomFloatNumOne / 100) - 1, (randomFloatNumTwo / 100) - 1), random.Next(2) + 1));
                }
                if (powerUpType == Sprite.powerUpTypes.InvincibleBullet && level >= levelToAddUnstopaBallPowerUp)
                {
                    powerUp.Play();
                    float randomFloatNumOne = random2.Next(200);
                    float randomFloatNumTwo = random3.Next(200);
                    InvincibleBulletPowerUps.Add(new InvincibleBulletPowerUp(this, new Vector2(random2.Next((int)(viewport.Width)), random2.Next((int)(viewport.Height))),
                        new Vector2((randomFloatNumOne / 100) - 1, (randomFloatNumTwo / 100) - 1), random2.Next(2) + 1));
                }
                if (powerUpType == Sprite.powerUpTypes.SuperBomb && level > levelToAddSuperBombPowerUp)
                {
					//if (Guide.IsTrialMode == false)
					//{
						powerUp.Play();
						float randomFloatNumOne = random2.Next(200);
						float randomFloatNumTwo = random3.Next(200);
						superBombPowerUps.Add(new SuperBombPowerUp(this, new Vector2(random3.Next((int)(viewport.Width)), random3.Next((int)(viewport.Height))),
							new Vector2((randomFloatNumOne / 100) - 1, (randomFloatNumTwo / 100) - 1), random3.Next(2) + 1));
					//}
                }
                if (powerUpType == Sprite.powerUpTypes.Shield && level > levelToAddShieldPowerUp)
                {
                    powerUp.Play();
                    float randomFloatNumOne = random4.Next(200);
                    float randomFloatNumTwo = random5.Next(200);
					 
                    shieldPowerUps.Add(new ShieldPowerUp(this, new Vector2(random4.Next((int)(viewport.Width)), random4.Next((int)(viewport.Height))),
                        new Vector2((randomFloatNumOne / 100) - 1, (randomFloatNumTwo / 100) - 1), random4.Next(2) + 1));
                }
                if (powerUpType == Sprite.powerUpTypes.Speed && level > levelToAddSpeedPowerUp)
                {
                    powerUp.Play();
                    float randomFloatNumOne = random.Next(200);
                    float randomFloatNumTwo = random5.Next(200);

                    speedPowerUps.Add(new SpeedPowerUp(this, new Vector2(random5.Next((int)(viewport.Width)), random5.Next((int)(viewport.Height))),
                        new Vector2((randomFloatNumOne / 100) - 1, (randomFloatNumTwo / 100) - 1), random5.Next(2) + 1));
                }
                if (powerUpType == Sprite.powerUpTypes.ExtraLife && level > levelToAddExLifePowerUp)
                {
                    //if (Guide.IsTrialMode == false)
                    //{
                        powerUp.Play();
                        float randomFloatNumOne = random6.Next(200);
                        float randomFloatNumTwo = random4.Next(200);

                        extraLifePowerUps.Add(new ExtraLifePowerUp(this, new Vector2(random6.Next((int)(viewport.Width)), random6.Next((int)(viewport.Height))),
                            new Vector2((randomFloatNumOne / 100) - 1, (randomFloatNumTwo / 100) - 1), random6.Next(2) + 1));
                    //}
                }
            }

        } // end method()
    } // End partial class
} // end namespace
