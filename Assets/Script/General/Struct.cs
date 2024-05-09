
namespace GR_Game.Struct
{
    public struct TimeData
    {
        public int month;   //月
        public int day;     //日
        public int hour;    //時
        public int minute;  //分
        public int second;  //秒

        //初期化処理
        public void Initialize()
        {
            month = 1;
            day = 1;
            hour = 0;
            minute = 0;
            second = 0;
        }
    }
}
