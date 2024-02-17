using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI timer;
    [SerializeField]
    private Image[] chances;

    void Update()
    {
        if(timer && !Global.IsGamePaused())
            timer.text = ScoreLogger.GetTime().ToString("0.00s");//Updates timer in the UI
    }

    public void SetChances(int chanceCount){
        for(int i = 0; i < chances.Length; i++)
            chances[i].enabled = (i+1 <= chanceCount);
    }
}
