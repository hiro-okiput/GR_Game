using GR_Game.Struct;
using GR_Game.Enum;

namespace GR_Game
{
    static public class EnvironmentData
    {
        static private float gravity;           //�d��
        static private TimeData currentTime;    //��������
        static private float windSpeed;         //����
        static private float windSpeedMin;      //����_�Œ�
        static private float windSpeedMax;      //����_�ō�
        static private Direction windDirection; //����
        static private float temperature;       //�C��
        static private float temperatureMin;    //�C��_�Œ�
        static private float temperatureMax;    //�C��_�ō�
        static private Weather weather;         //�V�C

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
        //����_�Œ��ݒ�
        static public void SetWindSpeedMin(float num) => windSpeedMin = num;
        //����_�ō���ݒ�
        static public void SetWindSpeedMax(float num) => windSpeedMax = num;
        //������ݒ�
        static public void SetWindDirection(Direction dir) => windDirection = dir;
        //�C����ݒ�
        static public void SetTemperature(float num) => temperature = num;
        //�C��_�Œ��ݒ�
        static public void SetTemperatureMin(float num) => temperatureMin = num;
        //�C��_�ō���ݒ�
        static public void SetTemperatureMax(float num) => temperatureMax = num;
        //�V�C��ݒ�
        static public void SetWeather(Weather wea) => weather = wea;

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
        //����_�Œ���擾
        static public float GetWindSpeedMin() => windSpeedMin;
        //����_�ō����擾
        static public float GetWindSpeedMax() => windSpeedMax;
        //�������擾
        static public Direction GetWindDirection() => windDirection;
        //�C�����擾
        static public float GetTemperature() => temperature;
        //�C��_�Œ���擾
        static public float GetTemperatureMin() => temperatureMin;
        //�C��_�ō����擾
        static public float GetTemperatureMax() => temperatureMax;
        //�V�C���擾
        static public Weather GetWeather() => weather;

        //�n���Ɠ����ɏ�����
        static public void InitializeToEarth()
        {
            SetGravity(1);
        }

    }
}
