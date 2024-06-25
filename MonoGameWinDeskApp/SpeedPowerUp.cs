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
    class SpeedPowerUp : PowerUp
    {
        public int speed;
        public Vector2 direction;

        public static int playerSpeedModifier;

        public static float timeout = 20000; // in miliseconds 1000 = 1 second

        int defaultPlayerSpeedModifier = 4;

        public SpeedPowerUp(MainGame game, Vector2 Position, Vector2 Directon, int Speed)
        {
            position = Position;
            direction = Directon;
            sprite = game.Content.Load<Texture2D>("speed");
            powerUpType = powerUpTypes.Speed;
            speed = Speed;

            playerSpeedModifier = defaultPlayerSpeedModifier;
        }
    }
}
