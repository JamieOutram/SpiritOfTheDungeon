using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeControl
{
    public const float speedUpSpeed = 20f;
    private static float _gameSpeed = 1f;
    public static float GameSpeed
    {
        get { return _gameSpeed; }
        set {
            if (!isGamePaused)
                Time.timeScale = value;
            _gameSpeed = value;
        }
    }

    public static bool isGamePaused = false;


    public static void PauseGame(bool pause)
    {
        if (!isGamePaused && pause)
        {
            Time.timeScale = 0f;
            isGamePaused = true;
        }
        else if(isGamePaused && !pause)
        {
            Time.timeScale = GameSpeed;
            isGamePaused = false;
        }
    }

    public static void ToggleSpeedUp()
    {
        if (GameSpeed != speedUpSpeed)
            GameSpeed = speedUpSpeed;
        else
            GameSpeed = 1f;
    }

}
