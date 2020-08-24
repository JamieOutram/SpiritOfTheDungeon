using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseControl
{
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
            Time.timeScale = 1;
            isGamePaused = false;
        }
    }
}
