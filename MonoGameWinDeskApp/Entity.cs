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
    class Entity : Sprite
    {
        public enum entityTypes { Kamikaze, CrystalGrabber, none };
        public entityTypes EntityType;
        public int headExplodeCounter;
        public int explodeCounter;
        public int explodeDownTime;
        public float speed;
        public int defaultSpeed = 2;
        public Vector2 direction;
        public AnimatedTexture explosion;
    }
}
