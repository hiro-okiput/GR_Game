using GR_Game;
using GR_Game.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    private float period;

    private float stringLength = 1f;

    private float startDegree = 60f;

    private float stepTime = 0.01f;

    private List<float> radianList = new List<float>();

    private bool isInitialized = false;

    private float nowSimulationTime = 0;

    public void InitializeSimulation()
    {
        float radian = GR_GameMath.DegreeToRadian(startDegree);

        float K = GR_GameMath.CompleteEllipticIntegralOfTheFirstKind(Math.Sin(radian / 2), 0.25);

        period = 4 * GR_GameMath.Sqrt(stringLength / (9.8f * EnvironmentData.GetGravity())) * K;

        int stepCount = (int)(period / stepTime);

        float angularVelocity = 0;

        for (int i = 0; i < stepCount; i++)
        {
            radianList.Add(radian);

            float angularVelocityValue = -((9.8f * EnvironmentData.GetGravity()) / stringLength) * (float)Math.Sin(radian);

            // Šp‘¬“x‚ÆŠp“x‚ÌXV
            angularVelocity += angularVelocityValue * stepTime;
            radian += angularVelocity * stepTime;
        }

        nowSimulationTime = 0;

        isInitialized = true;
    }

    private void Update()
    {
        if (!isInitialized) return;

        nowSimulationTime += Time.deltaTime;

        if (nowSimulationTime / stepTime >= radianList.Count)
        {
            nowSimulationTime = 0;
        }

        float nowRadian = radianList[(int)(nowSimulationTime / stepTime)];

        transform.rotation = Quaternion.Euler(0, 0, GR_GameMath.RadianToDegree(nowRadian));

    }


}
