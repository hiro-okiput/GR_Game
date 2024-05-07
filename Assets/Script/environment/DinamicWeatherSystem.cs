using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using GR_Game;
using GR_Game.Enum;

public class DinamicWeatherSystem : MonoBehaviour
{
    [SerializeField, Header("‰½•b‚Éˆê“xˆ—‚³‚¹‚é‚©")]
    private float dWSStartWaitTime;

    private float nowWaitTime = 0;

    private void Update()
    {
        nowWaitTime += Time.deltaTime;
        if (nowWaitTime > dWSStartWaitTime) StartDinamicWeatherSystem();
    }

    public async Task StartDinamicWeatherSystem()
    {
        await GenerateTemparetureMinMax(EnvironmentData.GetMonth());

        EnvironmentData.SetWeather((Weather)Random.Range(0, (int)Weather.Max));
    }
    
    private async Task GenerateTemparetureMinMax(int month)
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
        await Task.Run(() => {});
    }
}
