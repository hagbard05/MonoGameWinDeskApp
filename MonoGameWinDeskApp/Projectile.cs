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
    /// <summary>
    /// projectile class for projectile objects in the game
    /// </summary>
    public class Projectile
    {

        public Vector2 position;
        public Vector2 direction; 
        public Texture2D sprite;
        public Color[] spriteTextureData;
        public int speed;
        public const int defaultSpeed = 23;
        public bool invincible = false;

        public Projectile(MainGame game, Vector2 pos, Vector2 projectileDirection)
        {
            position = pos;
            direction = projectileDirection;
            sprite = game.Content.Load<Texture2D>("smileSmall");
            speed = defaultSpeed;

            // Extract collision data for per pixle collisions
            spriteTextureData =
                new Color[sprite.Width * sprite.Height];
            sprite.GetData(spriteTextureData);
        }

        public Projectile(MainGame game, Vector2 pos, Vector2 projectileDirection, Texture2D Sprite)
        {
            position = pos;
            direction = projectileDirection;
            sprite = Sprite;
            speed = defaultSpeed;

            // Extract collision data for per pixle collisions
            spriteTextureData =
                new Color[sprite.Width * sprite.Height];
            sprite.GetData(spriteTextureData);
        }

        public Projectile(MainGame game, Vector2 pos, Vector2 projectileDirection, Texture2D Sprite, int projectileSpeed)
        {
            position = pos;
            direction = projectileDirection;
            sprite = Sprite;
            speed = projectileSpeed;

            // Extract collision data for per pixle collisions
            spriteTextureData =
                new Color[sprite.Width * sprite.Height];
            sprite.GetData(spriteTextureData);
        }

        public Projectile(MainGame game, Vector2 pos, Vector2 projectileDirection, Texture2D Sprite, int projectileSpeed, bool isInvincible)
        {
            position = pos;
            direction = projectileDirection;
            sprite = Sprite;
            speed = projectileSpeed;
            invincible = isInvincible;

            // Extract collision data for per pixle collisions
            spriteTextureData =
                new Color[sprite.Width * sprite.Height];
            sprite.GetData(spriteTextureData);
        }

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            }
        }
        public Vector2 centerPosition
        {
            get { return new Vector2(position.X + sprite.Width / 2, position.Y + sprite.Height / 2); }
        }
    }
}
