using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyFirstGame
{
    public class VectorOps
    {
        /// <summary>
        /// Returns a new vector rotated in rad passed in
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="rotateInRad"></param>
        /// <returns></returns>
        public static Vector2 RotateVector(Vector2 vector, double rotateInRad)
        {
            Vector2 rotatedVec;
            // Get the angles redians for the vector
            double radOfVec = System.Math.Atan2(vector.Y, vector.X);
            // get the new angles rads
            double newRad = radOfVec + rotateInRad;
            // create a new Vector2 by using sin and cos to find the X and Y
            rotatedVec = new Vector2((float)System.Math.Cos(newRad) * vector.Length(), (float)System.Math.Sin(newRad) * vector.Length());
            return rotatedVec;
        }
    }
}
