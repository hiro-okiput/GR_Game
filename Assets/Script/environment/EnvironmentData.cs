using GR_Game.Struct;
using GR_Game.Enum;

namespace GR_Game
{
    static public class EnvironmentData
    {
        static private float gravity;           //重力
        static private TimeData currentTime;    //月日日時
        static private float windSpeed;         //風速
        static private float windSpeedMin;      //風速_最低
        static private float windSpeedMax;      //風速_最高
        static private Direction windDirection; //風向
        static private float temperature;       //気温
        static private float temperatureMin;    //気温_最低
        static private float temperatureMax;    //気温_最高
        static private Weather weather;         //天気

        //重力を設定
        static public void SetGravity(float num) => gravity = num;
        //月日日時を設定
        static public void SetTime(TimeData data) => currentTime = data;
        //月を設定
        static public void SetMonth(int num) => currentTime.month = num;
        //日を設定
        static public void SetDay(int num) => currentTime.day = num;
        //時を設定
        static public void SetHour(int num) => currentTime.hour = num;
        //分を設定
        static public void SetMinute(int num) => currentTime.minute = num;
        //秒を設定
        static public void SetSecond(int num) => currentTime.second = num;
        //風速を設定
        static public void SetWindSpeed(float num) => windSpeed = num;
        //風速_最低を設定
        static public void SetWindSpeedMin(float num) => windSpeedMin = num;
        //風速_最高を設定
        static public void SetWindSpeedMax(float num) => windSpeedMax = num;
        //風向を設定
        static public void SetWindDirection(Direction dir) => windDirection = dir;
        //気温を設定
        static public void SetTemperature(float num) => temperature = num;
        //気温_最低を設定
        static public void SetTemperatureMin(float num) => temperatureMin = num;
        //気温_最高を設定
        static public void SetTemperatureMax(float num) => temperatureMax = num;
        //天気を設定
        static public void SetWeather(Weather wea) => weather = wea;

        //重力を取得
        static public float GetGravity() => gravity;
        //月日日時を取得
        static public TimeData GetTime() => currentTime;
        //月を取得
        static public int GetMonth() => currentTime.month;
        //日を取得
        static public int GetDay() => currentTime.day;
        //時を取得
        static public int GetHour() => currentTime.hour;
        //分を取得
        static public int GetMinute() => currentTime.minute;
        //秒を取得
        static public int GetSecond() => currentTime.second;
        //風速を取得
        static public float GetWindSpeed() => windSpeed;
        //風速_最低を取得
        static public float GetWindSpeedMin() => windSpeedMin;
        //風速_最高を取得
        static public float GetWindSpeedMax() => windSpeedMax;
        //風向を取得
        static public Direction GetWindDirection() => windDirection;
        //気温を取得
        static public float GetTemperature() => temperature;
        //気温_最低を取得
        static public float GetTemperatureMin() => temperatureMin;
        //気温_最高を取得
        static public float GetTemperatureMax() => temperatureMax;
        //天気を取得
        static public Weather GetWeather() => weather;

        //地球と同じに初期化
        static public void InitializeToEarth()
        {
            SetGravity(1);
        }

    }
}
