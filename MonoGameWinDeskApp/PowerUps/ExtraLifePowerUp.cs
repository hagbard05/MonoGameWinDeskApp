using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyFirstGame
{

    class ExtraLifePowerUp : PowerUp
    {
        public float speed;
        public Vector2 direction;

        public ExtraLifePowerUp(MainGame game, Vector2 Position, Vector2 Directon, float speed)
        {
            position = Position;
            direction = Directon;
            sprite = game.oneUpTex;
            powerUpType = powerUpTypes.ExtraLife;
            this.speed = speed;
        }
    }
}
