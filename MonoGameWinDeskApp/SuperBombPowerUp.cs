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
    class SuperBombPowerUp : PowerUp
    {
        public int speed;
        public Vector2 direction;

        public SuperBombPowerUp(MainGame game, Vector2 Position, Vector2 Directon, int Speed)
        {
            position = Position;
            direction = Directon;
            sprite = game.Content.Load<Texture2D>("H-Bomb");
            speed = Speed;
            powerUpType = powerUpTypes.SuperBomb;
        }
    }
}
