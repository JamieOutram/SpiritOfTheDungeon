using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TimeControl
{
    public static readonly float[] timeSpeeds = {1f,2f,5f,10f};  
    public const float speedUpSpeed = 20f;
    private static float _gameSpeed = 1f;
    

    public static float GameSpeed
    {
        get { return _gameSpeed; }
        set {
            if (!isGamePaused)
                Time.timeScale = value;
            _gameSpeed = value;
            _index = Array.IndexOf(timeSpeeds, value);
        }
    }

    private static int _index;
    public static int Index
    {
        get { return _index; }
        private set
        {
            if(value>=0 && value < timeSpeeds.Length) { 
                if (!isGamePaused)
                    Time.timeScale = timeSpeeds[value];
                _gameSpeed = timeSpeeds[value];
                _index = value;
            }
            else
            {
                Debug.LogError("Set time speed by invalid index");
            }
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

    //-1 if custom speed, otherwise index of list
    public static void NextSpeed()
    {
        if(Index >= 0 && Index < timeSpeeds.Length)
        {
            //Index = Index + 1 < timeSpeeds.Length ? Index + 1 : 0;
            
            if (Index + 1 < timeSpeeds.Length)
                Index++;
            else
                Index = 0;
        }
        else
        {
            Debug.Log("Game speed index out of bounds, defaulting to index 0");
            Index = 0;
        }
        
    }

}
