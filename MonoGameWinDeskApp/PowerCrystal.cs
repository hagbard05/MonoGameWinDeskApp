using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using AnimatedSprite;

namespace MyFirstGame 
{
    public class PowerCrystal : Sprite
    {
        public float speed;
        public float defaultSpeed = 0.017f;

        public PowerCrystal(MainGame game, Vector2 crystalPosition)
        {
            speed = defaultSpeed;
			this.sprite = game.powerCrystalTex;
            position = crystalPosition;
        }
    }
}
