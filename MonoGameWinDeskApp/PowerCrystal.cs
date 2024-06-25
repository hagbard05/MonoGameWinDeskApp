using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using AnimatedSprite;

namespace MonoGameWinDeskApp
{
    public class PowerCrystal : Sprite
    {
        public float speed;
        public float defaultSpeed = 0.04F;

        public PowerCrystal(Game game, Vector2 crystalPosition)
        {
            speed = defaultSpeed;
            sprite = game.Content.Load<Texture2D>("quartz");
            position = crystalPosition;
            position = CenterPosition;
        }
    }
}
