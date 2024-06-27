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

    class ShieldPowerUp : PowerUp
    {
        public float speed;
        public Vector2 direction;

        public static int projectileSpeedModifier;
        public static int playerSpeedModifier;
        public static float projectileFrequencyModifier; // in miliseconds
        public static float timeout = 20000; // in miliseconds 1000 = 1 second
        public static float shieldTime = 500;

        int defaultProjectileFrequencyModifier = -70;
        int defaultProjectileSpeedModifier = -5;
        int defaultPlayerSpeedModifier = 2;

        public ShieldPowerUp(MainGame game, Vector2 Position, Vector2 Directon, float speed)
        {
            if (Guide.IsTrialMode == true)
            {
                timeout = 5000;
            }
            position = Position;
            direction = Directon;
            sprite = game.shieldPowUpTex;
            powerUpType = powerUpTypes.Shield;
            this.speed = speed;

            projectileFrequencyModifier = defaultProjectileFrequencyModifier;
            projectileSpeedModifier = defaultProjectileSpeedModifier;
            playerSpeedModifier = defaultPlayerSpeedModifier;
        }


    }
}
