using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using GR_Game;
using GR_Game.Enum;
using Random = UnityEngine.Random;
using Unity.Mathematics;

public class DinamicWeatherSystem : MonoBehaviour
{
    [SerializeField, Header("何秒に一度処理させるか")]
    private float dWSStartWaitTime;

    [SerializeField, Header("気温設定時に何度までのずれを許容するか")]
    private float temperatureShift;

    [SerializeField, Header("風速の下限")]
    private float windSpeedMinValue;
    [SerializeField, Header("風速の上限")]
    private float windSpeedMaxValue;

    private float nowWaitTime = 0;              //現在の待機時間

    private float timeUpdateWait = 0;           //1秒計る用

    private int currentDay = int.MaxValue;      //今のゲーム内日にち

    private int currentHour = int.MaxValue;     //今のゲーム内時

    private void Start()
    {
        EnvironmentData.SetWindSpeedMin(windSpeedMinValue);
        EnvironmentData.SetWindSpeedMax(windSpeedMaxValue);
        nowWaitTime = dWSStartWaitTime;     //実行開始時に天候処理を走らせる用
    }

    private void Update()
    {
        nowWaitTime += Time.deltaTime;
        timeUpdateWait += Time.deltaTime;
        if (timeUpdateWait > 1.0f) TimeUpdate();
        if (nowWaitTime >= dWSStartWaitTime) StartDinamicWeatherSystem();
    }

    //天候設定処理
    public async Task StartDinamicWeatherSystem()
    {
        if (EnvironmentData.GetDay() != currentDay)
        {
            currentDay = EnvironmentData.GetDay();
            currentHour = 0;
            await GenerateTemperatureMinMax(EnvironmentData.GetMonth());
        }

        EnvironmentData.SetWeather((Weather)Random.Range(0, (int)Weather.Max));

        windSpeedMinValue = EnvironmentData.GetWindSpeedMin();
        windSpeedMaxValue = EnvironmentData.GetWindSpeedMax();
        EnvironmentData.SetWindSpeed(Random.Range(windSpeedMinValue, windSpeedMaxValue));

        //風向の設定用にインデックス番号を用意
        int enumIndex = Random.Range(0, Enum.GetValues(typeof(Direction)).Length);
        await GenerateWindDirection(enumIndex);

        if (currentHour != EnvironmentData.GetHour())
            await GenerateNowTemperature();

        nowWaitTime = 0;
    }
    
    //気温の最低最高値設定(大阪基準)
    private async Task GenerateTemperatureMinMax(int month)
    {
        switch(month)
        {
            case 1:
                EnvironmentData.SetTemperatureMin(3.0f);
                EnvironmentData.SetTemperatureMax(9.5f);
                break;
            case 2:
                EnvironmentData.SetTemperatureMin(3.5f);
                EnvironmentData.SetTemperatureMax(11.5f);
                break;
            case 3:
                EnvironmentData.SetTemperatureMin(7.5f);
                EnvironmentData.SetTemperatureMax(15f);
                break;
            case 4:
                EnvironmentData.SetTemperatureMin(11.0f);
                EnvironmentData.SetTemperatureMax(20.5f);
                break;
            case 5:
                EnvironmentData.SetTemperatureMin(15.5f);
                EnvironmentData.SetTemperatureMax(24f);
                break;
            case 6:
                EnvironmentData.SetTemperatureMin(20.0f);
                EnvironmentData.SetTemperatureMax(27.5f);
                break;
            case 7:
                EnvironmentData.SetTemperatureMin(25.0f);
                EnvironmentData.SetTemperatureMax(32.5f);
                break;
            case 8:
                EnvironmentData.SetTemperatureMin(26.0f);
                EnvironmentData.SetTemperatureMax(33.5f);
                break;
            case 9:
                EnvironmentData.SetTemperatureMin(23.0f);
                EnvironmentData.SetTemperatureMax(30.0f);
                break;
            case 10:
                EnvironmentData.SetTemperatureMin(15.5f);
                EnvironmentData.SetTemperatureMax(24.0f);
                break;
            case 11:
                EnvironmentData.SetTemperatureMin(10.5f);
                EnvironmentData.SetTemperatureMax(18.5f);
                break;
            case 12:
                EnvironmentData.SetTemperatureMin(5.0f);
                EnvironmentData.SetTemperatureMax(12.5f);
                break;
        }

        float temparetureMinValue = EnvironmentData.GetTemperatureMin();
        float temperatureDifference = (temparetureMinValue - EnvironmentData.GetTemperatureMax()) / 9f;
        float initTempature = temparetureMinValue + Random.Range(-temperatureShift, temperatureShift);
        initTempature += temperatureDifference * 5f;

        await Task.Run(() => EnvironmentData.SetTemperature(initTempature));
    }

