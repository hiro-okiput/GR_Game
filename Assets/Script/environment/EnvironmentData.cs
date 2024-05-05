using GR_Game.Struct;

namespace GR_Game
{
    static public class EnvironmentData
    {
        static private float gravity;           //�d��
        static private TimeData currentTime;    //��������
        static private float windSpeed;         //����
        static private int windDirection;       //����
        static private float temperature;       //�C��

        //�d�͂�ݒ�
        static public void SetGravity(float num) => gravity = num;
        //����������ݒ�
        static public void SetTime(TimeData data) => currentTime = data;
        //����ݒ�
        static public void SetMonth(int num) => currentTime.month = num;
        //����ݒ�
        static public void SetDay(int num) => currentTime.day = num;
        //����ݒ�
        static public void SetHour(int num) => currentTime.hour = num;
        //����ݒ�
        static public void SetMinute(int num) => currentTime.minute = num;
        //�b��ݒ�
        static public void SetSecond(int num) => currentTime.second = num;
        //������ݒ�
        static public void SetWindSpeed(float num) => windSpeed = num;
        //������ݒ�
        static public void SetWindDirection(int num) => windDirection = num;
        //�C����ݒ�
        static public void SetTemperature(float num) => temperature = num;

        //�d�͂��擾
        static public float GetGravity() => gravity;
        //�����������擾
        static public TimeData GetTime() => currentTime;
        //�����擾
        static public int GetMonth() => currentTime.month;
        //�����擾
        static public int GetDay() => currentTime.day;
        //�����擾
        static public int GetHour() => currentTime.hour;
        //�����擾
        static public int GetMinute() => currentTime.minute;
        //�b���擾
        static public int GetSecond() => currentTime.second;
        //�������擾
        static public float GetWindSpeed() => windSpeed;
        //�������擾
        static public int GetWindDirection() => windDirection;
        //�C�����擾
        static public float GetTemperature() => temperature;

        //�n���Ɠ����ɏ�����
        static public void InitializeToEarth()
        {
            SetGravity(1);
        }

    }
}
