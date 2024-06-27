using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyFirstGame 
{
    class InvincibleBulletPowerUp : PowerUp
    {
        public float speed;
        public Vector2 direction;

        public static int projectileSpeedModifier;
        public static int playerSpeedModifier;
        public static float projectileFrequencyModifier; // in miliseconds
        public static Texture2D projectileSprite;
        public static float timeout = 20000; // in miliseconds 1000 = 1 second

        int defaultProjectileFrequencyModifier = +120; // the higher the number the lower the frequency
        int defaultProjectileSpeedModifier = -20; 
        int defaultPlayerSpeedModifier = 0;

        public InvincibleBulletPowerUp(MainGame game, Vector2 Position, Vector2 Directon, float speed)
        {
            if (Guide.IsTrialMode == true)
            {
                timeout = 5000;
            }
            position = Position;
            direction = Directon;
            sprite = game.metelSpherePowerUp;
            projectileSprite = game.metelSpherePowerUpProjectileSprite;
            powerUpType = powerUpTypes.InvincibleBullet;
            this.speed = speed;

            projectileFrequencyModifier = defaultProjectileFrequencyModifier;
            projectileSpeedModifier = defaultProjectileSpeedModifier;
            playerSpeedModifier = defaultPlayerSpeedModifier;
        }
    }
}