    //風向生成処理
    private async Task GenerateWindDirection(int enumIndex)
    {
        Direction dir = Direction.North;
        switch (enumIndex)
        {
            case 0:
                dir = Direction.North;
                break;
            case 1:
                dir = Direction.NorthEast;
                break;
            case 2:
                dir = Direction.NorthWest;
                break;
            case 3:
                dir = Direction.Sounth;
                break;
            case 4:
                dir = Direction.SounthEast;
                break;
            case 5:
                dir = Direction.SounthWest;
                break;
            case 6:
                dir = Direction.East;
                break;
            case 7:
                dir = Direction.West;
                break;
        }

        await Task.Run(() => EnvironmentData.SetWindDirection(dir));
    }
    
    //ゲーム内での時間経過による気温変更処理
    private async Task GenerateNowTemperature()
    {
        int nowHour = EnvironmentData.GetHour();

        float nowTemperature = EnvironmentData.GetTemperature();

        float temperatureDifference = (EnvironmentData.GetTemperatureMin() - EnvironmentData.GetTemperatureMax()) / 9f;

        float temperatureShiftValue = Random.Range(0.2f, temperatureDifference) + Random.Range(0f, temperatureShift);

        if (nowHour > 5 && nowHour <= 14)
        {
            if(EnvironmentData.GetWeather() == Weather.Rainy)
            {
                temperatureShiftValue /= 2f;
            }
            else if (EnvironmentData.GetWeather() == Weather.Cloudy)
            {
                temperatureShiftValue /= 1.5f;
            }
            nowTemperature += temperatureShiftValue;
        }
        else
        {
            if (EnvironmentData.GetWeather() == Weather.Rainy)
            {
                temperatureShiftValue *= 1.5f;
            }
            else if (EnvironmentData.GetWeather() == Weather.Cloudy)
            {
                temperatureShiftValue *= 0.8f;
            }
            nowTemperature -= temperatureShiftValue;
        }

        await Task.Run(() => EnvironmentData.SetTemperature(nowTemperature));

        Debug.Log(EnvironmentData.GetTemperature());
    }

    //ゲーム内時間更新処理
    private async Task TimeUpdate()
    {
        int nowSecond = EnvironmentData.GetSecond();
        nowSecond++;
        if(nowSecond == 60)
        {
            nowSecond = 0;
            EnvironmentData.SetSecond(nowSecond);
            int nowMinute = EnvironmentData.GetMinute();
            nowMinute++;
            if(nowMinute == 60)
            {
                nowMinute = 0;
                EnvironmentData.SetMinute(nowMinute);
                int nowHour = EnvironmentData.GetHour();
                nowHour++;
                if(nowHour == 24)
                {
                    nowHour = 0;
                    EnvironmentData.SetHour(nowHour);
                    await DateUpdate();
                }
                else
                {
                    EnvironmentData.SetHour(nowHour);
                }
            }
            else
            {
                EnvironmentData.SetMinute(nowMinute);
            }
        }
        else
        {
            EnvironmentData.SetSecond(nowSecond);
        }
        timeUpdateWait = 0;
    }

    //ゲーム内月日更新処理
    private async Task DateUpdate()
    {
        int nowDay = EnvironmentData.GetDay();
        nowDay++;
        int nowMonth = EnvironmentData.GetMonth();
        switch (EnvironmentData.GetMonth())
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                if(nowDay == 32)
                {
                    nowDay = 1;
                    nowMonth++;
                    if (nowMonth == 13) nowMonth = 1;
                }
                break;
            case 2:
                if (nowDay == 29)
                {
                    nowDay = 1;
                    nowMonth++;
                }
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                if (nowDay == 31)
                {
                    nowDay = 1;
                    nowMonth++;
                }
                break;
        }

        await Task.Run(() => EnvironmentData.SetDay(nowDay));
        await Task.Run(() => EnvironmentData.SetMonth(nowMonth));
    }
}
