using GR_Game;
using GR_Game.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTest : MonoBehaviour
{
    Vector2 move = Vector2.zero;

    private void Update()
    {
        Direction windDirection = EnvironmentData.GetWindDirection();
        float windSpeed = EnvironmentData.GetWindSpeed();

        switch(windDirection)
        {
            case Direction.North:
                move = Vector2.up;
                break;
            case Direction.NorthEast:
                move = new Vector2(1, 1);
                break;
            case Direction.NorthWest:
                move = new Vector2(1, -1);
                break;
            case Direction.Sounth:
                move = Vector2.down;
                break;
            case Direction.SounthEast:
                move = new Vector2(-1, 1);
                break;
            case Direction.SounthWest:
                move = new Vector2(-1, -1);
                break;
            case Direction.East:
                move = Vector2.right;
                break;
            case Direction.West:
                move = Vector2.left;
                break;
        }
        move.Normalize();
        move *= windSpeed * 0.01f;

        transform.position += new Vector3 (move.x, 0 , move.y);
    }

}
