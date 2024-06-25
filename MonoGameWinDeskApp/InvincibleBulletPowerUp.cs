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
    class InvincibleBulletPowerUp : PowerUp
    {
        public int speed;
        public Vector2 direction;

        public static int projectileSpeedModifier;
        public static int playerSpeedModifier;
        public static float projectileFrequencyModifier; // in miliseconds
        public static Texture2D projectileSprite;
        public static float timeout = 20000; // in miliseconds 1000 = 1 second

        int defaultProjectileFrequencyModifier = +120; // the higher the number the lower the frequency
        int defaultProjectileSpeedModifier = -20; 
        int defaultPlayerSpeedModifier = 0;

        public InvincibleBulletPowerUp(MainGame game, Vector2 Position, Vector2 Directon, int Speed)
        {
            position = Position;
            direction = Directon;
            sprite = game.Content.Load<Texture2D>("metelSphere");
            projectileSprite = game.Content.Load<Texture2D>("metelSphereSmall");
            powerUpType = powerUpTypes.InvincibleBullet;
            speed = Speed;

            projectileFrequencyModifier = defaultProjectileFrequencyModifier;
            projectileSpeedModifier = defaultProjectileSpeedModifier;
            playerSpeedModifier = defaultPlayerSpeedModifier;
        }
    }
}
