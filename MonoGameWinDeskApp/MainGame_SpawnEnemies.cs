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
    public partial class MainGame : Microsoft.Xna.Framework.Game
    {
		int numKamToSpawn = 6;
		int numWobbleToSpawn = 6;
		int numToSpawnCryGrb = 9;
		float kamikazieSpeedAdd = 2.3f;
		int kSpeedRange = 5;
		float wobbleKSpeedAdd = 1.9f;
		int wobbleSpeedRange = 4;
		float crystalGrabberSpeedAdd = 1.2f;
		int cryGrbSpeedRange = 3;

        public int levelToAddWobbleKama = 5;
        public int levelToAddKama = 3;

		/// <summary>Handles spawning new wobbleKamikazes </summary>
		void spawnWobbleKamikazes(int frequency)
		{
			Random random = new Random(randomSeed3 + DateTime.Now.Millisecond % Int32.MaxValue);
            if (random.Next(10000 / numPlayersStarted) < frequency)
            {
                // above
                for (int i = 0; i < random.Next(numWobbleToSpawn); i++)
                {
                    Vector2 position = new Vector2(random.Next(viewport.Width), -123);
                    WobbleKamikaze wk = new WobbleKamikaze(this, position,
                        new AnimatedTexture(new Vector2(0), Content, explosionStripForWobble, 12, 12),
                        new AnimatedTexture(new Vector2(0), Content, wobbleKSheet, 12, 12));
                    wk.speed += ((float)random.Next(wobbleSpeedRange) * wobbleKSpeedAdd) + ((float)diffuculityAdder / 9);
                    wobbleKamikazes.Add(wk);
                }
            }
            if (random.Next(10000 / numPlayersStarted) < frequency)
			{
				// below
				for (int i = 0; i < random.Next(numWobbleToSpawn); i++)
				{
					Vector2 position = new Vector2(random.Next(viewport.Width), viewport.Height + 123);
					WobbleKamikaze wk = new WobbleKamikaze(this, position,
                        new AnimatedTexture(new Vector2(0), Content, explosionStripForWobble, 12, 12),
						new AnimatedTexture(new Vector2(0), Content, wobbleKSheet, 12, 12));
					wk.speed += ((float)random.Next(wobbleSpeedRange) * wobbleKSpeedAdd) + ((float)diffuculityAdder / 7);
					wobbleKamikazes.Add(wk);
				}
            }
            if (random.Next(10000 / numPlayersStarted) < frequency)
            {
				// right
				for (int i = 0; i < random.Next(numWobbleToSpawn); i++)
				{
					Vector2 position = new Vector2(random.Next(viewport.Height), viewport.Width + 123);
					WobbleKamikaze wk = new WobbleKamikaze(this, position,
                        new AnimatedTexture(new Vector2(0), Content, explosionStripForWobble, 12, 12),
						new AnimatedTexture(new Vector2(0), Content, wobbleKSheet, 12, 12));
					wk.speed += ((float)random.Next(wobbleSpeedRange) * wobbleKSpeedAdd) + ((float)diffuculityAdder / 10);
					wobbleKamikazes.Add(wk);
				}
            }
            if (random.Next(10000 / numPlayersStarted) < frequency)
            {
				// left 
				for (int i = 0; i < random.Next(numWobbleToSpawn); i++)
				{
					Vector2 position = new Vector2(random.Next(viewport.Height), -123);

					WobbleKamikaze wk = new WobbleKamikaze(this, position,
                        new AnimatedTexture(new Vector2(0), Content, explosionStripForWobble, 12, 12),
						new AnimatedTexture(new Vector2(0), Content, wobbleKSheet, 12, 12));
					wk.speed += ((float)random.Next(wobbleSpeedRange) * wobbleKSpeedAdd) + ((float)diffuculityAdder / 9);
					wobbleKamikazes.Add(wk);
				}
			}
		}

		/// <summary>Handles spawning new kamikazes above the viewable area</summary>
		void spawnKamikazesAboveScreen(int frequency)
		{
			Random random = new Random(randomSeed + 1);
			if (random.Next(10000 / numPlayersStarted) < frequency)
			{
				for (int i = 0; i < random.Next(numKamToSpawn); i++)
				{
					Kamikaze k = new Kamikaze(this, new Vector2(random.Next(viewport.Width), -240), new AnimatedTexture(new Vector2(0), Content, explosionStripForGhost, 12, 12), new AnimatedTexture(new Vector2(0), Content, KamakisaiSheetSmall, 4, 12));
					k.speed += ((float)random.Next(kSpeedRange) * kamikazieSpeedAdd) + ((float)diffuculityAdder / 9);
					kamikazes.Add(k);
				}
			}
		}

		/// <summary>Handles spawning new kamikazes below the viewable area</summary>
		void spawnKamikazesBelowScreen(int frequency)
		{
			Random random = new Random(randomSeed + 2);
			if (random.Next(10000 / numPlayersStarted) < frequency)
			{
				for (int i = 0; i < random.Next(numKamToSpawn); i++)
				{
					Kamikaze k = new Kamikaze(this, new Vector2(random.Next(viewport.Width), viewport.Height + 200), new AnimatedTexture(new Vector2(0), Content, explosionStripForGhost, 12, 12), new AnimatedTexture(new Vector2(0), Content, KamakisaiSheetSmall, 4, 12));
					k.speed += ((float)random.Next(kSpeedRange) * kamikazieSpeedAdd) + ((float)diffuculityAdder / 10);
					kamikazes.Add(k);
				}
			}
		}

		/// <summary>Handles spawning new kamikazes left of the viewable area</summary>
		void spawnKamikazesLeftOfScreen(int frequency)
		{
			Random random = new Random(randomSeed + 3);
			if (random.Next(10000 / numPlayersStarted) < frequency)
			{
				for (int i = 0; i < random.Next(numKamToSpawn); i++)
				{
					Kamikaze k = new Kamikaze(this, new Vector2(-200, random.Next(viewport.Height)), new AnimatedTexture(new Vector2(0), Content, explosionStripForGhost, 12, 12), new AnimatedTexture(new Vector2(0), Content, KamakisaiSheetSmall, 4, 12));
					k.speed += ((float)random.Next(kSpeedRange) * kamikazieSpeedAdd) + ((float)diffuculityAdder / 7);
					kamikazes.Add(k);
				}
			}
		}

		/// <summary>Handles spawning new kamikazes right of the viewable area</summary>
		void spawnKamikazesRightOfScreen(int frequency)
		{
			Random random = new Random(randomSeed + 4);
			if (random.Next(10000 / numPlayersStarted) < frequency)
			{
				for (int i = 0; i < random.Next(numKamToSpawn); i++)
				{
					Kamikaze k = new Kamikaze(this, new Vector2(viewport.Width + 200, random.Next(viewport.Height)), new AnimatedTexture(new Vector2(0), Content, explosionStripForGhost, 12, 12), new AnimatedTexture(new Vector2(0), Content, KamakisaiSheetSmall, 4, 12));
					k.speed += ((float)random.Next(kSpeedRange) * kamikazieSpeedAdd) + ((float)diffuculityAdder / 9);
					kamikazes.Add(k);
				}
			}
		}

		/// <summary>Handles spawning new crystalGrabbewrs  above the viewable area</summary>
		void spawnCrystalGrabbersAboveScreen(int frequency)
		{
			if (powerCrystal.position.Y > 0) // if the top of the crystal has moved above the top of the screen then we will not spawn crystal grabbers that can grab it without being seen 
			{
				Random random = new Random(randomSeed);
				Random random2 = new Random(randomSeed2);
				if (random.Next(30000 / numPlayersStarted) < frequency)
				{
					for (int i = 0; i < random.Next(numToSpawnCryGrb); i++)
					{
						CrystalGrabber c = new CrystalGrabber(this, new Vector2(random.Next(viewport.Width), -200), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12));
						c.speed += ((float)random.Next(3) * crystalGrabberSpeedAdd) + ((float)diffuculityAdder / 10);
						crystalGrabbers.Add(c);
						CrystalGrabber c2 = new CrystalGrabber(this, new Vector2(random.Next(viewport.Width), -200), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation, 9, 12));
						c2.speed += ((float)random2.Next(cryGrbSpeedRange) * crystalGrabberSpeedAdd) + ((float)diffuculityAdder / 12);
						crystalGrabbers.Add(c2);
					}
				}
			}
		}

		/// <summary>Handles spawning new crystalGrabbewrs below the viewable area </summary>
		void spawnCrystalGrabbersBelowScreen(int frequency)
		{
			Random random = new Random(randomSeed);
			Random random2 = new Random(randomSeed2);
			if (random.Next(5000 / numPlayersStarted) < frequency)
			{
				for (int i = 0; i < random.Next(numToSpawnCryGrb); i++)
				{
					CrystalGrabber c = new CrystalGrabber(this, new Vector2(random.Next(viewport.Width), viewport.Height + 200), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12));
					c.speed += ((float)random.Next(5) * crystalGrabberSpeedAdd) + ((float)diffuculityAdder / 10);
					crystalGrabbers.Add(c);
					CrystalGrabber c2 = new CrystalGrabber(this, new Vector2(random.Next(viewport.Width), viewport.Height + 200), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation, 9, 12));
					c2.speed += ((float)random2.Next(cryGrbSpeedRange) * crystalGrabberSpeedAdd) + ((float)diffuculityAdder / 8);
					crystalGrabbers.Add(c2);
				}

			}
		}

		/// <summary>Handles spawning new crystalGrabbewrs right of the viewable area</summary>
		void spawnCrystalGrabbersRightOfScreen(int frequency)
		{
			Random random = new Random(randomSeed);
			Random random2 = new Random(randomSeed);
			if (random.Next(8000 / numPlayersStarted) < frequency)
			{
				for (int i = 0; i < random.Next(numToSpawnCryGrb); i++)
				{
					CrystalGrabber c = new CrystalGrabber(this, new Vector2(viewport.Width + 200, random.Next(viewport.Height)), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12));
					c.speed += ((float)random.Next(cryGrbSpeedRange) * crystalGrabberSpeedAdd) + ((float)diffuculityAdder / 12);
					crystalGrabbers.Add(c);
					CrystalGrabber c2 = new CrystalGrabber(this, new Vector2(viewport.Width + 200, random2.Next(viewport.Height)), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation, 9, 12));
					c2.speed += ((float)random2.Next(cryGrbSpeedRange) * crystalGrabberSpeedAdd) + ((float)diffuculityAdder / 8);
					crystalGrabbers.Add(c2);
				}

			}
		}

		/// <summary>Handles spawning new crystalGrabbewrs left of the viewable area</summary>
		void spawnCrystalGrabbersLeftOfScreen(int frequency)
		{
			Random random = new Random(randomSeed);
			Random random2 = new Random(randomSeed);
			if (random.Next(8000 / numPlayersStarted) < frequency)
			{
				for (int i = 0; i < random.Next(numToSpawnCryGrb); i++)
				{
					CrystalGrabber c = new CrystalGrabber(this, new Vector2(random.Next(viewport.Width), -200), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12));
					c.speed += ((float)random.Next(cryGrbSpeedRange) * crystalGrabberSpeedAdd) + ((float)diffuculityAdder / 12);
					crystalGrabbers.Add(c);
					CrystalGrabber c2 = new CrystalGrabber(this, new Vector2(random.Next(viewport.Width), -200), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation, 9, 12));
					c2.speed += ((float)random2.Next(cryGrbSpeedRange) * crystalGrabberSpeedAdd) + ((float)diffuculityAdder / 8);
					crystalGrabbers.Add(c2);
				}
			}
		}

        public void spawn10CrystalGrabbersBelow()
        {
            crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(0, viewport.Height + OctoEnemyAnamation2.Height), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12), (float)new Random(randomSeed).Next(5) * crystalGrabberSpeedAdd + 1.0f));
            crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(viewport.Width / 10, viewport.Height + OctoEnemyAnamation2.Height), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12), (float)new Random(randomSeed).Next(5) * crystalGrabberSpeedAdd + 1.0f));
            crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(viewport.Width / 10 * 2, viewport.Height + OctoEnemyAnamation2.Height), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12), (float)new Random(randomSeed).Next(5) * crystalGrabberSpeedAdd + 1.0f));
            crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(viewport.Width / 10 * 3, viewport.Height + OctoEnemyAnamation2.Height), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12), (float)new Random(randomSeed).Next(5) * crystalGrabberSpeedAdd + 1.0f));
            crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(viewport.Width / 10 * 4, viewport.Height + OctoEnemyAnamation2.Height), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12), (float)new Random(randomSeed).Next(5) * crystalGrabberSpeedAdd + 1.0f));
            crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(viewport.Width / 10 * 5, viewport.Height + OctoEnemyAnamation2.Height), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12), (float)new Random(randomSeed).Next(5) * crystalGrabberSpeedAdd + 1.0f));
            crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(viewport.Width / 10 * 6, viewport.Height + OctoEnemyAnamation2.Height), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12), (float)new Random(randomSeed).Next(5) * crystalGrabberSpeedAdd + 1.0f));
            crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(viewport.Width / 10 * 7, viewport.Height + OctoEnemyAnamation2.Height), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12), (float)new Random(randomSeed).Next(5) * crystalGrabberSpeedAdd + 1.0f));
            crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(viewport.Width / 10 * 8, viewport.Height + OctoEnemyAnamation2.Height), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12), (float)new Random(randomSeed).Next(5) * crystalGrabberSpeedAdd + 1.0f));
            crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(viewport.Width / 10 * 9, viewport.Height + OctoEnemyAnamation2.Height), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12), (float)new Random(randomSeed).Next(5) * crystalGrabberSpeedAdd + 1.0f));
            crystalGrabbers.Add(new CrystalGrabber(this, new Vector2(viewport.Width, viewport.Height + OctoEnemyAnamation2.Height), new AnimatedTexture(new Vector2(0), Content, explosionStrip, 7, 12), new AnimatedTexture(new Vector2(0), Content, OctoEnemyAnamation2, 14, 12), (float)new Random(randomSeed).Next(5) * crystalGrabberSpeedAdd + 1.0f));
        }
    } // End partial class

} // end namespace
