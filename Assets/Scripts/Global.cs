using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global// : MonoBehaviour
{
    private static bool gamePaused = false;

    public static void SetPause(bool pause){
        if(pause){
            Time.timeScale = 0;
            gamePaused = true;
        } else {
            Time.timeScale = 1;
            gamePaused = false;
        }
    }

    public static bool IsGamePaused(){ return gamePaused; }

    public static void ReloadScene(){
        SceneManager.LoadScene("Scene");
        SetPause(false);
    }
}
