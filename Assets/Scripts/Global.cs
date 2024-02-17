using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global
{
    private static bool gamePaused = false;

    public static void SetPause(bool pause, bool timescale = true){
        gamePaused = pause;
        if(timescale){
            if(pause){
                Time.timeScale = 0;
            } else {
                Time.timeScale = 1;
            }
        }
    }

    public static bool IsGamePaused(){ return gamePaused; }

    public static void ReloadScene(){
        SceneManager.LoadScene("Scene");
        SetPause(false);
    }
}
