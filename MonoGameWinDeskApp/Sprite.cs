using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// Local Projects
using AnimatedSprite;


namespace MyFirstGame
{
    
	public class Sprite
    {
        public Vector2 position;
        public enum powerUpTypes { Pepper, InvincibleBullet, SuperBomb, Shield, Speed, ExtraLife, none };
        public powerUpTypes powerUpType;
        public Texture2D sprite;
        public bool bulletCollision;
        public bool playerCollision;

		private Color[] _spriteTextureData = null;
		public Color[] spriteTextureData
		{
			get
			{
				if (_spriteTextureData == null)
				{
					if (sprite != null)
					{
#if DEBUG
					System.Diagnostics.Debug.WriteLine(" Creating sprite texture data... ");
#endif
						// Extract collision data for per pixle collisions
						_spriteTextureData = new Color[sprite.Width * sprite.Height];
						sprite.GetData(_spriteTextureData);
					}
				}
				return _spriteTextureData;
			}
		}

		public virtual Rectangle rectangle { get { return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height); } }
		
        public virtual Vector2 CenterPosition 
        { 
            get { return new Vector2(position.X + (float)sprite.Width / 2.0f, position.Y + (float)sprite.Height / 2.0f); }
            set
            {
				if (sprite != null)
				{
					position.X = value.X - (float)sprite.Width / 2.0f;
					position.Y = value.Y - (float)sprite.Height / 2.0f;
#if DEBUG
					System.Diagnostics.Debug.WriteLine("Position set to: (" + position.X + ", " + position.Y + ") using center position set{} (" + value.X + ", " + value.Y +")");
#endif
				}
				else
				{
#if DEBUG
					System.Diagnostics.Debug.WriteLine("Texture is null so CenterPosition was set using X, Y - 10 values");
#endif
					position.X = value.X - 15;
					position.Y = value.Y - 15;
				}
            }
        }

	} // end class

}// end namespace
