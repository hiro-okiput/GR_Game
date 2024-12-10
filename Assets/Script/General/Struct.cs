using GR_Game.Enum;
using UnityEngine;

namespace GR_Game.Struct
{
    public struct TimeData
    {
        public int month;   //Œ
        public int day;     //“ú
        public int hour;    //
        public int minute;  //•ª
        public int second;  //•b

        //‰Šú‰»ˆ—
        public void Initialize()
        {
            month = 1;
            day = 1;
            hour = 0;
            minute = 0;
            second = 0;
        }
    }

    public struct BoundsData
    {
        public float[] max;
        public float[] min;
        public float[] center;

        public void Initialize()
        {
            max = new float[3];
            min = new float[3];
            center = new float[3];
        }

        public void SetValue(Axes axis, Vector3 boundsMax, Vector3 boundsMin, Vector3 boundsCenter)
        {
            switch(axis)
            {
                case Axes.X:
                    max[0] = boundsMax.x;
                    max[1] = boundsMax.y;
                    max[2] = boundsMax.z;
                    min[0] = boundsMin.x;
                    min[1] = boundsMin.y;
                    min[2] = boundsMin.z;
                    center[0] = boundsCenter.x;
                    center[1] = boundsCenter.y;
                    center[2] = boundsCenter.z;
                    break;
                case Axes.Y:
                    max[0] = boundsMax.y;
                    max[1] = boundsMax.x;
                    max[2] = boundsMax.z;
                    min[0] = boundsMin.y;
                    min[1] = boundsMin.x;
                    min[2] = boundsMin.z;
                    center[0] = boundsCenter.y;
                    center[1] = boundsCenter.x;
                    center[2] = boundsCenter.z;
                    break;
                case Axes.Z:
                    max[0] = boundsMax.z;
                    max[1] = boundsMax.x;
                    max[2] = boundsMax.y;
                    min[0] = boundsMin.z;
                    min[1] = boundsMin.x;
                    min[2] = boundsMin.y;
                    center[0] = boundsCenter.z;
                    center[1] = boundsCenter.x;
                    center[2] = boundsCenter.y;
                    break;
            }
        }
    }
}
