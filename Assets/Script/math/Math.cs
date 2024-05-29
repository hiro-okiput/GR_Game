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

        public static Vector3 Vector3Dif(Vector3 vec1, Vector3 vec2)
        {
            Vector3 resultVec = Vector3.zero;
            resultVec.x = vec1.x - vec2.x;
            resultVec.y = vec1.y - vec2.y;
            resultVec.z = vec1.z - vec2.z;
            return resultVec;
        }

        public static Vector3 Vector3Sum(Vector3 vec1, Vector3 vec2)
        {
            Vector3 resultVec = Vector3.zero;
            resultVec.x = vec1.x + vec2.x;
            resultVec.y = vec1.y + vec2.y;
            resultVec.z = vec1.z + vec2.z;
            return resultVec;
        }

        public static float Vector3Dot(Vector3 vec1, Vector3 vec2)
        {
            float result;

            result = vec1.x * vec2.x + vec1.y * vec2.y + vec1.z * vec2.z;

            return result;
        }

        public static Vector3 Vector3Cross(Vector3 vec1, Vector3 vec2)
        {
            Vector3 resultVec = Vector3.zero;

            resultVec.x = vec1.y * vec2.z - vec1.z * vec2.y;
            resultVec.y = vec1.z * vec2.x - vec1.x * vec2.z;
            resultVec.z = vec1.x * vec2.y - vec1.y * vec2.x;

            return resultVec;
        }
    }

    public class GR_GamePysics
    {
        public static void Crush()
        {

        }
    }
}
