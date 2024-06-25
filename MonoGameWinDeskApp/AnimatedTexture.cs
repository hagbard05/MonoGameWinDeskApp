using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AnimatedSprite
{
    public class AnimatedTexture
    {
        private int frameCount;
        private Texture2D myTexture;
        private float TimePerFrame;
        public int Frame;
        private float TotalElapsed;
        private bool Paused;
		protected int _FrameWidth;

		public int FrameWidth 
		{
			get { _FrameWidth = myTexture.Width / frameCount; return _FrameWidth; }
			set { _FrameWidth = value; }
		}
		public int FrameHeight { get { return myTexture.Height; } }

        public float rotation, scale, depth;
        public Vector2 position;


        public AnimatedTexture(Vector2 Position, float Rotation, float Scale, float Depth)
        {
            this.position = Position;
            this.rotation = Rotation;
            this.scale = Scale;
            this.depth = Depth;
        }

        public AnimatedTexture(Vector2 Position)
        {
            this.position = Position;
            this.rotation = 0;
            this.scale = 1f;
            this.depth = 0.5f;
        }

        public AnimatedTexture(Vector2 Position, ContentManager content, string asset, int FrameCount, int framesPerSec)
        {
            this.position = Position;
            this.rotation = 0;
            this.scale = 1f;
            this.depth = 0.5f;

            frameCount = FrameCount;
            myTexture = content.Load<Texture2D>(asset);
            TimePerFrame = (float)1 / framesPerSec;
            Frame = 0;
            TotalElapsed = 0;
            Paused = false;
        }

        public void Load(ContentManager content, string asset, int FrameCount, int framesPerSec)
        {
            frameCount = FrameCount;
            myTexture = content.Load<Texture2D>(asset);
            TimePerFrame = (float)1 / framesPerSec;
            Frame = 0;
            TotalElapsed = 0;
            Paused = false;
        }

        // class AnimatedTexture
        public void UpdateFrame(float elapsed)
        {
            if (Paused)
                return;
            TotalElapsed += elapsed;
            if (TotalElapsed > TimePerFrame)
            {
                Frame++;
                // Keep the Frame between 0 and the total frames, minus one.
                Frame = Frame % frameCount;
                TotalElapsed -= TimePerFrame;
            }
        }

        // class AnimatedTexture
        public void DrawFrame(SpriteBatch batch, Vector2 screenPos)
        {
            DrawFrame(batch, Frame, screenPos);
        }
        public void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos)
        {
            FrameWidth = myTexture.Width / frameCount;
            Rectangle sourcerect = new Rectangle(FrameWidth * frame, 0,
                FrameWidth, myTexture.Height);
            batch.Draw(myTexture, screenPos, sourcerect, Color.White,
                rotation, position, scale, SpriteEffects.None, depth);
        }

        public bool IsPaused
        {
            get { return Paused; }
        }
        public void Reset()
        {
            Frame = 0;
            TotalElapsed = 0f;
        }
        public void Stop()
        {
            Pause();
            Reset();
        }
        public void Play()
        {
            Paused = false;
        }
        public void Pause()
        {
            Paused = true;
        }

    }
}
