
namespace GR_Game.Struct
{
    public struct TimeData
    {
        public int month;   //��
        public int day;     //��
        public int hour;    //��
        public int minute;  //��
        public int second;  //�b

        //����������
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
