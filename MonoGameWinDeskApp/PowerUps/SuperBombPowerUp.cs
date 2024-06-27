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
    class SuperBombPowerUp : PowerUp
    {
        public float speed;
        public Vector2 direction;

        public SuperBombPowerUp(MainGame game, Vector2 Position, Vector2 Directon, float speed)
        {
            position = Position;
            direction = Directon;
            sprite = game.bomb;
            this.speed = speed;
            powerUpType = powerUpTypes.SuperBomb;
        }
    }
}
