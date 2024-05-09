using GR_Game;
using GR_Game.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    private float period;

    private float pi = 3.14f;

    private float stringLength = 1;

    public void InitializeSimulation()
    {
        period = 2 * pi * Math.Sqrt(stringLength / EnvironmentData.GetGravity());
    }


}
