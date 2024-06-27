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
    class SpeedPowerUp : PowerUp
    {
        public float speed;
        public Vector2 direction;

        public static int playerSpeedModifier;

        public static float timeout = 20000; // in miliseconds 1000 = 1 second

        int defaultPlayerSpeedModifier = 4;

        public SpeedPowerUp(MainGame game, Vector2 Position, Vector2 Directon, float speed)
        {
			if (Guide.IsTrialMode == true)
			{
				timeout = 5000;
			}
            position = Position;
            direction = Directon;
            sprite = game.speedPowUpTex;
            powerUpType = powerUpTypes.Speed;
            this.speed = speed;

            playerSpeedModifier = defaultPlayerSpeedModifier;
        }
    }
}
