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
    /// <summary>
    /// projectile class for projectile objects in the game
    /// </summary>
    public class Projectile
    {

        public Vector2 position;
        public Vector2 direction; 
        public Vector2 inheritedVelocity;
        public Texture2D sprite = null;
        public bool hasHitShield = false;

		private Color[] _spriteTextureData = null;
		public Color[] spriteTextureData
		{
			get 
			{
				if (_spriteTextureData == null)
				{
					if (sprite != null)
					{
						// Extract collision data for per pixle collisions
						_spriteTextureData = new Color[sprite.Width * sprite.Height];
						sprite.GetData(_spriteTextureData);
					}
				}
				return _spriteTextureData; 
			}
		}
		
		private float _speed;
		public float speed
		{
			get
			{
				return _speed;
			}
			set
			{
				if (value < 0.7f)
					_speed = 0.7f;
				else
					_speed = value;
			}
		}
		
		public const int defaultSpeed = 18;
        public bool invincible = false;

        public Projectile(MainGame game, Vector2 pos, Vector2 projectileDirection, Vector2 inheritedVelocity = default(Vector2))
        {
            position = pos;
            direction = projectileDirection;
            this.inheritedVelocity = inheritedVelocity;
            sprite = game.playerBullet;
            speed = defaultSpeed;
        }

        public Projectile(MainGame game, Vector2 pos, Vector2 projectileDirection, Texture2D Sprite, int projectileSpeed, Vector2 inheritedVelocity = default(Vector2))
        {
            position = pos;
            direction = projectileDirection;
            this.inheritedVelocity = inheritedVelocity;
            sprite = Sprite;
            speed = projectileSpeed;

        }

        public Projectile(MainGame game, Vector2 pos, Vector2 projectileDirection, Texture2D Sprite, int projectileSpeed, bool isInvincible, Vector2 inheritedVelocity = default(Vector2))
        {
            position = pos;
            direction = projectileDirection;
            this.inheritedVelocity = inheritedVelocity;
            sprite = Sprite;
            speed = projectileSpeed;
            invincible = isInvincible;

        }

		public Rectangle rectangle { get { return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height); } }

        public Vector2 CenterPosition
        {
            get { return new Vector2(position.X + sprite.Width / 2, position.Y + sprite.Height / 2); }
            //set
            //{
            //    if (sprite != null)
            //    {
            //        position.X = value.X - sprite.Width / 2.0f;
            //        position.Y = value.Y - sprite.Height / 2.0f;
            //    }
            //}
        }

    }
}
