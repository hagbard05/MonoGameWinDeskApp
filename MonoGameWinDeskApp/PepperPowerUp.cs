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
    public class PepperPowerUp : PowerUp
    {
        public float speed;
        public Vector2 direction;

        public static int projectileSpeedModifier;
        public static int playerSpeedModifier;
        public static float projectileFrequencyModifier; // in miliseconds
        public static Texture2D projectileSprite;
        public static float timeout = 20000; // in miliseconds 1000 = 1 second

        int defaultProjectileFrequencyModifier = 99;
        int defaultProjectileSpeedModifier = -14;
        int defaultPlayerSpeedModifier = 1;

        //public PepperPowerUp(MainGame game, Vector2 Position)
        //{
        //    position = Position;
        //    loadDefaults(game);
        //}

        public PepperPowerUp(MainGame game, Vector2 Position, Vector2 Directon, float speed)
        {
            position = Position;
            direction = Directon;
            this.speed = speed;
			loadDefaults(game);
        }

		public void loadDefaults(MainGame game)
		{
            if (Guide.IsTrialMode == true)
            {
                timeout = 5000;
            }
			sprite = game.habanerio;
			projectileSprite = game.fireball;
			powerUpType = powerUpTypes.Pepper;
			projectileFrequencyModifier = defaultProjectileFrequencyModifier;
			projectileSpeedModifier = defaultProjectileSpeedModifier;
			playerSpeedModifier = defaultPlayerSpeedModifier;
		}
    }
}
