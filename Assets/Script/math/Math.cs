using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GR_Game.Math
{
    public class Math
    {
        public static float Sqrt(double num)
        {
            double result = num;
            for (int i = 0; i < 100; i++)
            {
                result = (result * result + num) / (2 * result);
            }
            return (float)result;
        }
    }
}
