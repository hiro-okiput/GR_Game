using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace GR_Game.Math
{
    public class GR_GameMath
    {
        public static float pi = 3.14f;

        public static float Sqrt(double num)
        {
            double result = num;
            for (int i = 0; i < 100; i++)
            {
                result = (result * result + num) / (2 * result);
            }
            return (float)result;
        }

        public static float DegreeToRadian(double degree) => (float)degree * (pi / 180.0f);

        public static float RadianToDegree(double radian) => (float)radian * ( 180.0f / pi);

        public static float CompleteEllipticIntegralOfTheFirstKind(double k, double correction)
        {
            double a = 1.0;
            double b = Sqrt( 1 - k * k );
            double sum = 1.0;

            while(a - b > 1e-15)
            {
                double aNext = (a + b) / 2;
                double bNext = Sqrt(a * b);
                double amountOfChange = a - aNext;

                sum -= 2 * correction * amountOfChange * amountOfChange;

                correction *= 2;

                a = aNext;
                b = bNext;
            }
            double result = pi / (2 * a) * sum;

            return (float)result;
        }

        public static Vector2 Vector2Dif(Vector2 vec1, Vector2 vec2)
        {
            Vector2 resultVec = Vector2.zero;
            return resultVec;
        }

    }

    public class GR_GamePysics
    {
        public  Crush
    }
}
