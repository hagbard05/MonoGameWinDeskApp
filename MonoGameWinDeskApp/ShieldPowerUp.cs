using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace MonoGameWinDeskApp
{

    class ShieldPowerUp : PowerUp
    {
        public int speed;
        public Vector2 direction;

        public static int projectileSpeedModifier;
        public static int playerSpeedModifier;
        public static float projectileFrequencyModifier; // in miliseconds
        public static float timeout = 20000; // in miliseconds 1000 = 1 second
        public static float shieldTime = 500;

        int defaultProjectileFrequencyModifier = -70;
        int defaultProjectileSpeedModifier = -5;
        int defaultPlayerSpeedModifier = 2;

        public ShieldPowerUp(MainGame game, Vector2 Position, Vector2 Directon, int Speed)
        {
            position = Position;
            direction = Directon;
            sprite = game.Content.Load<Texture2D>("Sphere75small");
            powerUpType = powerUpTypes.Shield;
            speed = Speed;

            projectileFrequencyModifier = defaultProjectileFrequencyModifier;
            projectileSpeedModifier = defaultProjectileSpeedModifier;
            playerSpeedModifier = defaultPlayerSpeedModifier;
        }


    }
}
