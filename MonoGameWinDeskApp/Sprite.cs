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
    public class Sprite
    {
        public Vector2 position;
        public enum powerUpTypes { Pepper, InvincibleBullet, SuperBomb, Shield, Speed, ExtraLife, none };
        public powerUpTypes powerUpType;
        public Texture2D sprite;
        public bool bulletCollision;
        public bool playerCollision;

		public virtual Rectangle rectangle { get { return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height); } }
		public Vector2 CenterPosition { get { return new Vector2(position.X - sprite.Width / 2, position.Y - sprite.Height / 2); } }


	} // end class

}
